using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var connString = config.GetConnectionString("localConn");
            var testDb = config.GetSection("");
            // services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            // services.AddDbContext<InventoryDbContext>(
            //     options => options.UseNpgsql("Host=localhost; Username=postgres; Password=MCdonald12; Database =inventory")
            //     //options => options.UseNpgsql("Host=162.0.222.79; Port=5432; Username=mystaruser; Password=mystarpostgr; Database=mystardbtest")
            // );

            
            // services.AddScoped<IStoreRepo, StoreRepo>();
            // services.AddScoped<ISupplyRepo, SupplyRepo>();
            // services.AddScoped<ITenantRepo, TenantRepo>();
            // services.AddScoped<ILocationRepo, LocationRepo>();
            // services.AddScoped<ITemsRepo, ItemRepo>();
            // services.AddScoped<ISalesRepo, SalesRepo>();
            // services.AddScoped<IOrderRepo, OrderRepo>();
            // services.AddScoped<IStockRepo, StockRepo>();

            return services;
        }
    }
}