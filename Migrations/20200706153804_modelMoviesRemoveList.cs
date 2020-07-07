using Microsoft.EntityFrameworkCore.Migrations;

namespace RentAMovies.Migrations
{
    public partial class modelMoviesRemoveList : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
