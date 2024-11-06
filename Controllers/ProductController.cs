using Microsoft.AspNetCore.Mvc;
using UserAuthApp_MVC.Models;

namespace UserAuthApp_MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly UserAuthDbContext _context;

        public ProductController(UserAuthDbContext context)
        {
            _context = context;
        }

        // GET: Product/Index
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        // GET: /Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Product/Create
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Sản phẩm đã được thêm thành công!";
                return RedirectToAction("Index");
            }
            return View(product);
        }


        // GET: /Product/Edit/{id}
        public IActionResult Edit(int id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");

            // Kiểm tra nếu người dùng có vai trò ADMIN
            if (userRole == "ADMIN")
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    TempData["ErrorMessage"] = "Sản phẩm không tồn tại!";
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            else
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập trang này.";
                return RedirectToAction("Index");
            }
        }

        // POST: /Product/Edit/{id}
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            var userRole = HttpContext.Session.GetString("UserRole");

            // Kiểm tra nếu người dùng có vai trò ADMIN
            if (userRole == "ADMIN")
            {
                if (ModelState.IsValid)
                {
                    _context.Products.Update(product);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Sản phẩm đã được chỉnh sửa!";
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            else
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập trang này.";
                return RedirectToAction("Index");
            }
        }

        // GET: /Product/Delete/{id}
        public IActionResult Delete(int id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");

            // Kiểm tra nếu người dùng có vai trò ADMIN
            if (userRole == "ADMIN")
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    TempData["ErrorMessage"] = "Sản phẩm không tồn tại!";
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            else
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập trang này.";
                return RedirectToAction("Index");
            }
        }

        // POST: /Product/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] 
        public IActionResult DeleteConfirmed(int id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");

            // Kiểm tra nếu người dùng có vai trò ADMIN
            if (userRole == "ADMIN")
            {
                var product = _context.Products.Find(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Sản phẩm đã được xóa!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không tìm thấy sản phẩm để xóa!";
                }
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập trang này.";
                return RedirectToAction("Index");
            }
        }
    }
}
