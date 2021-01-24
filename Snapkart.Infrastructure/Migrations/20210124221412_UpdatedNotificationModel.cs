using Microsoft.EntityFrameworkCore.Migrations;

namespace Snapkart.Infrastructure.Migrations
{
    public partial class UpdatedNotificationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "AppNotification",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "AppNotification");
        }
    }
}
