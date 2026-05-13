using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Orders",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                CityFrom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                AdressFrom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                CityTo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                AdressTo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                Weight = table.Column<double>(type: "double precision", nullable: false),
                PickupDate = table.Column<DateOnly>(type: "date", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Orders", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Orders");
    }
}
