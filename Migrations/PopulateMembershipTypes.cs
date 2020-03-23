using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace RentAMovies.Migrations
{
    

    //     Sql("INSTER INTO MembershipTypes (Id, SignUpFee, DurationInMonths, DiscountRate) VALUES (1,0,0,0)");
    //     Sql("INSTER INTO MembershipTypes (Id, SignUpFee, DurationInMonths, DiscountRate) VALUES (2,30,1,10)");
    //    Sql("INSTER INTO MembershipTypes (Id, SignUpFee, DurationInMonths, DiscountRate) VALUES (3,90,3,15)");
    //     Sql("INSTER INTO MembershipTypes (Id, SignUpFee, DurationInMonths, DiscountRate) VALUES (4,300,12,20)");

    public partial class PopulateMembershipTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MembershipTypes",
                columns: new[] { "Id", "SignUpFee", "DurationInMonths", "DiscountRate" },
                values: new object[] { 1, 0, 0, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}


