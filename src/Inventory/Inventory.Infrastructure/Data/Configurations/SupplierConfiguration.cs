using System;

using Inventory.Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Data.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("InvSupplier");
        builder.HasIndex(x => x.TenantId);
        builder.HasIndex(x => x.Name).UseCollation("case-insensitive");
        builder.HasIndex(x => x.BranchId);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(250).UseCollation("case-insensitive").IsRequired();
        builder.Property(x => x.Email).HasMaxLength(300).IsRequired();
        builder.Property(x => x.Address).HasMaxLength(350).IsRequired();
        builder.Property(x => x.TenantId).IsRequired();
        builder.Property(x => x.BranchId).IsRequired();
    }
}
