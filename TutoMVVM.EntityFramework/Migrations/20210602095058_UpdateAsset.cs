using Microsoft.EntityFrameworkCore.Migrations;

namespace TutoMVVM.EntityFramework.Migrations
{
    public partial class UpdateAsset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountHolder",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "Stock_Symbol",
                table: "AssetTransactions",
                newName: "Asset_Symbol");

            migrationBuilder.RenameColumn(
                name: "Stock_PricePerShare",
                table: "AssetTransactions",
                newName: "Asset_PricePerShare");

            migrationBuilder.AddColumn<int>(
                name: "AccountHolderId",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountHolderId",
                table: "Accounts",
                column: "AccountHolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_AccountHolderId",
                table: "Accounts",
                column: "AccountHolderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_AccountHolderId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountHolderId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AccountHolderId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "Asset_Symbol",
                table: "AssetTransactions",
                newName: "Stock_Symbol");

            migrationBuilder.RenameColumn(
                name: "Asset_PricePerShare",
                table: "AssetTransactions",
                newName: "Stock_PricePerShare");

            migrationBuilder.AddColumn<string>(
                name: "AccountHolder",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
