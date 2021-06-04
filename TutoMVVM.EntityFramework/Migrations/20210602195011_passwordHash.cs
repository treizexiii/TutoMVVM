using Microsoft.EntityFrameworkCore.Migrations;

namespace TutoMVVM.EntityFramework.Migrations
{
    public partial class passwordHash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "ShareAmount",
                table: "AssetTransactions",
                newName: "Shares");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Shares",
                table: "AssetTransactions",
                newName: "ShareAmount");
        }
    }
}
