using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoMarketplace.Data.Migrations
{
    public partial class AddTankVolumeToCarMOdel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TankVolume",
                table: "CarModels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TankVolume",
                table: "CarModels");
        }
    }
}
