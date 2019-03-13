using Microsoft.EntityFrameworkCore.Migrations;

namespace Customer.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "CustomerNumbers",
                startValue: 100000L);

            migrationBuilder.CreateTable(
                name: "CorporateCustomer",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    No = table.Column<long>(nullable: false, defaultValueSql: "NEXT VALUE FOR CustomerNumbers"),
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
                    No = table.Column<long>(nullable: false, defaultValueSql: "NEXT VALUE FOR CustomerNumbers"),
                    FirstName = table.Column<string>(maxLength: 150, nullable: false),
                    LastName = table.Column<string>(maxLength: 150, nullable: false),
                    NationalId = table.Column<decimal>(nullable: false)
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
                name: "CustomerNumbers");
        }
    }
}
