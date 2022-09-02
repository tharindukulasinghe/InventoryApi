using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApi.Migrations
{
    public partial class AddView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE OR ALTER VIEW dbo.View_Summary AS " +
                "SELECT " +
                "dbo.Items.ItemCode, " +
                "COUNT(dbo.Repairs.Id) AS Count, " +
                "SUM(dbo.Repairs.Fee) AS TotalCharge, " +
                "MAX(dbo.Items.ItemName) AS ItemName " +
                "FROM dbo.Repairs " +
                "LEFT OUTER JOIN " +
                "dbo.Items ON dbo.Items.Id = dbo.Repairs.ItemId " +
                "WHERE (dbo.Repairs.Status IN (1, 2, 3)) " +
                "GROUP BY dbo.Items.ItemCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW dbo.View_Summary;");
        }
    }
}
