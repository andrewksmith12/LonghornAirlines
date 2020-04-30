using Microsoft.EntityFrameworkCore.Migrations;

namespace LonghornAirlines.Migrations
{
    public partial class adjResModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumPassengers",
                table: "Reservations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumPassengers",
                table: "Reservations");
        }
    }
}
