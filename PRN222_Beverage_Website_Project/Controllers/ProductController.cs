using System.Security.Claims;
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
        private readonly ProductService _productService;


        public ProductController(IImageService imageService)
        {
            _context = new Prn222BeverageWebsiteProjectContext();
            _imageService = imageService;
            _configDataService = new ConfigDataService();
            _productService = new ProductService();
        }

        public IActionResult ProductOfShop()
        {
            Shop shop = HttpContext.Session.GetObjectFromSession<Shop>("shop");
            List<Product> products = _productService.GetProductsByShopId(shop.ShopId);

            return View(products);
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
            //Debug
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
            //End Debug

            ModelState.Remove("ProductImage");
            ModelState.Remove("productImageFile");

            //Add ProductSizes to return view if error
            model.ProductSizes = _context.ProductSizes
                    .Select(s => new SelectListItem { Value = s.ProductSizeId.ToString(), Text = s.ProductSizeName })
                    .ToList();
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
                //End Debug

                return View(model);
            }


            //VariantError
            HashSet<int> sizeIds = new HashSet<int>();

            foreach (var variant in model.ProductVariants)
            {
                if (!sizeIds.Add(variant.ProductSizeId)) // Nếu Add() trả về false => bị trùng
                {
                    TempData["VariantError"] = $"Lỗi: Kích thước bị trùng!";
                    return View(model);
                }
            }

            Shop shop = HttpContext.Session.GetObjectFromSession<Shop>("shop");
            var userId = User.FindFirstValue("UserID");

            Product product = new Product();
            product.ProductName = model.ProductName;
            product.ProductDescription = model.ProductDescription;
            product.StatusProductId = _configDataService.GetStatusProductIdByStatusProductName("pending") ?? 10;
            product.ShopId = shop.ShopId;
            product.CreatedAt = DateTime.Now;
            product.CreatedBy = int.Parse(userId);

            if (productImageFile != null && productImageFile.Length > 0)
            {

                product.ProductImage = await _imageService.SaveImageAsync(productImageFile, "products");
            }
            else
            {
                ViewData["ImageError"] = "Vui lòng tải lên một ảnh.";
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
            return Redirect("/product/product-of-shop");
        }

        [HttpGet]
        [Authorize(Roles = "sale")]
        public IActionResult Update(int productId)
        {
            Product? product = _productService.GetProductByProductId(productId);

            if (product == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue("UserID");

            if (product.CreatedBy != int.Parse(userId))
            {
                return View("~/Views/User/AccessDenied.cshtml");
            }

            ProductViewModel productView = new ProductViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductImage = product.ProductImage,
                ProductVariants = product.ProductVariants.Select(v => new ProductVariantViewModel
                {
                    ProductVariantId = v.ProductVariantId,
                    ProductVariantPrice = v.ProductVariantPrice,
                    ProductSizeId = v.ProductSizeId
                }).ToList()
            };

            productView.ProductSizes = _context.ProductSizes
                .Select(s => new SelectListItem { Value = s.ProductSizeId.ToString(), Text = s.ProductSizeName })
                .ToList();

            return View(productView);
        }

        [HttpPost]
        [Authorize(Roles = "sale")]
        public async Task<IActionResult> UpdateAsync(ProductViewModel model, IFormFile productImageFile)
        {
            //Debug
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
            //End Debug

            ModelState.Remove("ProductImage");
            ModelState.Remove("productImageFile");

            //Add ProductSizes to return view if error
            model.ProductSizes = _context.ProductSizes
                    .Select(s => new SelectListItem { Value = s.ProductSizeId.ToString(), Text = s.ProductSizeName })
                    .ToList();
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
                //End Debug

                return View(model);
            }


            //VariantError
            HashSet<int> sizeIds = new HashSet<int>();

            foreach (var variant in model.ProductVariants)
            {
                if (!sizeIds.Add(variant.ProductSizeId)) // Nếu Add() trả về false => bị trùng
                {
                    TempData["VariantError"] = $"Lỗi: Kích thước bị trùng!";
                    return View(model);
                }
            }

            Product product = new Product();
            product.ProductId = model.ProductId;
            product.ProductName = model.ProductName;
            product.ProductDescription = model.ProductDescription;

            if (productImageFile != null && productImageFile.Length > 0)
            {

                product.ProductImage = await _imageService.SaveImageAsync(productImageFile, "products");
            }
            else
            {
                ViewData["ImageError"] = "Vui lòng tải lên một ảnh.";
                return View(model);
            }

            _productService.UpdateProduct(product);

            _productService.DeleteProductVariantByProductId(product.ProductId);
            foreach (var variant in model.ProductVariants)
            {
                ProductVariant productVariant = new ProductVariant();
                productVariant.ProductVariantPrice = variant.ProductVariantPrice;
                productVariant.ProductSizeId = variant.ProductSizeId;
                productVariant.ProductId = model.ProductId;

                _productService.AddProductVarriant(productVariant);
            }

            TempData["SuccessMessage"] = "Thêm sản phẩm thành công!";
            return Redirect("/product/product-of-shop");
        }

        [HttpGet]
        [Authorize(Roles = "sale")]
        public IActionResult Delete(int productId)
        {
            var userId = User.FindFirstValue("UserID");
            Product product = _productService.GetProductByProductId(productId);

            if (product == null)
            {
                return NotFound("Sản phẩm không tồn tại.");
            }

            if (product.CreatedBy != int.Parse(userId))
            {
                return View("~/Views/User/AccessDenied.cshtml");
            }

            _productService.DeleteProductByProductId(productId);
            return Redirect("/product/product-of-shop");
        }

        [HttpGet]
        [Authorize(Roles = "manager")]
        public IActionResult Approve()
        {
            List<Product>? products = _productService.GetProductsPending();

            return View(products);
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public IActionResult UpdateStatusProduct(int productId, string statusProduct)
        {
            var userId = User.FindFirstValue("UserID");
            int statusId = _configDataService.GetStatusProductIdByStatusProductName(statusProduct) ?? 10;
            _productService.UpdateStatusProductByProductId(productId, statusId);

            Product? product = _productService.GetProductByProductId(productId);
            if(product != null)
            {
                product.ApprovedBy = int.Parse(userId);
                _productService.UpdateApprovedByOfProduct(product);
            }

            return Json(new { success = true });
        }
        public IActionResult Detail(int productId)
        {
            Product? product = _productService.GetProductByProductId(productId);

            return View(product);
        }
    }
}
