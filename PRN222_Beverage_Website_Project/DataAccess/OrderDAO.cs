using Microsoft.EntityFrameworkCore;
using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.DataAccess
{
    public class OrderDAO
    {
        private readonly Prn222BeverageWebsiteProjectContext _context;
        public OrderDAO()
        {
            _context = new Prn222BeverageWebsiteProjectContext();
        }

        public void AddOrder (Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();
        }

        public List<Order> GetOrdersByShopId(int shopId)
        {
            return _context.Orders
                .Where(o => o.ShopId == shopId)
                .Include(o => o.StatusOrder)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariant)
                        .ThenInclude(pv => pv.Product)
                .OrderByDescending(o => o.CreatedAt)
                .ToList();
        }

        public Order? GetOrderByOrderId(int orderId)
        {
            return _context.Orders
                    .Where(o => o.OrderId == orderId)
                    .Include(o => o.StatusOrder)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.ProductVariant)
                            .ThenInclude(pv => pv.Product)
                    .FirstOrDefault();
        }
    }
}
