using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Threading.Tasks;
using Microsoft.AspNet.StaticFiles;
using System.Collections.Generic;

namespace AngularASPNext
{
    public class AngularFileProviderMiddleware
    {
        protected readonly RequestDelegate Next;
        protected readonly IEnumerable<string> IndexFiles;

        public AngularFileProviderMiddleware(RequestDelegate next, IEnumerable<string> indexFiles)
        {
            Next = next;
            IndexFiles = indexFiles;
        }

        public async Task Invoke(HttpContext context)
        {
            if(!context.Request.Method.Equals("GET", StringComparison.InvariantCultureIgnoreCase))
            {
                await Next(context);
                return;
            }

            if(context.Request.Path.ToString().StartsWith("/api"))
            {
                await Next(context);
                return;
            }

            context.Response.ContentType = "text/html";
            foreach(var indexFile in IndexFiles)
            {
                try
                {
                    await context.Response.SendFileAsync(indexFile);
                    return;
                }
                catch
                { }
            }

            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("<html><body><h1>Not Found</h1><p>Could not find the Angular application entrypoint</p></body></html>");
        }
    }
}