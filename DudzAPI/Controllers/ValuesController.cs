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
        [HttpGet, Route("api/values/get")]
        public IHttpActionResult Get()
        {
            return Ok("Get All");
        }

        // GET api/values/5
        [HttpGet, Route("api/values/get/{id}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(string.Format("Get id={0}", id));
        }

        // POST api/values
        [HttpPost, Route("api/values/post")]
        public IHttpActionResult Post([FromBody]string value)
        {
            return Ok(value);
        }

        // PUT api/values/5
        [HttpPut, Route("api/values/put")]
        public IHttpActionResult Put([FromBody]string value)
        {
            return Ok(string.Format("value={0}", value));
        }

        // DELETE api/values/5
        [HttpDelete, Route("api/values/delete")]
        public IHttpActionResult Delete([FromBody]int id)
        {
            return Ok(string.Format("id={0}", id));
        }
    }
}
