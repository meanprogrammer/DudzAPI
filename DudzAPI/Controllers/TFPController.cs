using DudzAPI.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace DudzAPI.Controllers
{
    public class TFPController : ApiController
    {
        Database db;

        public TFPController()
        {
            this.db = DatabaseFactory.CreateDatabase();
        }


        // GET: api/TFP
        [HttpGet, Route("api/get")]
        public IEnumerable<string> Get()
        {
            TFPParam tf = new TFPParam();
            tf.InquiryReferenceNo = string.Format("GU7777-777-{0}", RandomNumber());
            tf.IssuingBankName = 1727; // Counterparty --BRAC Bank
            tf.ConfirmingBankName = 1727;
            tf.TypeOfTradeTransaction = 15;
            tf.ApplicantName = 13399;
            tf.BeneficiaryName = 13400;
            tf.Tenor = 666;
            tf.Goods = "Here goes the goods description";
            tf.TotalTransactionValue = 123456;
            tf.AmountADBCovered = 10000;

            var ss = JsonConvert.SerializeObject(tf);

            return new string[] { "value1", "value2" };
        }


        private int RandomNumber()
        {
            Random r = new Random();
            return r.Next(100, 999);
        }

        // GET: api/TFP/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(string.Format("GET id={0}", id));
        }

        // POST: api/TFP
        [HttpPost, Route("api/post")]
        public void Post([FromBody]string json)
        {

            //{0}, --Here goes the transaction ID
		    //{1}, --Trade transaction type
            //{2}, --Applicant
            //{3}, --Beneficiary
            //'{4}', --Goods
            //{5}, --Tenor
            //{6}, --Total Transaction Value

            TFPParam tfp = JsonConvert.DeserializeObject<TFPParam>(json);

            StringBuilder b = new StringBuilder();
            string adbRef = tfp.InquiryReferenceNo;
            //GU7777-777-001
            using(DbCommand cmd1 = db.GetSqlStringCommand(string.Format(Strings.INSERT_TRANSACTION_SQL, tfp.IssuingBankName,adbRef)))
            {
                var transactionId = db.ExecuteScalar(cmd1);

                if (transactionId != null)
                { 
                    //INSERT TRANSACTION UEI
                    using (DbCommand ueiCmd = db.GetSqlStringCommand(
                        string.Format(Strings.INSERT_TRANSACTION_UEI_SQL, 
                        transactionId,
                        tfp.TypeOfTradeTransaction,
                        tfp.ApplicantName,
                        tfp.BeneficiaryName,
                        tfp.Goods,
                        tfp.Tenor,
                        tfp.TotalTransactionValue)
                        
                        ))
                    {
                        var ueiId = db.ExecuteScalar(ueiCmd);
                        if (ueiId != null)
                        {
                            using (DbCommand adbExpCmd = db.GetSqlStringCommand(string.Format(Strings.INSERT_TRANS_ADB_EXPOSURE, transactionId, ueiId, tfp.AmountADBCovered)))
                            {
                                db.ExecuteNonQuery(adbExpCmd);

                                using (DbCommand tranC = db.GetSqlStringCommand(string.Format(Strings.INSERT_COUNTERPARTIES_SQL, transactionId, tfp.IssuingBankName)))
                                {
                                    db.ExecuteNonQuery(tranC);
                                }


                                b.AppendFormat("{0},{1}{2}",transactionId, ueiId, Environment.NewLine);



                                File.AppendAllText(HttpContext.Current.Server.MapPath("~/createdIds.txt"), b.ToString());
                            }
                        }
                    }
                }

                /*
                if (recordId != null)
                { 
                    using(DbCommand deleteCmd = db.GetSqlStringCommand(string.Format("delete from [Transaction] where id={0}", recordId)))
                    {
                        db.ExecuteNonQuery(deleteCmd);
                    }
                }
                */ 
            }   

        }

        // PUT: api/TFP/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/TFP/5
        [HttpDelete, Route("api/delete")]
        public void Delete()
        {
            string[] ids = File.ReadAllLines(HttpContext.Current.Server.MapPath("~/createdIds.txt"));

            foreach (string id in ids)
            {
                var separatedIds = id.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                var sqlCmd = string.Format(
                    "delete from [Transaction] where id = {0};delete from [TransactionUEI] where id = {1};delete from [TransactionADBExposure] where [TransactionId] = {2} and [UEIId] = {3};delete from [TransactionCounterparties] where [TransactionId] = {4};",
                            separatedIds.FirstOrDefault(),
                            separatedIds.Last(),
                            separatedIds.FirstOrDefault(),
                            separatedIds.Last(),
                            separatedIds.FirstOrDefault()
                    );
                using (DbCommand deleteCommand = db.GetSqlStringCommand(sqlCmd))
                {
                    db.ExecuteNonQuery(deleteCommand);
                    File.WriteAllText(HttpContext.Current.Server.MapPath("~/createdIds.txt"), string.Empty);
                }
            }

            //delete from [Transaction] where id = 22319;
            //delete from [TransactionUEI] where id = 22465;
            //delete from [TransactionADBExposure] where [TransactionId] = 22319 and [UEIId] = 22465;
            //delete from [TransactionCounterparties] where [TransactionId] = 22319 and [IssuingBankId] = 22465;
        }
    }
}
