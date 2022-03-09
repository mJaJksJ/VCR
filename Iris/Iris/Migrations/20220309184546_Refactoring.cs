using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable 1591
namespace Iris.Migrations
{

    public partial class Refactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ip",
                table: "MailServers",
                newName: "Host");

            migrationBuilder.AddColumn<string>(
                name: "ConnectionProtocol",
                table: "Accounts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UseSsl",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionProtocol",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "UseSsl",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "Host",
                table: "MailServers",
                newName: "Ip");
        }
    }
}
