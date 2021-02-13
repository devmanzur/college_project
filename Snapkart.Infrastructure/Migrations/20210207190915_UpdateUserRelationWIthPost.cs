using Microsoft.EntityFrameworkCore.Migrations;

namespace Snapkart.Infrastructure.Migrations
{
    public partial class UpdateUserRelationWIthPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "SnapQueries",
                newName: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SnapQueries_AppUserId",
                table: "SnapQueries",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SnapQueries_AspNetUsers_AppUserId",
                table: "SnapQueries",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SnapQueries_AspNetUsers_AppUserId",
                table: "SnapQueries");

            migrationBuilder.DropIndex(
                name: "IX_SnapQueries_AppUserId",
                table: "SnapQueries");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "SnapQueries",
                newName: "CreatedBy");
        }
    }
}
