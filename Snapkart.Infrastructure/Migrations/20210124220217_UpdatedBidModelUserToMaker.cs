using Microsoft.EntityFrameworkCore.Migrations;

namespace Snapkart.Infrastructure.Migrations
{
    public partial class UpdatedBidModelUserToMaker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bid_AspNetUsers_UserId",
                table: "Bid");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Bid",
                newName: "MakerId");

            migrationBuilder.RenameIndex(
                name: "IX_Bid_UserId",
                table: "Bid",
                newName: "IX_Bid_MakerId");

            migrationBuilder.AddColumn<int>(
                name: "AcceptedBidId",
                table: "SnapQueries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_AspNetUsers_MakerId",
                table: "Bid",
                column: "MakerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bid_AspNetUsers_MakerId",
                table: "Bid");

            migrationBuilder.DropColumn(
                name: "AcceptedBidId",
                table: "SnapQueries");

            migrationBuilder.RenameColumn(
                name: "MakerId",
                table: "Bid",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bid_MakerId",
                table: "Bid",
                newName: "IX_Bid_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_AspNetUsers_UserId",
                table: "Bid",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
