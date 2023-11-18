using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArduinoWebApp.Acsses.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbLdrSensorReadings",
                columns: table => new
                {
                    LdrSensorReadingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LdrSensorReadingAvratgeValue = table.Column<float>(type: "real", nullable: false),
                    LdrSensorReadingTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbLdrSensorReadings", x => x.LdrSensorReadingID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbLdrSensorReadings");
        }
    }
}
