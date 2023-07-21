using System;

using Inventory.Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Data.Configurations;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("InvWarehouse");
        //builder.HasQueryFilter(x => x.TenantId == x.TenantId);
        builder.HasIndex(x => x.TenantId);
        builder.HasIndex(x => x.Name).UseCollation("case-insensitive");
        builder.HasIndex(x => x.BranchId);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(150).UseCollation("case-insensitive").IsRequired();
        builder.Property(x => x.Description).HasMaxLength(300);
        builder.Property(x => x.TenantId).IsRequired();
        builder.Property(x => x.BranchId).HasColumnName("Branch Id");

        builder.HasMany(x => x.StockItems).WithMany(x => x.Warehouse)
                                              .UsingEntity<StoreItems>(
                                                x => x.HasOne(x => x.Item).WithMany(x => x.WarehouseItems).HasForeignKey(x => x.ItemId),
                                                x => x.HasOne(x => x.Warehouse).WithMany(x => x.WarehouseItems).HasForeignKey(x => x.StoreId)
                                              );
    }
}
