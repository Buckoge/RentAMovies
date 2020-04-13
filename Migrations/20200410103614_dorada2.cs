using Microsoft.EntityFrameworkCore.Migrations;

namespace RentAMovies.Migrations
{
    public partial class dorada2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RentalId",
                table: "Movies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_RentalId",
                table: "Movies",
                column: "RentalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Rentals_RentalId",
                table: "Movies",
                column: "RentalId",
                principalTable: "Rentals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Rentals_RentalId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_RentalId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "RentalId",
                table: "Movies");
        }
    }
}
