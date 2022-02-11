using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shaper.DataAccess.Migrations
{
    public partial class addingUnitPrices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "OrderProducts",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "CartProducts",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "CartProducts");
        }
    }
}
