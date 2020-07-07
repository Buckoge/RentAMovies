using Microsoft.EntityFrameworkCore.Migrations;

namespace RentAMovies.Migrations
{
    public partial class RemoveInMovileModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Movies_MoviesId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_MoviesId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MoviesId",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Movies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MovieId",
                table: "Movies",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Movies_MovieId",
                table: "Movies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Movies_MovieId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_MovieId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "MoviesId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MoviesId",
                table: "Movies",
                column: "MoviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Movies_MoviesId",
                table: "Movies",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
