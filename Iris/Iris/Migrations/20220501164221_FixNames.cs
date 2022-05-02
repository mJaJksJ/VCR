using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable 1591
namespace Iris.Migrations
{
    public partial class FixNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Letters_Accounts_AccountId",
                table: "Letters");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Letters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Letters_Accounts_AccountId",
                table: "Letters",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Letters_Accounts_AccountId",
                table: "Letters");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Letters",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Letters_Accounts_AccountId",
                table: "Letters",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
