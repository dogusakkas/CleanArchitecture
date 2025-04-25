using Microsoft.AspNetCore.Diagnostics;

namespace CleanArchitecture.WebApi.Middleware
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseMiddlewareExtensions(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
