using DudzAPI.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DudzAPI.Controllers
{
    public class CounterPartyController : ApiController
    {
        Database db;

        public CounterPartyController()
        {
            db = DatabaseFactory.CreateDatabase();
        }

        [HttpGet, Route("api/cparty/getall")]
        public IHttpActionResult Get()
        {
            List<CounterParty> counterparties = new List<CounterParty>();
            using (DbCommand cmd = db.GetSqlStringCommand("select Id, Bankname from CounterParty order by Bankname"))
            {
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        CounterParty cps = new CounterParty();
                        cps.ID = reader.IsDBNull(reader.GetOrdinal("Id")) ? 0 : reader.GetInt64(reader.GetOrdinal("Id"));
                        cps.Bankname = reader.IsDBNull(reader.GetOrdinal("Bankname")) ? string.Empty : reader.GetString(reader.GetOrdinal("Bankname"));
                        counterparties.Add(cps);
                    }
                }
            }
            return Ok(counterparties);
        }

        // GET: api/CounterParty/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CounterParty
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CounterParty/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CounterParty/5
        public void Delete(int id)
        {
        }
    }
}
