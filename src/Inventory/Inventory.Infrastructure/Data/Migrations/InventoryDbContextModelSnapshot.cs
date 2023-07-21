﻿// <auto-generated />
using System;
using Inventory.Core.ValueObject;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Inventory.Infrastructure.Data.Migrations
{
    [DbContext(typeof(InventoryDbContext))]
    partial class InventoryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:CollationDefinition:case-insensitive", "en-us-ks-primary,en-us-ks-primary,icu,False")
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Inventory.Core.Entities.Branch", b =>
                {
                    b.Property<int>("BranchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("BranchID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BranchId"));

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("TenantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("tenantid")
                        .HasDefaultValueSql("1");

                    b.HasKey("BranchId")
                        .HasName("Branch_pkey");

                    b.ToTable("Branch", (string)null);
                });

            modelBuilder.Entity("Inventory.Core.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BranchId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .UseCollation("case-insensitive");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("TenantId");

                    b.ToTable("InvCategory", (string)null);
                });

            modelBuilder.Entity("Inventory.Core.Entities.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("BranchId")
                        .HasColumnType("integer")
                        .HasColumnName("Branch Id");

                    b.Property<int?>("CategoryID")
                        .HasColumnType("integer");

                    b.Property<decimal>("CostPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)")
                        .HasColumnName("Cost Per Unit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("character varying(350)");

                    b.Property<bool>("IsForSale")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsoftDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModified")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .UseCollation("case-insensitive");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)")
                        .HasColumnName("Selling Price");

                    b.Property<int>("SN")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("SN");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("SN"));

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.Property<string>("UPCNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Unit");

                    b.Property<Guid?>("WarehouseId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("CategoryID");

                    b.HasIndex("SN");

                    b.HasIndex("TenantId");

                    b.ToTable("InvStockItems", (string)null);
                });

            modelBuilder.Entity("Inventory.Core.Entities.StoreItems", b =>
                {
                    b.Property<Guid>("StoreId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uuid");

                    b.Property<int>("BranchId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Instock")
                        .HasPrecision(6, 1)
                        .HasColumnType("numeric(6,1)")
                        .HasColumnName("Quantity");

                    b.Property<DateTime>("LastModified")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp with time zone");

                    b.Property<QuantityDetails>("QuantityDetail")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.Property<string>("UPCNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("UPCNumber");

                    b.HasKey("StoreId", "ItemId");

                    b.HasIndex("BranchId");

                    b.HasIndex("ItemId");

                    b.HasIndex("TenantId");

                    b.ToTable("InvStoreItems", (string)null);
                });

            modelBuilder.Entity("Inventory.Core.Entities.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("character varying(350)");

                    b.Property<int>("BranchId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsoftDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .UseCollation("case-insensitive");

                    b.Property<string>("PhoneNumbers")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("Name");

                    NpgsqlIndexBuilderExtensions.UseCollation(b.HasIndex("Name"), new[] { "case-insensitive" });

                    b.HasIndex("TenantId");

                    b.ToTable("InvSupplier", (string)null);
                });

            modelBuilder.Entity("Inventory.Core.Entities.Tenants", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("AmountPaid")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasMaxLength(240)
                        .HasColumnType("character varying(240)");

                    b.Property<string>("ContactPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<DateTime?>("DateOnboarded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("NoOfSubscribedUsers")
                        .HasColumnType("integer");

                    b.Property<string>("SchoolName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("Sn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("SN");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Sn"));

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("SubscriptionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("SubscriptionExpiryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TenantName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .UseCollation("case-insensitive");

                    b.HasKey("Id");

                    b.HasIndex("TenantName");

                    NpgsqlIndexBuilderExtensions.UseCollation(b.HasIndex("TenantName"), new[] { "case-insensitive" });

                    b.ToTable("Tenants", (string)null);
                });

            modelBuilder.Entity("Inventory.Core.Entities.Warehouse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("BranchId")
                        .HasColumnType("integer")
                        .HasColumnName("Branch Id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .UseCollation("case-insensitive");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("Name");

                    NpgsqlIndexBuilderExtensions.UseCollation(b.HasIndex("Name"), new[] { "case-insensitive" });

                    b.HasIndex("TenantId");

                    b.ToTable("InvWarehouse", (string)null);
                });

            modelBuilder.Entity("Inventory.Core.Entities.Item", b =>
                {
                    b.HasOne("Inventory.Core.Entities.Category", "Category")
                        .WithMany("ItemCategories")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Inventory.Core.Entities.StoreItems", b =>
                {
                    b.HasOne("Inventory.Core.Entities.Item", "Item")
                        .WithMany("WarehouseItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Inventory.Core.Entities.Warehouse", "Warehouse")
                        .WithMany("WarehouseItems")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Inventory.Core.Entities.Category", b =>
                {
                    b.Navigation("ItemCategories");
                });

            modelBuilder.Entity("Inventory.Core.Entities.Item", b =>
                {
                    b.Navigation("WarehouseItems");
                });

            modelBuilder.Entity("Inventory.Core.Entities.Warehouse", b =>
                {
                    b.Navigation("WarehouseItems");
                });
#pragma warning restore 612, 618
        }
    }
}
