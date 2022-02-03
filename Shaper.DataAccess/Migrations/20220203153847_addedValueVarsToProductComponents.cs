using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shaper.DataAccess.Migrations
{
    public partial class addedValueVarsToProductComponents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AddedValue",
                table: "Transparencies",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AddedValue",
                table: "Shapes",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AddedValue",
                table: "Colors",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedValue",
                table: "Transparencies");

            migrationBuilder.DropColumn(
                name: "AddedValue",
                table: "Shapes");

            migrationBuilder.DropColumn(
                name: "AddedValue",
                table: "Colors");
        }
    }
}
