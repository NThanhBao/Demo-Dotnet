using Microsoft.EntityFrameworkCore;
using UserAuthApp_MVC.Models;

var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ DbContext và kết nối với cơ sở dữ liệu
builder.Services.AddDbContext<UserAuthDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21))));

// Thêm dịch vụ điều khiển và trang
builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
