using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN222_Beverage_Website_Project.Models;
using PRN222_Beverage_Website_Project.Services;

namespace PRN222_Beverage_Website_Project.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShopService _shopService;
        private readonly ConfigDataService _configDataService;
        private readonly IImageService _imageService;

        public ShopController(IImageService imageService)
        {
            _shopService = new ShopService();
            _configDataService = new ConfigDataService();
            _imageService = imageService;
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Create(Shop shop, IFormFile shopImageFile)
        {
            ModelState.Remove("ShopImage");
            ModelState.Remove("StatusShop");
            ModelState.Remove("CreatedByNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    if (shopImageFile != null && shopImageFile.Length > 0)
                    {
                        shop.ShopImage = await _imageService.SaveImageAsync(shopImageFile, "shops");
                    }
                    else
                    {
                        ViewData["ImageError"] = "Please upload an image.";
                        return View(shop);
                    }

                    shop.StatusShopId = _configDataService.GetStatusShopIdByStatusShopName("pending") ?? 10; // Trạng thái mặc định


                    var userId = User.FindFirstValue("UserID");
                    if (userId != null)
                    {
                        shop.CreatedBy = int.Parse(userId);
                    }

                    shop.CreatedAt = DateTime.UtcNow;

                    _shopService.AddShop(shop);
                    return Redirect("/user");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error saving shop: {ex.Message}");
                }
            }
            return View(shop);
        }
    }
}
