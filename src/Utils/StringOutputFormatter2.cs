using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Net.Http.Headers;

namespace GitAttributesWeb.Utils
{
    public class StringOutputFormatter2 : StringOutputFormatter
    {
        public override bool CanWriteResult(OutputFormatterContext context, MediaTypeHeaderValue contentType)
        {
            if (context.DeclaredType == typeof(IEnumerable<string>))
            {
                return true;
            }

            return base.CanWriteResult(context, contentType);
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterContext context)
        {
            var valueAsEnumerable = context.Object as IEnumerable<string>;
            if (valueAsEnumerable != null)
            {
                string result = string.Join(",", valueAsEnumerable);

                var response = context.HttpContext.Response;
                await response.WriteAsync(result, context.SelectedEncoding);
                return;
            }

            await base.WriteResponseBodyAsync(context);
        }
    }
}
