using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class userrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_Role_UserRolesId",
                table: "RoleUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_User_UserRolesId1",
                table: "RoleUser");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.RenameColumn(
                name: "UserRolesId1",
                table: "RoleUser",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "UserRolesId",
                table: "RoleUser",
                newName: "RolesId");

            migrationBuilder.RenameIndex(
                name: "IX_RoleUser_UserRolesId1",
                table: "RoleUser",
                newName: "IX_RoleUser_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_Role_RolesId",
                table: "RoleUser",
                column: "RolesId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_User_UsersId",
                table: "RoleUser",
                column: "UsersId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_Role_RolesId",
                table: "RoleUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_User_UsersId",
                table: "RoleUser");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "RoleUser",
                newName: "UserRolesId1");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "RoleUser",
                newName: "UserRolesId");

            migrationBuilder.RenameIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                newName: "IX_RoleUser_UserRolesId1");

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_Role_UserRolesId",
                table: "RoleUser",
                column: "UserRolesId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_User_UserRolesId1",
                table: "RoleUser",
                column: "UserRolesId1",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
