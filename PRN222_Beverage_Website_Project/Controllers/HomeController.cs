using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN222_Beverage_Website_Project.Extensions;
using PRN222_Beverage_Website_Project.Models;
using PRN222_Beverage_Website_Project.Services;

namespace PRN222_Beverage_Website_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopService _shopService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _shopService = new ShopService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "user")]
        public IActionResult HomeUser()
        {
            return View();
        }

        [Authorize(Roles = "sale")]
        public IActionResult Sale()
        {
            var userId = User.FindFirstValue("UserID");
            Shop shop = _shopService.GetShopByUserID(int.Parse(userId));

            HttpContext.Session.SetObjectAsSession("shop", shop);
            //List<Item> cart = HttpContext.Session.GetObjectFromSession<List<Item>>("cart");
            //HttpContext.Session.Remove("cart");

            return View();
        }

        [Authorize(Roles = "manager")]
        public IActionResult Manager()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult Admin()
        {
            return View();
        }

    }
}
