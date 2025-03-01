using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PRN222_Beverage_Website_Project.Models;
using PRN222_Beverage_Website_Project.Services;
using Microsoft.EntityFrameworkCore;

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
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {

            var user = _userService.CheckUserLogin(email, password);
            if (user == null)
            {
                ViewBag.ErrorLogin = "Email hoặc mật khẩu không đúng!";
                return View();
            }

            // Tạo Claims
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
