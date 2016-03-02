using DudzAPI.Helper;
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

        // GET api/values/5
        [HttpGet, Route("api/values/get")]
        public string Get(int id)
        {
            traceSource.TraceEvent(TraceEventType.Information, 0, id.ToString());
            return Guid.NewGuid().ToString();
        }

        // POST api/values
        [HttpPost, Route("api/values/post")]
        public IHttpActionResult Post([FromBody]string value)
        {
            return Ok(value);
        }

        // PUT api/values/5
        [HttpPut, Route("api/values/put")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete, Route("api/values/delete")]
        public void Delete(int id)
        {
        }
    }
}
