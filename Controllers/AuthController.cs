using Microsoft.AspNetCore.Mvc;
using UserAuthApp_MVC.Models;
using BCrypt.Net;
using System;

namespace UserAuthApp_MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserAuthDbContext _context;

        public AuthController(UserAuthDbContext context)
        {
            _context = context;
        }

        // GET: /Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("UserRole", user.Role.ToString());

                TempData["SuccessMessage"] = "Đăng nhập thành công!";

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Tên người dùng hoặc mật khẩu không chính xác.");
            return View();
        }


        // GET: /Auth/Logout
        public IActionResult Logout()
        {
            // Xóa thông tin người dùng khỏi session
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("UserRole");

            TempData["SuccessMessage"] = "Đăng xuất thành công!";

            return RedirectToAction("Index", "Home");
        }


        // GET: /Auth/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Auth/Register
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "Tên người dùng đã tồn tại.");
                    return View(user);
                }

                user.Password = HashPassword(user.Password);
                user.CreatedAt = DateTime.UtcNow;

                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(user);
        }


        // Hàm mã hóa mật khẩu
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
