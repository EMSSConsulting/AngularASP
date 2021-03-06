﻿using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;

namespace AngularASPNext
{
    public class AngularFileProviderMiddleware
    {
        protected readonly RequestDelegate Next;
        protected readonly IEnumerable<string> IndexFiles;
        protected readonly IFileProvider FileProvider;

        public AngularFileProviderMiddleware(RequestDelegate next, IFileProvider fileProvider, IEnumerable<string> indexFiles)
        {
            Next = next;
            IndexFiles = indexFiles;
            FileProvider = fileProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            if(!context.Request.Method.Equals("GET", StringComparison.CurrentCultureIgnoreCase))
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

            foreach (var indexFile in IndexFiles)
            {
                try
                {
                    var file = FileProvider.GetFileInfo(indexFile);
                    if (!file.Exists) continue;

                    await context.Response.SendFileAsync(file);
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