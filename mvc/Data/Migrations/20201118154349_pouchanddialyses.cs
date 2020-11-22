using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Data.Migrations
{
    public partial class pouchanddialyses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pouch",
                columns: table => new
                {
                    PouchID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    UsagePerDay = table.Column<int>(nullable: false),
                    StockOut = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pouch", x => x.PouchID);
                });

            migrationBuilder.CreateTable(
                name: "Dialysis",
                columns: table => new
                {
                    DialysisID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PouchID = table.Column<int>(nullable: false),
                    DialysisDate = table.Column<DateTime>(nullable: false),
                    OutWeight = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dialysis", x => x.DialysisID);
                    table.ForeignKey(
                        name: "FK_Dialysis_Pouch_PouchID",
                        column: x => x.PouchID,
                        principalTable: "Pouch",
                        principalColumn: "PouchID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dialysis_PouchID",
                table: "Dialysis",
                column: "PouchID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dialysis");

            migrationBuilder.DropTable(
                name: "Pouch");
        }
    }
}
