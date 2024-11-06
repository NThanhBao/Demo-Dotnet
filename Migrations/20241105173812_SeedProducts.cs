using Microsoft.EntityFrameworkCore.Migrations;

namespace UserAuthApp_MVC.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "Description", "Quantity", "CreatedAt" },
                values: new object[,]
                {
                    { 1, "Sản phẩm A", 100.00m, "Mô tả sản phẩm A", 10, DateTime.UtcNow },
                    { 2, "Sản phẩm B", 200.00m, "Mô tả sản phẩm B", 20, DateTime.UtcNow },
                    { 3, "Sản phẩm C", 150.00m, "Mô tả sản phẩm C", 15, DateTime.UtcNow }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3 });
        }
    }
}
