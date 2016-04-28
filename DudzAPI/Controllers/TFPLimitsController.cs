using DudzAPI.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
                        limit.CounterPartyIDTo = reader.IsDBNull(reader.GetOrdinal("CounterPartyIdTo")) ? 0 : reader.GetInt64(reader.GetOrdinal("CounterPartyIdTo"));
                        limit.ShortnameTo = reader.IsDBNull(reader.GetOrdinal("ShortnameTo")) ? string.Empty : reader.GetString(reader.GetOrdinal("ShortnameTo"));
                        limit.CountryTo = reader.IsDBNull(reader.GetOrdinal("CountryTo")) ? string.Empty : reader.GetString(reader.GetOrdinal("CountryTo"));
                        limit.SublimitAmountInUSDTo = reader.IsDBNull(reader.GetOrdinal("SublimitAmountInUSDTo")) ? 0 : reader.GetDecimal(reader.GetOrdinal("SublimitAmountInUSDTo"));
                        limit.SublimitTypeTo = reader.IsDBNull(reader.GetOrdinal("SublimitTypeTo")) ? string.Empty : reader.GetString(reader.GetOrdinal("SublimitTypeTo"));
                        limit.SublimitTypeIdTo = reader.IsDBNull(reader.GetOrdinal("SublimitTypeIdTo")) ? 0 : reader.GetInt64(reader.GetOrdinal("SublimitTypeIdTo"));
                        limit.CounterPartyIDFrom = reader.IsDBNull(reader.GetOrdinal("CounterPartyIDFrom")) ? 0 : reader.GetInt64(reader.GetOrdinal("CounterPartyIDFrom"));
                        limit.ShortnameFrom = reader.IsDBNull(reader.GetOrdinal("ShortnameFrom")) ? string.Empty : reader.GetString(reader.GetOrdinal("ShortnameFrom"));
                        limit.CountryFrom = reader.IsDBNull(reader.GetOrdinal("CountryFrom")) ? string.Empty : reader.GetString(reader.GetOrdinal("CountryFrom"));
                        limit.SublimitAmountInUSDFrom = reader.IsDBNull(reader.GetOrdinal("SublimitAmountInUSDFrom")) ? 0 : reader.GetDecimal(reader.GetOrdinal("SublimitAmountInUSDFrom"));
                        limit.SublimitTypeFrom = reader.IsDBNull(reader.GetOrdinal("SublimitTypeFrom")) ? string.Empty : reader.GetString(reader.GetOrdinal("SublimitTypeFrom"));
                        limit.SublimitTypeIdFrom = reader.IsDBNull(reader.GetOrdinal("SublimitTypeIdFrom")) ? 0 : reader.GetInt64(reader.GetOrdinal("SublimitTypeIdFrom"));
                        limit.FinalLimit = reader.IsDBNull(reader.GetOrdinal("FinalLimit")) ? 0 : reader.GetDecimal(reader.GetOrdinal("FinalLimit"));

                        limits.Add(limit);
                    }
                }
            }
            return Ok(limits);
        }


        // GET: api/TFPLimits/5
        [HttpGet, Route("api/limit/get/{counterPartyId}/{sublimitTypeId}")]
        public IHttpActionResult Get(int counterPartyId, int sublimitTypeId)
        {
            List<TFPLimit> limits = new List<TFPLimit>();

            var finalSql = string.Format(Strings.GET_LIMIT_BY_COUNTERPARTYID_LIMITTYPE, counterPartyId, sublimitTypeId);

            using (DbCommand cmd = db.GetSqlStringCommand(finalSql))
            {
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        TFPLimit limit = new TFPLimit();
                        limit.CounterPartyIDTo = reader.IsDBNull(reader.GetOrdinal("CounterPartyIdTo")) ? 0 : reader.GetInt64(reader.GetOrdinal("CounterPartyIdTo"));
                        limit.ShortnameTo = reader.IsDBNull(reader.GetOrdinal("ShortnameTo")) ? string.Empty : reader.GetString(reader.GetOrdinal("ShortnameTo"));
                        limit.CountryTo = reader.IsDBNull(reader.GetOrdinal("CountryTo")) ? string.Empty : reader.GetString(reader.GetOrdinal("CountryTo"));
                        limit.SublimitAmountInUSDTo = reader.IsDBNull(reader.GetOrdinal("SublimitAmountInUSDTo")) ? 0 : reader.GetDecimal(reader.GetOrdinal("SublimitAmountInUSDTo"));
                        limit.SublimitTypeTo = reader.IsDBNull(reader.GetOrdinal("SublimitTypeTo")) ? string.Empty : reader.GetString(reader.GetOrdinal("SublimitTypeTo"));
                        limit.SublimitTypeIdTo = reader.IsDBNull(reader.GetOrdinal("SublimitTypeIdTo")) ? 0 : reader.GetInt64(reader.GetOrdinal("SublimitTypeIdTo"));
                        limit.CounterPartyIDFrom = reader.IsDBNull(reader.GetOrdinal("CounterPartyIDFrom")) ? 0 : reader.GetInt64(reader.GetOrdinal("CounterPartyIDFrom"));
                        limit.ShortnameFrom = reader.IsDBNull(reader.GetOrdinal("ShortnameFrom")) ? string.Empty : reader.GetString(reader.GetOrdinal("ShortnameFrom"));
                        limit.CountryFrom = reader.IsDBNull(reader.GetOrdinal("CountryFrom")) ? string.Empty : reader.GetString(reader.GetOrdinal("CountryFrom"));
                        limit.SublimitAmountInUSDFrom = reader.IsDBNull(reader.GetOrdinal("SublimitAmountInUSDFrom")) ? 0 : reader.GetDecimal(reader.GetOrdinal("SublimitAmountInUSDFrom"));
                        limit.SublimitTypeFrom = reader.IsDBNull(reader.GetOrdinal("SublimitTypeFrom")) ? string.Empty : reader.GetString(reader.GetOrdinal("SublimitTypeFrom"));
                        limit.SublimitTypeIdFrom = reader.IsDBNull(reader.GetOrdinal("SublimitTypeIdFrom")) ? 0 : reader.GetInt64(reader.GetOrdinal("SublimitTypeIdFrom"));
                        limit.FinalLimit = reader.IsDBNull(reader.GetOrdinal("FinalLimit")) ? 0 : reader.GetDecimal(reader.GetOrdinal("FinalLimit"));

                        limits.Add(limit);
                    }
                }
            }
            return Ok(limits);
        }

        [HttpGet, Route("api/limit/get/{counterPartyId}")]
        public IHttpActionResult Get(int counterPartyId)
        {
            List<TFPLimit> limits = new List<TFPLimit>();

            StringBuilder dynamicWhere = new StringBuilder();

            var finalSql = string.Format(Strings.GET_LIMIT_BY_COUNTERPARTYID, counterPartyId);

            using (DbCommand cmd = db.GetSqlStringCommand(finalSql))
            {
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        TFPLimit limit = new TFPLimit();
                        limit.CounterPartyIDTo = reader.IsDBNull(reader.GetOrdinal("CounterPartyIdTo")) ? 0 : reader.GetInt64(reader.GetOrdinal("CounterPartyIdTo"));
                        limit.ShortnameTo = reader.IsDBNull(reader.GetOrdinal("ShortnameTo")) ? string.Empty : reader.GetString(reader.GetOrdinal("ShortnameTo"));
                        limit.CountryTo = reader.IsDBNull(reader.GetOrdinal("CountryTo")) ? string.Empty : reader.GetString(reader.GetOrdinal("CountryTo"));
                        limit.SublimitAmountInUSDTo = reader.IsDBNull(reader.GetOrdinal("SublimitAmountInUSDTo")) ? 0 : reader.GetDecimal(reader.GetOrdinal("SublimitAmountInUSDTo"));
                        limit.SublimitTypeTo = reader.IsDBNull(reader.GetOrdinal("SublimitTypeTo")) ? string.Empty : reader.GetString(reader.GetOrdinal("SublimitTypeTo"));
                        limit.SublimitTypeIdTo = reader.IsDBNull(reader.GetOrdinal("SublimitTypeIdTo")) ? 0 : reader.GetInt64(reader.GetOrdinal("SublimitTypeIdTo"));
                        limit.CounterPartyIDFrom = reader.IsDBNull(reader.GetOrdinal("CounterPartyIDFrom")) ? 0 : reader.GetInt64(reader.GetOrdinal("CounterPartyIDFrom"));
                        limit.ShortnameFrom = reader.IsDBNull(reader.GetOrdinal("ShortnameFrom")) ? string.Empty : reader.GetString(reader.GetOrdinal("ShortnameFrom"));
                        limit.CountryFrom = reader.IsDBNull(reader.GetOrdinal("CountryFrom")) ? string.Empty : reader.GetString(reader.GetOrdinal("CountryFrom"));
                        limit.SublimitAmountInUSDFrom = reader.IsDBNull(reader.GetOrdinal("SublimitAmountInUSDFrom")) ? 0 : reader.GetDecimal(reader.GetOrdinal("SublimitAmountInUSDFrom"));
                        limit.SublimitTypeFrom = reader.IsDBNull(reader.GetOrdinal("SublimitTypeFrom")) ? string.Empty : reader.GetString(reader.GetOrdinal("SublimitTypeFrom"));
                        limit.SublimitTypeIdFrom = reader.IsDBNull(reader.GetOrdinal("SublimitTypeIdFrom")) ? 0 : reader.GetInt64(reader.GetOrdinal("SublimitTypeIdFrom"));
                        limit.FinalLimit = reader.IsDBNull(reader.GetOrdinal("FinalLimit")) ? 0 : reader.GetDecimal(reader.GetOrdinal("FinalLimit"));

                        limits.Add(limit);
                    }
                }
            }
            return Ok(limits);
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
