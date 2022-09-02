using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApi.Migrations
{
    public partial class AddFee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "Repairs",
                newName: "Issue");

            migrationBuilder.AddColumn<double>(
                name: "Fee",
                table: "Repairs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fee",
                table: "Repairs");

            migrationBuilder.RenameColumn(
                name: "Issue",
                table: "Repairs",
                newName: "Reason");
        }
    }
}
