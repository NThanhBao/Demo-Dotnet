using Microsoft.EntityFrameworkCore.Migrations;
using UserAuthApp_MVC.Models;
#nullable disable

namespace UserAuthApp_MVC.Migrations
{
    /// <inheritdoc />
    public partial class SeedTwoAdminUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Thêm tài khoản admin1
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "Password", "Email", "Phone", "CreatedAt", "Role" },
                values: new object[] { "admin1", HashPassword("admin"), "admin1@example.com", "0123456789", DateTime.UtcNow, UserRole.ADMIN.ToString() });

            // Thêm tài khoản admin2
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "Password", "Email", "Phone", "CreatedAt", "Role" },
                values: new object[] { "admin2", HashPassword("admin"), "admin2@example.com", "0987654321", DateTime.UtcNow, UserRole.ADMIN.ToString() });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Xóa tài khoản admin1
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin1");

            // Xóa tài khoản admin2
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin2");
        }

        // Phương thức để băm mật khẩu
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
