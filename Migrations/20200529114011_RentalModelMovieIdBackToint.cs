using Microsoft.EntityFrameworkCore.Migrations;

namespace RentAMovies.Migrations
{
    public partial class RentalModelMovieIdBackToint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Movies_MovieId1",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_MovieId1",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "MovieId1",
                table: "Rentals");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Rentals",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_MovieId",
                table: "Rentals",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Movies_MovieId",
                table: "Rentals",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Movies_MovieId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_MovieId",
                table: "Rentals");

            migrationBuilder.AlterColumn<double>(
                name: "MovieId",
                table: "Rentals",
                type: "double",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "MovieId1",
                table: "Rentals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_MovieId1",
                table: "Rentals",
                column: "MovieId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Movies_MovieId1",
                table: "Rentals",
                column: "MovieId1",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
