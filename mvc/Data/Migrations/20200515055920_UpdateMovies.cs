using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Data.Migrations
{
    public partial class UpdateMovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Movie",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Movie",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
