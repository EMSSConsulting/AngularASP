using Microsoft.AspNet.Builder;

namespace AngularASPNext
{
    public static class AngularFileProviderExtensions
    {
        public static IApplicationBuilder UseAngularFileServer(this IApplicationBuilder builder)
        {
            return builder.UseAngularFileServer("index.html");
        }

        public static IApplicationBuilder UseAngularFileServer(this IApplicationBuilder builder, params string[] indexPaths)
        {
            return builder.Use(next => new AngularFileProviderMiddleware(next, indexPaths).Invoke);
        }
    }
}