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

        public OrderController()
        {
            _orderService = new OrderService();
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

            return View(order);
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult OrdersOfUser()
        {
            var userId = User.FindFirstValue("UserID");

            List<Order> orders = _orderService.GetOrdersByUserId(int.Parse(userId));

            return View(orders);
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public IActionResult OrderDetailOfUser(int orderId)
        {
            Order? order = _orderService.GetOrderByOrderId(orderId);

            if (order == null) return NotFound();

            return View(order);
        }
    }
}
