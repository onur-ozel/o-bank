using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Customer.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<decimal>(
                name: "CustomerNoSequence",
                startValue: 100000L);

            migrationBuilder.CreateTable(
                name: "CorporateCustomer",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CustomerNo = table.Column<decimal>(nullable: false, defaultValueSql: "NEXT VALUE FOR CustomerNumbers"),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    TaxId = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorporateCustomer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RetailCustomer",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CustomerNo = table.Column<decimal>(nullable: false, defaultValueSql: "NEXT VALUE FOR CustomerNoSequence"),
                    NationalId = table.Column<decimal>(maxLength: 11, nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    Nationality = table.Column<string>(maxLength: 2, nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Gender = table.Column<string>(maxLength: 1, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUser = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailCustomer", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorporateCustomer");

            migrationBuilder.DropTable(
                name: "RetailCustomer");

            migrationBuilder.DropSequence(
                name: "CustomerNoSequence");
        }
    }
}
