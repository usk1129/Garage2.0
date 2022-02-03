using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage2._0.Migrations
{
    public partial class NewSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
      table: "ParkVehicle",
      columns: new[] { "Id", "VehicleType", "ParkingSlot", "RegNumber", "Color", "Brand", "Model", "Wheels", "CheckInTime" },
      values: new object[,]
      {
                    { 1, 0, "1,2,3", "XAM931", "SILVER", "TOYOTA","RAV4", 4,  new DateTime(2022, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified)},
                    { 2, 1, "4", "ABC123", "BLACK", "YAMAHA", "Dirtbike", 2,  new DateTime(2022, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified)},
                    { 3, 2, "5", "XXX321", "BLUE", "VOLVO", "City", 4,  new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified)},
                    { 4, 4, "6,7", "DEX547", "BROWN", "KENWORTH","Farm", 4,  new DateTime(2022, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified)},
                    { 5, 3, "8", "OEF759", "WHITE", "FORD", "Offroad", 4,  new DateTime(2022, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified)},

      });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
            table: "ParkVehicle",
            keyColumn: "Id",
            keyValue: 1);

            migrationBuilder.DeleteData(
            table: "ParkVehicle",
            keyColumn: "Id",
            keyValue: 2);

            migrationBuilder.DeleteData(
            table: "ParkVehicle",
            keyColumn: "Id",
            keyValue: 3);

            migrationBuilder.DeleteData(
            table: "ParkVehicle",
            keyColumn: "Id",
            keyValue: 4);

            migrationBuilder.DeleteData(
            table: "ParkVehicle",
            keyColumn: "Id",
            keyValue: 5);

        }
    }
}
