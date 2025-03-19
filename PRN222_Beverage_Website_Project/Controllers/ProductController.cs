using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN222_Beverage_Website_Project.Extensions;
using PRN222_Beverage_Website_Project.Models;
using PRN222_Beverage_Website_Project.ModelViews;
using PRN222_Beverage_Website_Project.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PRN222_Beverage_Website_Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly Prn222BeverageWebsiteProjectContext _context;
        private readonly IImageService _imageService;
        private readonly ConfigDataService _configDataService;

        public ProductController(IImageService imageService)
        {
            _context = new Prn222BeverageWebsiteProjectContext();
            _imageService = imageService;
            _configDataService = new ConfigDataService();
        }

        [HttpGet]
        [Authorize(Roles = "sale")]
        public IActionResult Create()
        {
            var model = new ProductViewModel
            {
                ProductSizes = _context.ProductSizes
                    .Select(s => new SelectListItem { Value = s.ProductSizeId.ToString(), Text = s.ProductSizeName })
                    .ToList()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "sale")]
        public async Task<IActionResult> CreateAsync(ProductViewModel model, IFormFile productImageFile)
        {
            Console.WriteLine($"productImageFile: {productImageFile}");
            // In thông tin của model ra console
            Console.WriteLine("Tên sản phẩm: " + model.ProductName);
            Console.WriteLine("Mô tả sản phẩm: " + model.ProductDescription);
            Console.WriteLine("Hình ảnh: " + model.ProductImage);

            if (model.ProductVariants != null)
            {
                foreach (var variant in model.ProductVariants)
                {
                    Console.WriteLine($"Biến thể - Giá: {variant.ProductVariantPrice}, Kích thước ID: {variant.ProductSizeId}");
                }
            }
            ModelState.Remove("ProductImage");
            ModelState.Remove("productImageFile");

            if (!ModelState.IsValid)
            {
                //Debug
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Lỗi tại {state.Key}: {error.ErrorMessage}");
                    }
                }

                model.ProductSizes = _context.ProductSizes
                    .Select(s => new SelectListItem { Value = s.ProductSizeId.ToString(), Text = s.ProductSizeName })
                    .ToList();

                return View(model);

            }
            Shop shop = HttpContext.Session.GetObjectFromSession<Shop>("shop");

            Product product = new Product();

            product.ProductName = model.ProductName;
            product.ProductDescription = model.ProductDescription;
            product.StatusProductId = _configDataService.GetStatusProductIdByStatusProductName("pending") ?? 10;
            product.ShopId = shop.ShopId;
            product.CreatedAt = DateTime.Now;

            if (productImageFile != null && productImageFile.Length > 0)
            {

                product.ProductImage = await _imageService.SaveImageAsync(productImageFile, "products");
            }
            else
            {
                ViewData["ImageError"] = "Please upload an image.";
                return View(model);
            }

            _context.Products.Add(product);
            _context.SaveChanges();

            foreach (var variant in model.ProductVariants)
            {
                ProductVariant productVariant = new ProductVariant();
                productVariant.ProductVariantPrice = variant.ProductVariantPrice;
                productVariant.ProductSizeId = variant.ProductSizeId;
                productVariant.ProductId = product.ProductId;
                _context.ProductVariants.Add(productVariant);
            }
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Thêm sản phẩm thành công!";
            return RedirectToAction("Create");
        }
    }
}
