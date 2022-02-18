using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage2._0.Migrations
{
    public partial class ParkingSpot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParkingSpot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParkVehicleParkingSpot",
                columns: table => new
                {
                    ParkVehiclesId = table.Column<int>(type: "int", nullable: false),
                    ParkingSpotsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkVehicleParkingSpot", x => new { x.ParkVehiclesId, x.ParkingSpotsId });
                    table.ForeignKey(
                        name: "FK_ParkVehicleParkingSpot_ParkingSpot_ParkingSpotsId",
                        column: x => x.ParkingSpotsId,
                        principalTable: "ParkingSpot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParkVehicleParkingSpot_ParkVehicle_ParkVehiclesId",
                        column: x => x.ParkVehiclesId,
                        principalTable: "ParkVehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkVehicleParkingSpot_ParkingSpotsId",
                table: "ParkVehicleParkingSpot",
                column: "ParkingSpotsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkVehicleParkingSpot");

            migrationBuilder.DropTable(
                name: "ParkingSpot");
        }
    }
}
