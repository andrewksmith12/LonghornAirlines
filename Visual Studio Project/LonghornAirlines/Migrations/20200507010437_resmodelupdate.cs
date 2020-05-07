using Microsoft.EntityFrameworkCore.Migrations;

namespace LonghornAirlines.Migrations
{
    public partial class resmodelupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UpgradeWithMilage",
                table: "Tickets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "ReservationMethod",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MilesPaid",
                table: "Reservations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpgradeWithMilage",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "MilesPaid",
                table: "Reservations");

            migrationBuilder.AlterColumn<string>(
                name: "ReservationMethod",
                table: "Reservations",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
