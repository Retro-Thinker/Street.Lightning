using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Street.Lightning.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Illumination",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IlluminationProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Power = table.Column<double>(type: "float", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Illumination", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CityIlluminationDetails",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false),
                    IlluminationId = table.Column<int>(type: "int", nullable: false),
                    QuantityOfBulbs = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityIlluminationDetails", x => new { x.CityId, x.IlluminationId });
                    table.ForeignKey(
                        name: "FK_CityIlluminationDetails_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CityIlluminationDetails_Illumination_IlluminationId",
                        column: x => x.IlluminationId,
                        principalTable: "Illumination",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "CountryName", "DateCreated", "DateModified" },
                values: new object[] { 1, "Poland", new DateTime(2024, 4, 19, 15, 44, 46, 887, DateTimeKind.Utc).AddTicks(5840), null });

            migrationBuilder.InsertData(
                table: "Illumination",
                columns: new[] { "Id", "DateCreated", "DateModified", "IlluminationProvider", "Name", "Power" },
                values: new object[] { 1, new DateTime(2024, 4, 19, 15, 44, 46, 887, DateTimeKind.Utc).AddTicks(6270), null, "Tauron", "Eco Bulbs", 30.0 });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "CityName", "CountryId", "DateCreated", "DateModified", "Latitude", "Longitude" },
                values: new object[] { 1, "Wrocław", 1, new DateTime(2024, 4, 19, 15, 44, 46, 887, DateTimeKind.Utc).AddTicks(2660), null, 51.107883000000001, 17.038537999999999 });

            migrationBuilder.InsertData(
                table: "CityIlluminationDetails",
                columns: new[] { "CityId", "IlluminationId", "DateCreated", "DateModified", "QuantityOfBulbs" },
                values: new object[] { 1, 1, new DateTime(2024, 4, 19, 15, 44, 46, 887, DateTimeKind.Utc).AddTicks(5370), null, 250 });

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryId",
                table: "City",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CityIlluminationDetails_IlluminationId",
                table: "CityIlluminationDetails",
                column: "IlluminationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityIlluminationDetails");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Illumination");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
