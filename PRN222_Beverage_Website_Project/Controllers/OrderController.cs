using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN222_Beverage_Website_Project.Extensions;
using PRN222_Beverage_Website_Project.Models;
using PRN222_Beverage_Website_Project.Services;

namespace PRN222_Beverage_Website_Project.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly ConfigDataService _configDataService;
        private readonly Prn222BeverageWebsiteProjectContext _context;


        public OrderController()
        {
            _orderService = new OrderService();
            _configDataService = new ConfigDataService();
            _context = new Prn222BeverageWebsiteProjectContext();
        }


        [HttpGet]
        [Authorize(Roles = "sale")]
        public IActionResult OrdersOfShop()
        {
            Shop shop = HttpContext.Session.GetObjectFromSession<Shop>("shop");
            List< Order > orders = _orderService.GetOrdersByShopId(shop.ShopId);

            return View(orders);
        }

        [HttpPost]
        [Authorize(Roles = "sale")]
        public IActionResult OrderDetailOfShop(int orderId)
        {
            Order? order = _orderService.GetOrderByOrderId(orderId);

            if (order == null) return NotFound();

            ViewBag.StatusOrders = _configDataService.GetStatusOrders();
            return View(order);
        }

        [HttpGet]
        public IActionResult OrdersOfUser()
        {
            var userId = User.FindFirstValue("UserID");

            List<Order> orders = _orderService.GetOrdersByUserId(int.Parse(userId));

            return View(orders);
        }

        [HttpPost]
        public IActionResult OrderDetailOfUser(int orderId)
        {
            Order? order = _orderService.GetOrderByOrderId(orderId);

            if (order == null) return NotFound();

            return View(order);
        }

        [HttpPost]
        [Authorize(Roles = "user, sale")]
        public IActionResult UpdateStatusOrderByUser(int orderId, string statusName)
        {
            var statusId = _configDataService.GetStatusOrderIdByStatusOrderName(statusName) ?? 10;
            _orderService.UpdateStatusOrderByOrderId(orderId, statusId);

            return Redirect("/user/order");
        }

        public IActionResult UpdateStatusOrderByShop(int orderId, string statusName)
        {
            var statusId = _configDataService.GetStatusOrderIdByStatusOrderName(statusName) ?? 10;
            _orderService.UpdateStatusOrderByOrderId(orderId, statusId);

            if (statusName.ToLower() == "success")
            {
                var order = _context.Orders
                    .Where(o => o.OrderId == orderId)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.ProductVariant)
                            .ThenInclude(pv => pv.Product)
                    .FirstOrDefault();

                if (order != null)
                {
                    foreach (var item in order.OrderItems)
                    {
                        var product = item.ProductVariant.Product;
                        if (product != null)
                        {
                            product.ProductSoldCount = (product.ProductSoldCount ?? 0) + item.OrderItemQuantity;
                        }
                    }

                    _context.SaveChanges();
                }
            }

            return Redirect("/shop/order");
        }
    }
}
