using Microsoft.EntityFrameworkCore.Migrations;

namespace Snapkart.Infrastructure.Migrations
{
    public partial class PostCreatedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SnapQueries",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SnapQueries");
        }
    }
}
