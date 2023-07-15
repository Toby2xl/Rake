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
    //public DbSet<StoreItems> StoreItems { get; set; } = null!;

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

        modelBuilder.Entity<StoreItems>(entity =>
        {
            entity.ToTable("InvStoreItems");
            entity.HasIndex(x => x.TenantId);
            entity.HasIndex(x => x.BranchId);

            entity.HasKey(e => new { e.StoreId, e.ItemId });
            entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").ValueGeneratedOnAdd();
            entity.Property(e => e.LastModified).HasColumnType("timestamp with time zone").ValueGeneratedOnUpdate();
            entity.Property(e => e.Instock).HasPrecision(6, 1).HasColumnName("Quantity").IsRequired();
            entity.Property(e => e.UPCNumber).HasColumnName("UPCNumber").HasMaxLength(20);
            entity.Property(e => e.QuantityDetail).HasColumnType("jsonb");
            entity.Property(e => e.BranchId).IsRequired();
            entity.Property(e => e.TenantId).IsRequired();
        });

        
    }
}
