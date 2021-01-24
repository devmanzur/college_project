using Microsoft.EntityFrameworkCore.Migrations;

namespace Snapkart.Infrastructure.Migrations
{
    public partial class UpdatedNotificationRecipientrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppNotification_AspNetUsers_RecipientId1",
                table: "AppNotification");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "AppNotification");

            migrationBuilder.RenameColumn(
                name: "RecipientId1",
                table: "AppNotification",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AppNotification_RecipientId1",
                table: "AppNotification",
                newName: "IX_AppNotification_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppNotification_AspNetUsers_UserId",
                table: "AppNotification",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppNotification_AspNetUsers_UserId",
                table: "AppNotification");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AppNotification",
                newName: "RecipientId1");

            migrationBuilder.RenameIndex(
                name: "IX_AppNotification_UserId",
                table: "AppNotification",
                newName: "IX_AppNotification_RecipientId1");

            migrationBuilder.AddColumn<long>(
                name: "RecipientId",
                table: "AppNotification",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_AppNotification_AspNetUsers_RecipientId1",
                table: "AppNotification",
                column: "RecipientId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
