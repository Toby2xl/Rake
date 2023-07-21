using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUseCollationToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InvWarehouse_Name",
                table: "InvWarehouse");

            migrationBuilder.DropIndex(
                name: "IX_InvSupplier_Name",
                table: "InvSupplier");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InvWarehouse",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                collation: "case-insensitive",
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InvSupplier",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                collation: "case-insensitive",
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InvStockItems",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                collation: "case-insensitive",
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.CreateIndex(
                name: "IX_InvWarehouse_Name",
                table: "InvWarehouse",
                column: "Name")
                .Annotation("Relational:Collation", new[] { "case-insensitive" });

            migrationBuilder.CreateIndex(
                name: "IX_InvSupplier_Name",
                table: "InvSupplier",
                column: "Name")
                .Annotation("Relational:Collation", new[] { "case-insensitive" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InvWarehouse_Name",
                table: "InvWarehouse");

            migrationBuilder.DropIndex(
                name: "IX_InvSupplier_Name",
                table: "InvSupplier");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InvWarehouse",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150,
                oldCollation: "case-insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InvSupplier",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250,
                oldCollation: "case-insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InvStockItems",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150,
                oldCollation: "case-insensitive");

            migrationBuilder.CreateIndex(
                name: "IX_InvWarehouse_Name",
                table: "InvWarehouse",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_InvSupplier_Name",
                table: "InvSupplier",
                column: "Name");
        }
    }
}
