using System;
using System.Net;

using Inventory.Application.Service;

namespace Inventory.Api.Services.Middleware;

public class TenantMiddleware
{
    private readonly ILogger<TenantMiddleware> _logger;
    private readonly RequestDelegate _next;
    public TenantMiddleware(ILogger<TenantMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ITenantService tenantService)
    {
        _logger.LogInformation("Entered the Tenant Middle ware Handler timeSpan");
        var route = context.GetRouteData();
        if (!route.Values.TryGetValue("tenant", out var values))
        {
            await context.Response.WriteAsJsonAsync(new { message = "No tenant name identified", statusCode = HttpStatusCode.BadRequest });
            return;
        }
        var tenant = values?.ToString()?.Trim();
        if(!await tenantService.IsTenantAvailableAsync(tenant!, CancellationToken.None))
        {
            await context.Response.WriteAsJsonAsync(new { message = "No tenant name or valid tenant name identified", statusCode = HttpStatusCode.BadRequest });
            return;
        }
        tenantService.SetTenant(tenant!);

        await _next(context);
        _logger.LogInformation("Tenant Identifier obtained.....{tenantServiceName} with Tenant Id of {tenantServiceId}", tenantService.Tenant, tenantService.TenantId);
    }

}
