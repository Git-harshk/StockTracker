using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockTracker.Migrations
{
    /// <inheritdoc />
    public partial class portfolioandtransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_Users_userId",
                table: "Portfolios");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Portfolios",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Portfolios_userId",
                table: "Portfolios",
                newName: "IX_Portfolios_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolios_Users_UserId",
                table: "Portfolios",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_Users_UserId",
                table: "Portfolios");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Portfolios",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Portfolios_UserId",
                table: "Portfolios",
                newName: "IX_Portfolios_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolios_Users_userId",
                table: "Portfolios",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
