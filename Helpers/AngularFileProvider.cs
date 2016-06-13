using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

namespace AngularASPNext
{
    public static class AngularFileProviderExtensions
    {
        public static IApplicationBuilder UseAngularFileServer(this IApplicationBuilder builder, IFileProvider fileProvider)
        {
            return builder.UseAngularFileServer(fileProvider, "index.html");
        }

        public static IApplicationBuilder UseAngularFileServer(this IApplicationBuilder builder, IFileProvider fileProvider, params string[] indexPaths)
        {
            return builder.Use(next => new AngularFileProviderMiddleware(next, fileProvider, indexPaths).Invoke);
        }
    }
}