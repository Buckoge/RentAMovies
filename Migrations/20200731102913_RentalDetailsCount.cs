using Microsoft.EntityFrameworkCore.Migrations;

namespace RentAMovies.Migrations
{
    public partial class RentalDetailsCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "RentalDetails",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "RentalDetails");
        }
    }
}
