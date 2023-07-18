using Inventory.Application.Repository;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Repository;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var connString = config.GetConnectionString("RakeDbConn");
            services.AddDbContext<InventoryDbContext>(
                options => options.UseNpgsql(connString)
            );

             services.AddScoped<IStoreRepo, StoreRepo>();
             services.AddScoped<ITenantRepo, TenantRepo>();
             services.AddScoped<ISupplyRepo, SupplyRepo>();
            // services.AddScoped<ILocationRepo, LocationRepo>();
            // services.AddScoped<ITemsRepo, ItemRepo>();
            // services.AddScoped<ISalesRepo, SalesRepo>();
            // services.AddScoped<IOrderRepo, OrderRepo>();
            // services.AddScoped<IStockRepo, StockRepo>();

            return services;
        }
    }
}