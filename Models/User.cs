using System;

namespace UserAuthApp_MVC.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public UserRole Role { get; set; } = UserRole.USER;
    }
}
