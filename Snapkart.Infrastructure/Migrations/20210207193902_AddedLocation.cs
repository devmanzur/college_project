using Microsoft.EntityFrameworkCore.Migrations;

namespace Snapkart.Infrastructure.Migrations
{
    public partial class AddedLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "SnapQueries",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "SnapQueries");
        }
    }
}
