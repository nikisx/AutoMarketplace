using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoMarketplace.Data.Migrations
{
    public partial class AddPropertiesForCarModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BodyType",
                table: "CarModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CombinedFuelConsumptionPer100km",
                table: "CarModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Engine",
                table: "CarModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FuelType",
                table: "CarModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InTownFuelConsumptionPer100km",
                table: "CarModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDoors",
                table: "CarModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OutOfTownFuelConsumptionPer100km",
                table: "CarModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartYearOfProduction",
                table: "CarModels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BodyType",
                table: "CarModels");

            migrationBuilder.DropColumn(
                name: "CombinedFuelConsumptionPer100km",
                table: "CarModels");

            migrationBuilder.DropColumn(
                name: "Engine",
                table: "CarModels");

            migrationBuilder.DropColumn(
                name: "FuelType",
                table: "CarModels");

            migrationBuilder.DropColumn(
                name: "InTownFuelConsumptionPer100km",
                table: "CarModels");

            migrationBuilder.DropColumn(
                name: "NumberOfDoors",
                table: "CarModels");

            migrationBuilder.DropColumn(
                name: "OutOfTownFuelConsumptionPer100km",
                table: "CarModels");

            migrationBuilder.DropColumn(
                name: "StartYearOfProduction",
                table: "CarModels");
        }
    }
}
