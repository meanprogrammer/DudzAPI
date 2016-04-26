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
    public class TFPLimitsController : ApiController
    {
        Database db;

        public TFPLimitsController()
        {
            this.db = DatabaseFactory.CreateDatabase();
        }

        // GET: api/TFPLimits
        [HttpGet, Route("api/limit/get")]
        public IHttpActionResult Get()
        {
            List<TFPLimit> limits = new List<TFPLimit>();
            using (DbCommand cmd = db.GetSqlStringCommand(Strings.GETALL_LIMIT_SQL))
            {
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        TFPLimit limit = new TFPLimit();
                        limit.CounterPartyIDTo = reader.IsDBNull(0) ? 0 : reader.GetInt64(0);
                        limit.ShortnameTo = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        limit.CountryTo = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        limit.SublimitAmountInUSDTo = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3);
                        limit.SublimitTypeTo = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        limit.CounterPartyIDFrom = reader.IsDBNull(5) ? 0 : reader.GetInt64(5);
                        limit.ShortnameFrom = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        limit.CountryFrom = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        limit.SublimitAmountInUSDFrom = reader.IsDBNull(8) ? 0 : reader.GetDecimal(8);
                        limit.SublimitTypeFrom = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        limit.FinalLimit = reader.IsDBNull(10) ? 0 : reader.GetDecimal(10);

                        limits.Add(limit);
                    }
                }
            }
            return Ok(limits);
        }

        // GET: api/TFPLimits/5

        public string Get(int id)
        {
            return "value";
        }

        // POST: api/TFPLimits
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/TFPLimits/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TFPLimits/5
        public void Delete(int id)
        {
        }
    }
}
