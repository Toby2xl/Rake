#if DEBUG //Runs only on Debug mode or in Development.
using JasperFx.Core;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Inventory.Infrastructure.Data;

public class SqlDesignTimeDb : IDesignTimeDbContextFactory<InventoryDbContext>
{
    public InventoryDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<InventoryDbContext>();
        builder.UseNpgsql("Host = localhost; Username = postgres; Password = MCdonald12; Database = Rake-InventoryDB");
        return new InventoryDbContext(builder.Options);
    }
}
#endif