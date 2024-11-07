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
                    v => v.ToString(), 
                    v => (UserRole)Enum.Parse(typeof(UserRole), v));
        }
    }
}
