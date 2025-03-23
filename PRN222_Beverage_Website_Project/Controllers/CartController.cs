using Microsoft.AspNetCore.Mvc;
using PRN222_Beverage_Website_Project.Extensions;
using PRN222_Beverage_Website_Project.Models;
using PRN222_Beverage_Website_Project.ModelViews;
using PRN222_Beverage_Website_Project.Services;

namespace PRN222_Beverage_Website_Project.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductVariantService _productVariantService;


        public CartController(IImageService imageService)
        {
            _productVariantService = new ProductVariantService();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var shoppingCart = HttpContext.Session.GetObjectFromSession<List<ItemCart>>("cart") ?? new List<ItemCart>();
            return View(shoppingCart);
        }

        [HttpPost]
        public IActionResult AddToCart(int variantId, int quantity)
        {
            var productVariant = _productVariantService.GetProductVariantByProductVariantId(variantId);
            if (productVariant == null) return NotFound();

            List<ItemCart> cart = HttpContext.Session.GetObjectFromSession<List<ItemCart>>("cart") ?? new List<ItemCart>();

            var existingItem = cart.FirstOrDefault(i => i.ProductVariantId == variantId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new ItemCart
                {
                    ProductVariantId = productVariant.ProductVariantId,
                    ProductName = productVariant.Product.ProductName,
                    ProductImage = productVariant.Product.ProductImage,
                    ProductVariantPrice = productVariant.ProductVariantPrice,
                    ProductSizeName = productVariant.ProductSize.ProductSizeName,
                    Quantity = quantity
                });
            }

            HttpContext.Session.SetObjectAsSession("cart", cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveProductVariantInCart(int productVariantId)
        {
            var cart = HttpContext.Session.GetObjectFromSession<List<ItemCart>>("cart") ?? new List<ItemCart>();

            // Kiểm tra xem sản phẩm có tồn tại trong giỏ hàng không
            var itemToRemove = cart.FirstOrDefault(i => i.ProductVariantId == productVariantId);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove); // Xóa sản phẩm khỏi giỏ hàng
            }

            HttpContext.Session.SetObjectAsSession("cart", cart);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productVariantId, int quantity)
        {
            var cart = HttpContext.Session.GetObjectFromSession<List<ItemCart>>("cart") ?? new List<ItemCart>();

            var item = cart.FirstOrDefault(i => i.ProductVariantId == productVariantId);
            if (item != null && quantity > 0)
            {
                item.Quantity = quantity; // Cập nhật số lượng sản phẩm
                HttpContext.Session.SetObjectAsSession("cart", cart);
            }

            return Ok(); // Trả về thành công mà không cần reload trang
        }
    }
}
