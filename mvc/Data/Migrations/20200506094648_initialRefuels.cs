using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Data.Migrations
{
    public partial class initialRefuels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Refuel",
                columns: table => new
                {
                    RefuelID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleID = table.Column<int>(nullable: false),
                    RefuelDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refuel", x => x.RefuelID);
                    table.ForeignKey(
                        name: "FK_Refuel_Vehicle_VehicleID",
                        column: x => x.VehicleID,
                        principalTable: "Vehicle",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Refuel_VehicleID",
                table: "Refuel",
                column: "VehicleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Refuel");
        }
    }
}
