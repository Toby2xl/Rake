using System;

using Inventory.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Data.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("InvStockItems");
            builder.HasIndex(x => x.TenantId);
            builder.HasIndex(x => x.BranchId);
            builder.HasIndex(e => e.SN);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.SN).ValueGeneratedOnAdd().HasColumnName("SN").UseIdentityAlwaysColumn();
            builder.Property(x => x.Name).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(350);
            builder.Property(x => x.TenantId).IsRequired();
            builder.Property(x => x.Unit).HasColumnName("Unit");
            builder.Property(x => x.WarehouseId).IsRequired();
            builder.Property(x => x.IsForSale).IsRequired();
            builder.Property(x => x.Price).HasPrecision(18, 2).HasColumnName("Selling Price");
            builder.Property(x => x.CostPrice).HasPrecision(18, 2).HasColumnName("Cost Per Unit").IsRequired();
            builder.Property(x => x.BranchId).HasColumnName("Branch Id").IsRequired();
            builder.Property(x => x.UPCNumber).HasMaxLength(20);
            builder.Property(x => x.LastModified).HasColumnType("timestamp with time zone").ValueGeneratedOnUpdate();
        }
    }
}
