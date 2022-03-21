using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Iris.Migrations
{
    public partial class AddIsPrivateFieldToMailServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "MailServers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "MailServers");
        }
    }
}
