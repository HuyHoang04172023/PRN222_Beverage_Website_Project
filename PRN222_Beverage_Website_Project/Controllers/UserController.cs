using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PRN222_Beverage_Website_Project.Models;
using PRN222_Beverage_Website_Project.Services;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace PRN222_Beverage_Website_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly Prn222BeverageWebsiteProjectContext _dbContext;

        public UserController(ILogger<UserController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _userService = new UserService();
            _dbContext = new Prn222BeverageWebsiteProjectContext();
        }



        [HttpGet]
        public ActionResult Register()
        {
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User model)
        {
            ModelState.Remove("Role");
            ModelState.Remove("UserId");

            Console.WriteLine("UserID: " + model.UserId);

            if (ModelState.IsValid)
            {
                // Kiểm tra xem email đã tồn tại chưa
                if (_userService.CheckEMailExist(model.Email))
                {
                    ModelState.AddModelError("Email", "Email đã được sử dụng.");
                    return View(model);
                }

                // Mã hóa mật khẩu trước khi lưu vào database
                model.Password = HashPassword(model.Password);
                model.CreatedAt = DateTime.Now;

                model.RoleId = 1;

                Console.WriteLine("UserID (Trước khi lưu vào DB): " + model.UserId);  // In ra UserID trước khi lưu
                model.UserId = 0;
                // Thêm người dùng mới vào database
                _userService.AddUser(model);

                return Redirect("/login");
            }

            return View(model);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var byteValue in bytes)
                {
                    stringBuilder.Append(byteValue.ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }









        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {

            var user = _userService.CheckUserLogin(email, HashPassword(password));
            if (user == null)
            {
                ViewBag.ErrorLogin = "Email hoặc mật khẩu không đúng!";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.RoleName),
                new Claim("UserID", user.UserId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          new ClaimsPrincipal(claimsIdentity),
                                          authProperties);

            var role = user.Role.RoleName;
            switch (role)
            {
                case "admin":
                    return Redirect("/admin");
                case "sale":
                    return Redirect("/sale");
                case "manager":
                    return Redirect("/manager");
                default:
                    return Redirect("/");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
