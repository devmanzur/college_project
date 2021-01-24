using Microsoft.EntityFrameworkCore.Migrations;

namespace Snapkart.Infrastructure.Migrations
{
    public partial class UpdatedBidModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bid_SnapQueries_SnapQueryId",
                table: "Bid");

            migrationBuilder.AlterColumn<int>(
                name: "SnapQueryId",
                table: "Bid",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Bid",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bid_UserId",
                table: "Bid",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_AspNetUsers_UserId",
                table: "Bid",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_SnapQueries_SnapQueryId",
                table: "Bid",
                column: "SnapQueryId",
                principalTable: "SnapQueries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bid_AspNetUsers_UserId",
                table: "Bid");

            migrationBuilder.DropForeignKey(
                name: "FK_Bid_SnapQueries_SnapQueryId",
                table: "Bid");

            migrationBuilder.DropIndex(
                name: "IX_Bid_UserId",
                table: "Bid");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bid");

            migrationBuilder.AlterColumn<int>(
                name: "SnapQueryId",
                table: "Bid",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_SnapQueries_SnapQueryId",
                table: "Bid",
                column: "SnapQueryId",
                principalTable: "SnapQueries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
