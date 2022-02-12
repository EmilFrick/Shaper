using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shaper.DataAccess.Migrations
{
    public partial class ChangeNameInOderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderTotalValue",
                table: "OrderDetails",
                newName: "EntryTotalValue");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntryTotalValue",
                table: "OrderDetails",
                newName: "OrderTotalValue");
        }
    }
}
