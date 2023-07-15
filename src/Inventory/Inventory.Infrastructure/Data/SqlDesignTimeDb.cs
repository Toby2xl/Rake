using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Inventory.Infrastructure.Data;

public class SqlDesignTimeDb : IDesignTimeDbContextFactory<InventoryDbContext>
{
    private readonly IConfiguration _config;
    public SqlDesignTimeDb(IConfiguration config)
    {
        _config = config;
    }
    public SqlDesignTimeDb(){}

    public InventoryDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<InventoryDbContext>();
        builder.UseNpgsql("Host = localhost; Username = postgres; Password = MCdonald12; Database = Rake-InvnetoryDB");
        //builder.UseNpgsql(_config.GetConnectionString(""));

        return new InventoryDbContext(builder.Options);
    }
}