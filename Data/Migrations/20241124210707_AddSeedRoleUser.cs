using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Net8StarterAuthApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedRoleUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admin_role_user",
                columns: table => new
                {
                    admin_roles_name = table.Column<string>(type: "varchar(15)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    users_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_admin_role_user", x => new { x.admin_roles_name, x.users_id });
                    table.ForeignKey(
                        name: "fk_admin_role_user_admin_roles_admin_roles_name",
                        column: x => x.admin_roles_name,
                        principalTable: "admin_roles",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_admin_role_user_users_users_id",
                        column: x => x.users_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_admin_role_user_users_id",
                table: "admin_role_user",
                column: "users_id");
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin_role_user");
        }
    }
}
