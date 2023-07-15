using System;
using Inventory.Core.ValueObject;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Inventory.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvSupplier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Address = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    PhoneNumbers = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvSupplier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvWarehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(name: "Branch Id", type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvWarehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Branch",
                columns: table => new
                {
                    BranchID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BranchName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    tenantid = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("tbl_Branch_pkey", x => x.BranchID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Tenant",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    SN = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    TenantName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SchoolName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DateOnboarded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    NoOfSubscribedUsers = table.Column<int>(type: "integer", nullable: true),
                    SubscriptionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SubscriptionExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AmountPaid = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    ContactPhoneNumber = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    ContactEmail = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Tenant", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InvStockItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SN = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(name: "Branch Id", type: "integer", nullable: false),
                    UPCNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false),
                    IsForSale = table.Column<bool>(type: "boolean", nullable: false),
                    IsoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    SellingPrice = table.Column<decimal>(name: "Selling Price", type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CostPerUnit = table.Column<decimal>(name: "Cost Per Unit", type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryID = table.Column<int>(type: "integer", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvStockItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvStockItems_InvCategory_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "InvCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "InvStoreItems",
                columns: table => new
                {
                    StoreId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(6,1)", precision: 6, scale: 1, nullable: false),
                    UPCNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    QuantityDetail = table.Column<QuantityDetails>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvStoreItems", x => new { x.StoreId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_InvStoreItems_InvStockItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "InvStockItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvStoreItems_InvWarehouse_StoreId",
                        column: x => x.StoreId,
                        principalTable: "InvWarehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvCategory_BranchId",
                table: "InvCategory",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_InvCategory_TenantId",
                table: "InvCategory",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_InvStockItems_Branch Id",
                table: "InvStockItems",
                column: "Branch Id");

            migrationBuilder.CreateIndex(
                name: "IX_InvStockItems_CategoryID",
                table: "InvStockItems",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_InvStockItems_SN",
                table: "InvStockItems",
                column: "SN");

            migrationBuilder.CreateIndex(
                name: "IX_InvStockItems_TenantId",
                table: "InvStockItems",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_InvStoreItems_BranchId",
                table: "InvStoreItems",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_InvStoreItems_ItemId",
                table: "InvStoreItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InvStoreItems_TenantId",
                table: "InvStoreItems",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_InvSupplier_BranchId",
                table: "InvSupplier",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_InvSupplier_Name",
                table: "InvSupplier",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_InvSupplier_TenantId",
                table: "InvSupplier",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_InvWarehouse_Branch Id",
                table: "InvWarehouse",
                column: "Branch Id");

            migrationBuilder.CreateIndex(
                name: "IX_InvWarehouse_Name",
                table: "InvWarehouse",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_InvWarehouse_TenantId",
                table: "InvWarehouse",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvStoreItems");

            migrationBuilder.DropTable(
                name: "InvSupplier");

            migrationBuilder.DropTable(
                name: "tbl_Branch");

            migrationBuilder.DropTable(
                name: "tbl_Tenant");

            migrationBuilder.DropTable(
                name: "InvStockItems");

            migrationBuilder.DropTable(
                name: "InvWarehouse");

            migrationBuilder.DropTable(
                name: "InvCategory");
        }
    }
}
