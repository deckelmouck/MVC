using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Data.Migrations
{
    public partial class addDeliveryAndDeliveryPouches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    DeliveryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    LastOrderDate = table.Column<DateTime>(nullable: false),
                    Delivered = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery", x => x.DeliveryID);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryPouch",
                columns: table => new
                {
                    DeliveryPouchID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryID = table.Column<int>(nullable: false),
                    PouchID = table.Column<int>(nullable: false),
                    OrderQuantity = table.Column<int>(nullable: false),
                    Delivered = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryPouch", x => x.DeliveryPouchID);
                    table.ForeignKey(
                        name: "FK_DeliveryPouch_Delivery_DeliveryID",
                        column: x => x.DeliveryID,
                        principalTable: "Delivery",
                        principalColumn: "DeliveryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryPouch_Pouch_PouchID",
                        column: x => x.PouchID,
                        principalTable: "Pouch",
                        principalColumn: "PouchID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryPouch_DeliveryID",
                table: "DeliveryPouch",
                column: "DeliveryID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryPouch_PouchID",
                table: "DeliveryPouch",
                column: "PouchID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryPouch");

            migrationBuilder.DropTable(
                name: "Delivery");
        }
    }
}
