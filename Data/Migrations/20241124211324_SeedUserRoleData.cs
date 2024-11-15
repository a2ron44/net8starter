using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Net8StarterAuthApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserRoleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "first_name", "last_name" },
                values: new object[] { new Guid("12d1ab10-aaa6-11ef-9084-ec8f097e5f62"), "a2ron44@gmail.com", "Aaron", "H" });
            
            migrationBuilder.Sql("INSERT INTO admin_role_user (admin_roles_name, users_id) VALUES ('SysAdmin', '12d1ab10-aaa6-11ef-9084-ec8f097e5f62')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("12d1ab10-aaa6-11ef-9084-ec8f097e5f62"));
        }
    }
}
