using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN222_Beverage_Website_Project.Extensions;
using PRN222_Beverage_Website_Project.Models;
using PRN222_Beverage_Website_Project.ModelViews;
using PRN222_Beverage_Website_Project.Services;

namespace PRN222_Beverage_Website_Project.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductVariantService _productVariantService;
        private readonly Prn222BeverageWebsiteProjectContext _context;
        private readonly ConfigDataService _configDataService;
        private readonly OrderService _orderService;

        public CartController(IImageService imageService)
        {
            _productVariantService = new ProductVariantService();
            _context = new Prn222BeverageWebsiteProjectContext();
            _configDataService = new ConfigDataService();
            _orderService = new OrderService();
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

        [HttpGet]
        public IActionResult Confirm()
        {
            var userId = User.FindFirstValue("UserID");
            if (userId == null)
            {
                return Redirect("/login");
            }
            var shoppingCart = HttpContext.Session.GetObjectFromSession<List<ItemCart>>("cart") ?? new List<ItemCart>();
            return View(shoppingCart);
        }
        //var userId = User.FindFirstValue("UserID");

        [HttpPost]
        public IActionResult Checkout(string orderNote, string phoneNumber, string shippingAddress)
        {
            var shoppingCart = HttpContext.Session.GetObjectFromSession<List<ItemCart>>("cart") ?? new List<ItemCart>();

            if (shoppingCart.Count == 0)
            {
                return RedirectToAction("Cart");
            }

            var productVariants = _context.ProductVariants
                .Where(pv => shoppingCart.Select(c => c.ProductVariantId).Contains(pv.ProductVariantId))
                .Include(pv => pv.Product) // Lấy Product để truy xuất ShopID
                .ToList();

            var groupedByShop = shoppingCart
                .GroupBy(item => productVariants.First(pv => pv.ProductVariantId == item.ProductVariantId).Product.ShopId)
                .ToList();

            var userId = User.FindFirstValue("UserID");
            foreach (var group in groupedByShop)
            {
                Order order = new Order();
                order.UserId = int.Parse(userId);
                order.ShopId = group.Key;
                order.StatusOrderId = _configDataService.GetStatusOrderIdByStatusOrderName("pending") ?? 10;
                order.CreatedAt = DateTime.Now;
                order.OrderNote = orderNote;
                order.PhoneNumber = phoneNumber;
                order.ShippingAddress = shippingAddress;
                _orderService.AddOrder(order); // Lưu Order để lấy OrderID

                foreach (var item in group)
                {
                    OrderItem ot = new OrderItem();
                    ot.OrderId = order.OrderId;
                    ot.ProductVariantId = item.ProductVariantId;
                    ot.OrderItemQuantity = item.Quantity;
                    ot.OrderItemPrice = item.ProductVariantPrice;
                    _orderService.AddOrderItem(ot);
                }
            }

            HttpContext.Session.Remove("cart");

            return RedirectToAction("Index");
        }

    }
}
