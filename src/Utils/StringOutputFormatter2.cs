using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace GitAttributesWeb.Utils
{
    public class StringOutputFormatter2 : StringOutputFormatter
    {
        public override bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            if (context.ObjectType == typeof(IEnumerable<string>))
            {
                return true;
            }

            return base.CanWriteResult(context);
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var valueAsEnumerable = context.Object as IEnumerable<string>;
            if (valueAsEnumerable != null)
            {
                string result = string.Join(",", valueAsEnumerable);
                
                var response = context.HttpContext.Response;
                MediaTypeHeaderValue contentType = ((OutputFormatterCanWriteContext)context).ContentType;
                Encoding encoding = (contentType != null ? contentType.Encoding : (Encoding)null) ?? Encoding.UTF8;
                CancellationToken cancellationToken = new CancellationToken();
                await response.WriteAsync(result, encoding, cancellationToken);
            }

            await base.WriteResponseBodyAsync(context);
        }
    }
}
