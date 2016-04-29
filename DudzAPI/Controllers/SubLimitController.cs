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
    public class SubLimitController : ApiController
    {
        Database db;

        public SubLimitController()
        {
            db = DatabaseFactory.CreateDatabase();
        }

        // GET: api/SubLimit
        [HttpGet, Route("api/sublimit/getall")]
        public IHttpActionResult Get()
        {
            //select Id, Mnemonic from SubLimitType
            List<SubLimit> types = new List<SubLimit>();
            using (DbCommand cmd = db.GetSqlStringCommand("select Id, Mnemonic from SubLimitType"))
            {
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        SubLimit sb = new SubLimit();
                        sb.ID = reader.IsDBNull(reader.GetOrdinal("Id")) ? 0 : reader.GetInt64(reader.GetOrdinal("Id"));
                        sb.Mnemonic = reader.IsDBNull(reader.GetOrdinal("Mnemonic")) ? string.Empty : reader.GetString(reader.GetOrdinal("Mnemonic"));
                        types.Add(sb);
                    }
                }
            }
            return Ok(types);
        }

        // GET: api/SubLimit/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SubLimit
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SubLimit/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SubLimit/5
        public void Delete(int id)
        {
        }
    }
}
