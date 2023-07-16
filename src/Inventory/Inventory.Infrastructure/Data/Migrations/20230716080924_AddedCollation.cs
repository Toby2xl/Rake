using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCollation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_Tenant",
                table: "tbl_Tenant");

            migrationBuilder.DropPrimaryKey(
                name: "tbl_Branch_pkey",
                table: "tbl_Branch");

            migrationBuilder.RenameTable(
                name: "tbl_Tenant",
                newName: "Tenants");

            migrationBuilder.RenameTable(
                name: "tbl_Branch",
                newName: "Branch");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:CollationDefinition:case-insensitive", "en-us-ks-primary,en-us-ks-primary,icu,False");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InvCategory",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                collation: "case-insensitive",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "TenantName",
                table: "Tenants",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                collation: "case-insensitive",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "Branch_pkey",
                table: "Branch",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_TenantName",
                table: "Tenants",
                column: "TenantName")
                .Annotation("Relational:Collation", new[] { "case-insensitive" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_TenantName",
                table: "Tenants");

            migrationBuilder.DropPrimaryKey(
                name: "Branch_pkey",
                table: "Branch");

            migrationBuilder.RenameTable(
                name: "Tenants",
                newName: "tbl_Tenant");

            migrationBuilder.RenameTable(
                name: "Branch",
                newName: "tbl_Branch");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:CollationDefinition:case-insensitive", "en-us-ks-primary,en-us-ks-primary,icu,False");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InvCategory",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldCollation: "case-insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "TenantName",
                table: "tbl_Tenant",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldCollation: "case-insensitive");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_Tenant",
                table: "tbl_Tenant",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "tbl_Branch_pkey",
                table: "tbl_Branch",
                column: "BranchID");
        }
    }
}
