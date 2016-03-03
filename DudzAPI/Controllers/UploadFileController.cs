using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;

namespace DudzAPI.Controllers
{
    public class UploadFileController : ApiController
    {
        // GET: api/UploadFile
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/UploadFile/5
        public string Get(int id)
        {
            return "value";
        }
        // POST: api/UploadFile
        [HttpPost, Route("api/file/post")]
        public void Post()
        {
            
        }

        // PUT: api/UploadFile/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UploadFile/5
        public void Delete(int id)
        {
        }
    }
}
