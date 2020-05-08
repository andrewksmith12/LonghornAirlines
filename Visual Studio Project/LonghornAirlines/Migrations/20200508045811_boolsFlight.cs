using Microsoft.EntityFrameworkCore.Migrations;

namespace LonghornAirlines.Migrations
{
    public partial class boolsFlight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AttendantCheckIn",
                table: "Flights",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CoPilotCheckIn",
                table: "Flights",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PilotCheckIn",
                table: "Flights",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendantCheckIn",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "CoPilotCheckIn",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "PilotCheckIn",
                table: "Flights");
        }
    }
}
