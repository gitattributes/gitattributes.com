using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitAttributesWeb.Utils;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;

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

        // GET: api/{types}
        [HttpGet]
        [Route("{types}")]
        public async Task<IActionResult> Get(string types)
        {
            var content = await GetAttributesContent(types);
            if (content == null)
            {
                return new NoContentResult();
            }

            return Content(content);
        }

        // GET: api/f/{id}
        [HttpGet]
        [Route("f/{types}")]
        public async Task<IActionResult> GetFile(string types)
        {
            var content = await GetAttributesContent(types);
            if (content == null)
            {
                return new NoContentResult();
            }

            var bytes = Encoding.UTF8.GetBytes(content);
            return File(bytes, contentType: "text/plain", fileDownloadName: "gitattributes");
        }

        private async Task<string> GetAttributesContent(string types)
        {
            if (String.IsNullOrWhiteSpace(types))
            {
                return null;
            }

            var list = types.Split(',');

            var q = from file in this.data.Files
                    where list.Contains(file.Id)
                    select file;

            if (!q.Any())
            {
                return null;
            }

            var sb = new StringBuilder();
            foreach (var file in q.ToList())
            {
                using (var reader = System.IO.File.OpenText(file.Path))
                {
                    var content = await reader.ReadToEndAsync();
                    sb.Append(content);
                }
            }

            return sb.ToString();
        }
    }
}
