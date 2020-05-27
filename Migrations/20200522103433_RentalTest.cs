using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentAMovies.Migrations
{
    public partial class RentalTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RentalTest",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalTest");
        }
    }
}
