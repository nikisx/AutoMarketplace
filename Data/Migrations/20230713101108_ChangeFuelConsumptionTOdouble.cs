using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoMarketplace.Data.Migrations
{
    public partial class ChangeFuelConsumptionTOdouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "OutOfTownFuelConsumptionPer100km",
                table: "CarModels",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "InTownFuelConsumptionPer100km",
                table: "CarModels",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "CombinedFuelConsumptionPer100km",
                table: "CarModels",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OutOfTownFuelConsumptionPer100km",
                table: "CarModels",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "InTownFuelConsumptionPer100km",
                table: "CarModels",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "CombinedFuelConsumptionPer100km",
                table: "CarModels",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
