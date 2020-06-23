using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertApi.Migrations
{
    public partial class AdvertApiMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    IdAdvertisement = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6, 2)", nullable: false),
                    IdCampaing = table.Column<int>(nullable: false),
                    Area = table.Column<decimal>(type: "decimal(6, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.IdAdvertisement);
                });

            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    IdBuilding = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(maxLength: 100, nullable: true),
                    StreetNumber = table.Column<int>(nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    Height = table.Column<decimal>(type: "decimal(6, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.IdBuilding);
                });

            migrationBuilder.CreateTable(
                name: "Campaign",
                columns: table => new
                {
                    IdCampaign = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClient = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    PricePerSquareMeter = table.Column<decimal>(type: "decimal(6, 2)", nullable: false),
                    FromIdBuilding = table.Column<int>(nullable: false),
                    ToldBuilding = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaign", x => x.IdCampaign);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    IdClient = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Phone = table.Column<string>(maxLength: 100, nullable: false),
                    Login = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    Salt = table.Column<string>(nullable: true),
                    RefreshToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.IdClient);
                });

            migrationBuilder.InsertData(
                table: "Banner",
                columns: new[] { "IdAdvertisement", "Area", "IdCampaing", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1000.0m, 1, 1, 100.99m },
                    { 2, 500.0m, 2, 2, 2000.5m },
                    { 3, 100.0m, 3, 3, 30000.0m },
                    { 4, 2500.0m, 4, 4, 40000.5m }
                });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "IdBuilding", "City", "Height", "Street", "StreetNumber" },
                values: new object[,]
                {
                    { 1, "Warszawa", 50.5m, "A", 1 },
                    { 2, "Warszawa", 30.0m, "B", 2 },
                    { 3, "Warszawa", 70.5m, "C", 3 },
                    { 4, "Warszawa", 34.5m, "D", 4 }
                });

            migrationBuilder.InsertData(
                table: "Campaign",
                columns: new[] { "IdCampaign", "EndDate", "FromIdBuilding", "IdClient", "PricePerSquareMeter", "StartDate", "ToldBuilding" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 5000.0m, new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 2, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 10000.5m, new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 3, new DateTime(2020, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 20000.0m, new DateTime(2020, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 4, new DateTime(2021, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, 100000.99m, new DateTime(2020, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "IdClient", "Email", "FirstName", "LastName", "Login", "Password", "Phone", "RefreshToken", "Salt" },
                values: new object[,]
                {
                    { 1, "Jan@maiil.com", "Jan", "A", "JanA", "abbbb", "100 200 300", null, null },
                    { 2, "Adam@maiil.com", "Adam", "B", "Adam1", "abssss", "200 300 400", null, null },
                    { 3, "Anna@maiil.com", "Anna", "C", "Anna1", "asdadabbb", "900 888 111", null, null },
                    { 4, "Karol@maiil.com", "Karol", "D", "Karol", "abadsadxxbbb", "000 333 444", null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Campaign");

            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
