using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OrdersOfShop(int shopId)
        {
            List< Order > orders = _orderService.GetOrdersByShopId(shopId);

            return View(orders);
        }

        [HttpPost]
        public IActionResult OrderDetails(int orderId)
        {
            Order? order = _orderService.GetOrderByOrderId(orderId);

            if (order == null) return NotFound();

            return View(order);
        }

    }
}
