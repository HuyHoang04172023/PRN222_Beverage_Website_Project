using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN222_Beverage_Website_Project.Models;
using PRN222_Beverage_Website_Project.ModelViews;
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
            var userId = User.FindFirstValue("UserID");
            Shop shop = _shopService.GetShopByUserID(int.Parse(userId));
            if(shop != null)
            {
                return View("Show", shop);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Create(ShopViewModel shopView, IFormFile shopImageFile)
        {
            ModelState.Remove("ShopImage");
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Shop shop = new Shop();
                    if (shopImageFile != null && shopImageFile.Length > 0)
                    {
                        shop.ShopImage = await _imageService.SaveImageAsync(shopImageFile, "shops");
                    }
                    else
                    {
                        ViewData["ImageError"] = "Please upload an image.";
                        return View(shop);
                    }

                    shop.ShopName = shopView.ShopName;
                    shop.ShopAddress = shopView.ShopAddress;
                    shop.ShopDescription = shopView.ShopDescription;
                    shop.StatusShopId = _configDataService.GetStatusShopIdByStatusShopName("pending") ?? 10;

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
            return View(shopView);
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult Update(int shopId)
        {
            var userId = User.FindFirstValue("UserID");
            Shop shop = _shopService.GetShopByShopID(shopId);

            if (shop == null)
            {
                return NotFound("Cửa hàng không tồn tại.");
            }

            if (shop.CreatedBy != int.Parse(userId))
            {
                return View("~/Views/User/AccessDenied.cshtml");
            }

            var shopView = new ShopViewModel
            {
                ShopId = shop.ShopId,
                ShopName = shop.ShopName,
                ShopAddress = shop.ShopAddress,
                ShopImage = shop.ShopImage,
                ShopDescription = shop.ShopDescription
            };
            return View(shopView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Update(ShopViewModel shopView, IFormFile shopImageFile)
        {
            ModelState.Remove("ShopImage");

            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Shop shop = new Shop();
                    if (shopImageFile != null && shopImageFile.Length > 0)
                    {
                        shop.ShopImage = await _imageService.SaveImageAsync(shopImageFile, "shops");
                    }
                    else
                    {
                        ViewData["ImageError"] = "Please upload an image.";
                        return View(shop);
                    }
                    shop.ShopId = shopView.ShopId;
                    shop.ShopName = shopView.ShopName;
                    shop.ShopAddress = shopView.ShopAddress;
                    shop.ShopDescription = shopView.ShopDescription;

                    _shopService.UpdateShop(shop);
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
            return View(shopView);
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult Delete(int shopId)
        {
            var userId = User.FindFirstValue("UserID");
            Shop shop = _shopService.GetShopByShopID(shopId);

            if (shop == null)
            {
                return NotFound("Cửa hàng không tồn tại.");
            }

            if (shop.CreatedBy != int.Parse(userId))
            {
                return View("~/Views/User/AccessDenied.cshtml");
            }

            _shopService.DeleteShop(shopId);
            return Redirect("/user");
        }
    }
}
