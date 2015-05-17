using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GitAttributesWeb.Utils;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Runtime;
using NuGet;

namespace GitAttributesWeb.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        private readonly AppData data;

        public ApiController(AppData data)
        {
            this.data = data;
        }

        // GET: api/list
        [HttpGet]
        [Route("list")]
        public IEnumerable<string> Get()
        {
            var q = from file in this.data.Files
                    select file.Id;

            return q.ToList();
        }

        // GET: api/{id}
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(string id)
        {
            var q = from file in this.data.Files
                    where file.Id == id
                    select file;

            var validFile = q.FirstOrDefault();
            if (validFile == null)
            {
                return new NoContentResult();
            }

            string content = System.IO.File.ReadAllText(validFile.Path);

            return Content(content);
        }

        // GET: api/f/{id}
        [HttpGet]
        [Route("f/{id}")]
        public IActionResult GetFile(string id)
        {
            var q = from file in this.data.Files
                    where file.Id == id
                    select file;

            var validFile = q.FirstOrDefault();
            if (validFile == null)
            {
                return new NoContentResult();
            }

            var content = System.IO.File.ReadAllBytes(validFile.Path);

            return File(content, contentType: "text/plain", fileDownloadName: "gitattributes");
        }
    }
}
