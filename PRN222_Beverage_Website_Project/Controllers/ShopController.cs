using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN222_Beverage_Website_Project.Models;
using PRN222_Beverage_Website_Project.Services;

namespace PRN222_Beverage_Website_Project.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShopService _shopService;
        private readonly IImageService _imageService;

        public ShopController(IImageService imageService)
        {
            _shopService = new ShopService();
            _imageService = imageService;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Shop shop, IFormFile shopImageFile)
        {
            ModelState.Remove("ShopImage");
            ModelState.Remove("StatusShop");
            ModelState.Remove("CreatedByNavigation");
            if (!ModelState.IsValid)
            {
                // Debug: In ra tất cả lỗi trong ModelState
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                    .ToList();
                foreach (var error in errors)
                {
                    foreach (var detail in error.Errors)
                    {
                        Console.WriteLine($"Property: {error.Key}, Error: {detail.ErrorMessage}");
                    }
                }
                return View(shop); // Trả về View để xem lỗi
            }
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

                    shop.StatusShopId = 1; // Trạng thái mặc định


                    var userId = User.FindFirstValue("UserID"); // Lấy UserID dưới dạng string
                    if (userId != null)
                    {
                        shop.CreatedBy = int.Parse(userId);
                    }

                    // Gán các giá trị mặc định
                    shop.CreatedAt = DateTime.UtcNow;

                    _shopService.AddShop(shop);
                    return Redirect("/user");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message); // Hiển thị lỗi từ ImageService
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
