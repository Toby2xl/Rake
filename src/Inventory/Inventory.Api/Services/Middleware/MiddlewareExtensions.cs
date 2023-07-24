using System;

namespace Inventory.Api.Services.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseTenantHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TenantMiddleware>();
        }
    }
}
