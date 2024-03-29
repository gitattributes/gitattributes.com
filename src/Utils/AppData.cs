﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;
using NuGet;

namespace GitAttributesWeb.Utils
{
    public class AppData
    {
        private IEnumerable<FileTemplateInfo> files;

        public AppData(IWebHostEnvironment env)
        {
            this.Init(env);
        }

        private void Init(IWebHostEnvironment env)
        {
            var dataPath = Path.Combine(env.WebRootPath, "data");
            var files = PathResolver.PerformWildcardSearch(dataPath, "*.gitattributes");

            var q = from file in files
                    let filename = Path.GetFileNameWithoutExtension(file)
                    let name = filename.ToLowerInvariant()
                    orderby name
                    select new FileTemplateInfo
                    {
                        Id = name,
                        Name = filename,
                        Path = file
                    };

            this.files = q.ToList();
        }

        public IEnumerable<FileTemplateInfo> Files
        {
            get { return this.files; }
        }

        public string FilesJson
        {
            get
            {
                return JsonSerializer.Serialize(this.Files);
            }
        }
    }
}
