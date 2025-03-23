using Microsoft.AspNetCore.Authentication.Cookies;
using PRN222_Beverage_Website_Project.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian session hết hạn
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Thêm dịch vụ Authentication với Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // Đường dẫn đến trang Login
        options.LogoutPath = "/Logout"; // Đường dẫn Logout
        options.AccessDeniedPath = "/AccessDenied"; // Trang từ chối truy cập
        options.ExpireTimeSpan = TimeSpan.FromDays(7); // Cookie có thời hạn 7 ngày
        options.SlidingExpiration = true; // Gia hạn Cookie nếu user vẫn hoạt động
    });

// Thêm Authorization
builder.Services.AddAuthorization();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped<IImageService, ImageService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Đảm bảo gọi UseRouting() trước Authentication và Authorization
app.UseRouting();

app.UseAuthentication();  // Middleware xử lý xác thực người dùng
app.UseAuthorization();   // Middleware kiểm tra quyền truy cập người dùng
app.UseSession();         // Nếu dùng session

app.MapControllers();  // Mapping các controllers

// Các route định nghĩa cho các controller và action
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/");
app.MapControllerRoute(name: "user", pattern: "user", defaults: new { controller = "Home", action = "HomeUser" });
app.MapControllerRoute(name: "sale", pattern: "sale", defaults: new { controller = "Home", action = "Sale" });
app.MapControllerRoute(name: "manager", pattern: "manager", defaults: new { controller = "Home", action = "Manager" });
app.MapControllerRoute(name: "admin", pattern: "admin", defaults: new { controller = "Home", action = "Admin" });
app.MapControllerRoute(name: "login", pattern: "login", defaults: new { controller = "User", action = "Login" });
app.MapControllerRoute(name: "logout", pattern: "logout", defaults: new { controller = "User", action = "Logout" });
app.MapControllerRoute(name: "AccessDenied", pattern: "AccessDenied", defaults: new { controller = "User", action = "AccessDenied" });
app.MapControllerRoute(name: "register", pattern: "register", defaults: new { controller = "User", action = "Register" });

//Route for Shop
app.MapControllerRoute(name: "CreateShop", pattern: "shop/create", defaults: new { controller = "Shop", action = "Create" });
app.MapControllerRoute(name: "UpdateShop", pattern: "shop/update/{shopId}", defaults: new { controller = "Shop", action = "Update" });
app.MapControllerRoute(name: "DeleteShop", pattern: "shop/delete/{shopId}", defaults: new { controller = "Shop", action = "Delete" });
app.MapControllerRoute(name: "ApproveShop", pattern: "shop/approve", defaults: new { controller = "Shop", action = "Approve" });
app.MapControllerRoute(name: "ShopDetail", pattern: "shop/detail/{shopId}", defaults: new { controller = "Shop", action = "Detail" });

//Route for Product
app.MapControllerRoute(name: "GetProductOfShop", pattern: "product/product-of-shop", defaults: new { controller = "Product", action = "ProductOfShop" });
app.MapControllerRoute(name: "CreateProduct", pattern: "product/create", defaults: new { controller = "Product", action = "Create" });
app.MapControllerRoute(name: "UpdateProduct", pattern: "product/update/{productId}", defaults: new { controller = "Product", action = "Update" });
app.MapControllerRoute(name: "DeleteProduct", pattern: "product/delete/{productId}", defaults: new { controller = "Product", action = "Delete" });
app.MapControllerRoute(name: "ApproveProduct", pattern: "product/approve", defaults: new { controller = "Product", action = "Approve" });


app.Run();
