using Microsoft.EntityFrameworkCore.Migrations;

namespace RentAMovies.Migrations
{
    public partial class rental2777 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Rentals_RentalId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_RentalId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "RentalId",
                table: "Rentals");

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

            migrationBuilder.AddColumn<int>(
                name: "RentalId",
                table: "Rentals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RentalId",
                table: "Rentals",
                column: "RentalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Rentals_RentalId",
                table: "Rentals",
                column: "RentalId",
                principalTable: "Rentals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
