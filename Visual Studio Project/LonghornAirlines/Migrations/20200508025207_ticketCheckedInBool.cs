using Microsoft.EntityFrameworkCore.Migrations;

namespace LonghornAirlines.Migrations
{
    public partial class ticketCheckedInBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CheckedIn",
                table: "Tickets",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckedIn",
                table: "Tickets");
        }
    }
}
