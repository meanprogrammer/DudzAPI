using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DudzAPI.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        static string postedValue;
        
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return HttpContext.Current.Session["vvv"].ToString(); //postedValue;//Guid.NewGuid().ToString();
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            postedValue = value;
            var traceSource = new TraceSource("AppHarborTraceSource", defaultLevel: SourceLevels.All);
            traceSource.TraceEvent(TraceEventType.Critical, 0, value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
