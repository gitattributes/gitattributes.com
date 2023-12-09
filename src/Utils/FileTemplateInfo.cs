using System;
using System.Text.Json.Serialization;

namespace GitAttributesWeb.Utils
{
    public class FileTemplateInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public string Path { get; set; }
    }
}
