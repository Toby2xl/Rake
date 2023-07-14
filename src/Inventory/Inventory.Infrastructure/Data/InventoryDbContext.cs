using Inventory.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inventory.Infrastructure.Data;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
                .LogTo(
                    action: Console.WriteLine,
                    minimumLevel: LogLevel.Information
                );
    }

    public DbSet<Warehouse> Warehouses { get; set; } = null!;
    public DbSet<Supplier> Suppliers { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<Branch> Branches { get; set; } = null!;
    public DbSet<Tenants> Tenants { get; set; } = null!;
    public DbSet<StoreItems> StoreItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var inventoryDbContextAsembly = typeof(InventoryDbContext).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(inventoryDbContextAsembly);

        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.BranchId)
                .HasName("tbl_Branch_pkey");

            entity.ToTable("tbl_Branch");

            entity.Property(e => e.BranchId)
                .HasColumnName("BranchID");

            entity.Property(e => e.BranchName).HasMaxLength(200);

            entity.Property(e => e.TenantId)
                .HasColumnName("tenantid")
                .HasDefaultValueSql("1");
        });

        modelBuilder.Entity<Tenants>(entity =>
        {
            entity.ToTable("tbl_Tenant");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.AmountPaid).HasPrecision(18, 2);

            entity.Property(e => e.ContactEmail).HasMaxLength(240);

            entity.Property(e => e.ContactPhoneNumber).HasMaxLength(120);

            entity.Property(e => e.DateOnboarded).HasColumnType("timestamp with time zone");

            entity.Property(e => e.SchoolName).HasMaxLength(200);

            entity.Property(e => e.Sn)
                .ValueGeneratedOnAdd()
                .HasColumnName("SN")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.SubscriptionDate).HasColumnType("timestamp with time zone");

            entity.Property(e => e.SubscriptionExpiryDate).HasColumnType("timestamp with time zone");

            entity.Property(e => e.TenantName).HasMaxLength(100);
        });
    }
}
