using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentAMovies.Migrations
{
    public partial class Videoklub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalTest");

            migrationBuilder.CreateTable(
                name: "VideoKlub",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    MovieId = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    DateRented = table.Column<DateTime>(nullable: false),
                    DateReturned = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoKlub", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoKlub_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_VideoKlub_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideoKlub_CustomerId",
                table: "VideoKlub",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoKlub_MovieId",
                table: "VideoKlub",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideoKlub");

            migrationBuilder.CreateTable(
                name: "RentalTest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateRented = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateReturned = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalTest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalTest_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_RentalTest_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentalTest_CustomerId",
                table: "RentalTest",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalTest_MovieId",
                table: "RentalTest",
                column: "MovieId");
        }
    }
}
