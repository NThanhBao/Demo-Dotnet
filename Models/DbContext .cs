using Microsoft.EntityFrameworkCore;
using System;

namespace UserAuthApp_MVC.Models
{
    public class UserAuthDbContext : DbContext
    {
        public UserAuthDbContext(DbContextOptions<UserAuthDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion(
                    v => v.ToString(), // Chuyển enum thành chuỗi trước khi lưu vào cơ sở dữ liệu
                    v => (UserRole)Enum.Parse(typeof(UserRole), v)); // Chuyển chuỗi thành enum khi truy xuất
        }
    }
}
