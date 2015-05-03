using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Runtime;
using NuGet;

namespace GitAttributesWeb.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        private readonly IHostingEnvironment env;

        public ApiController(IHostingEnvironment env)
        {
            this.env = env;
        }

        // GET: api/list
        [HttpGet]
        [Route("list")]
        public IEnumerable<string> Get()
        {
            var dataPath = Path.Combine(this.env.WebRootPath, "data");
            var files = PathResolver.PerformWildcardSearch(dataPath, "*.gitattributes");

            var q = from file in files
                    let name = Path.GetFileNameWithoutExtension(file).ToLowerInvariant()
                    orderby name
                    select name;

            return q.ToList();
        }

        // GET: api/{id}
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(string id)
        {
            var dataPath = Path.Combine(this.env.WebRootPath, "data");
            var files = PathResolver.PerformWildcardSearch(dataPath, "*.gitattributes");

            var q = from file in files
                    let name = Path.GetFileNameWithoutExtension(file).ToLowerInvariant()
                    where name == id
                    select file;

            var validFile = q.FirstOrDefault();
            if (validFile == null)
            {
                return new NoContentResult();
            }

            string content = System.IO.File.ReadAllText(validFile);

            return Content(content);
        }

        // GET: api/f/{id}
        [HttpGet]
        [Route("f/{id}")]
        public IActionResult GetFile(string id)
        {
            var dataPath = Path.Combine(this.env.WebRootPath, "data");
            var files = PathResolver.PerformWildcardSearch(dataPath, "*.gitattributes");

            var q = from file in files
                    let name = Path.GetFileNameWithoutExtension(file).ToLowerInvariant()
                    where name == id
                    select file;

            var validFile = q.FirstOrDefault();
            if (validFile == null)
            {
                return new NoContentResult();
            }

            var content = System.IO.File.ReadAllBytes(validFile);

            return File(content, contentType: "text/plain", fileDownloadName: "gitattributes");
        }
    }
}
