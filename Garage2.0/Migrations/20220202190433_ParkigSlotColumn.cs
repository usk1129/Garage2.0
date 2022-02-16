using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage2._0.Migrations
{
    public partial class ParkigSlotColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParkingSlot",
                table: "ParkVehicle",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkingSlot",
                table: "ParkVehicle");
        }
    }
}
