using FluentValidation;

using Inventory.Application.Service;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(assembly);
        services.AddValidatorsFromAssembly(assembly);
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); <-- if there's conflict, use this instead....
        services.AddScoped<ITenantService, TenantService>();
        return services;
    }
}
