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
        TraceSource traceSource = new TraceSource("AppHarborTraceSource", defaultLevel: SourceLevels.All);
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            traceSource.TraceEvent(TraceEventType.Information, 0, id.ToString());
            return Guid.NewGuid().ToString();
        }

        // POST api/values
        [HttpPost]
        public HttpResponseMessage Post([FromBody]string value)
        {
            HttpResponseMessage resp = Request.CreateResponse();
            resp.ReasonPhrase = value;
            resp.StatusCode = HttpStatusCode.Forbidden;
            return resp;
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
