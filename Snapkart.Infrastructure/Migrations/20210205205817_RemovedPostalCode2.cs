using Microsoft.EntityFrameworkCore.Migrations;

namespace Snapkart.Infrastructure.Migrations
{
    public partial class RemovedPostalCode2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Area");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostalCode",
                table: "Area",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}