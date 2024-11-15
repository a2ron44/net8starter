using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Net8StarterAuthApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "admin_roles",
                columns: new[] { "name", "description" },
                values: new object[,]
                {
                    { "support", "Internal Support" },
                    { "sysadmin", "System Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admin_roles",
                keyColumn: "name",
                keyValue: "support");

            migrationBuilder.DeleteData(
                table: "admin_roles",
                keyColumn: "name",
                keyValue: "sysadmin");
        }
    }
}
