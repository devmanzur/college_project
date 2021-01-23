using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Snapkart.Infrastructure.Migrations
{
    public partial class ModeledSubsciptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_AspNetUsers_AppUserId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_AppUserId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Category");

            migrationBuilder.CreateTable(
                name: "UserSubscription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppUserId = table.Column<string>(type: "text", nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSubscription_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSubscription_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscription_AppUserId",
                table: "UserSubscription",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscription_CategoryId",
                table: "UserSubscription",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSubscription");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Category",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_AppUserId",
                table: "Category",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_AspNetUsers_AppUserId",
                table: "Category",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
