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
        private readonly ProductService _productService;
        private readonly Prn222BeverageWebsiteProjectContext _context;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _shopService = new ShopService();
            _productService = new ProductService();
            _context = new Prn222BeverageWebsiteProjectContext();
        }

        public IActionResult Index()
        {
            ViewBag.TopSellProducts = _productService.GetTopSellProducts();
            ViewBag.LatestProducts = _productService.GetLatestProducts();

            List<Shop>? shops = _shopService.GetShopsByStatusShopName("active");
            return View(shops);
        }

        public IActionResult Search(string keyword, string city)
        {
            var shops = _context.Shops.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                shops = shops.Where(s => s.ShopName.Contains(keyword));
            }

            if (!string.IsNullOrEmpty(city))
            {
                shops = shops.Where(s => s.ShopAddress.Contains(city));
            }

            ViewBag.TopSellProducts = _productService.GetTopSellProducts();
            ViewBag.LatestProducts = _productService.GetLatestProducts();
            ViewBag.Keyword = keyword;
            ViewBag.SelectedCity = city;

            return View("Index", shops.ToList());
        }

        [Authorize(Roles = "user")]
        public IActionResult HomeUser()
        {
            return Redirect("/");
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
