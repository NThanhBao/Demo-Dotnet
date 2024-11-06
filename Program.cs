using Microsoft.EntityFrameworkCore;
using UserAuthApp_MVC.Models;

var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ DbContext và kết nối với cơ sở dữ liệu
builder.Services.AddDbContext<UserAuthDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21)))); // Thay đổi phiên bản MySQL nếu cần

// Thêm dịch vụ điều khiển và trang
builder.Services.AddControllersWithViews();

// Thêm dịch vụ xác thực
builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/Auth/Login"; // Đường dẫn đăng nhập
        options.LogoutPath = "/Auth/Logout"; // Đường dẫn đăng xuất
    });

// Thêm dịch vụ phân quyền
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("ADMIN")); // Đảm bảo chỉ ADMIN mới có thể thực hiện các hành động này
});

// Thêm dịch vụ Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Cấu hình pipeline yêu cầu HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
