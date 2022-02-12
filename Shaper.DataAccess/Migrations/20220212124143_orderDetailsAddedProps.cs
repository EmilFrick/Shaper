using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shaper.DataAccess.Migrations
{
    public partial class orderDetailsAddedProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "OrderDetails",
                newName: "ProductUnitPrice");

            migrationBuilder.AddColumn<string>(
                name: "ColorHex",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ColorName",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "OrderTotalValue",
                table: "OrderDetails",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "ShapeHasFrame",
                table: "OrderDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ShapeName",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TransparencyDescription",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TransparencyName",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TransparencyValue",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorHex",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ColorName",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "OrderTotalValue",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ShapeHasFrame",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ShapeName",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "TransparencyDescription",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "TransparencyName",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "TransparencyValue",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "ProductUnitPrice",
                table: "OrderDetails",
                newName: "UnitPrice");
        }
    }
}
