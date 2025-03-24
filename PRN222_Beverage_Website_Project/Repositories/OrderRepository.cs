using PRN222_Beverage_Website_Project.DataAccess;
using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDAO _dao;
        public OrderRepository()
        {
            _dao = new OrderDAO();
        }

        public void AddOrder(Order order)
        {
            _dao.AddOrder(order);
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            _dao.AddOrderItem(orderItem);
        }

        public List<Order> GetOrdersByShopId(int shopId)
        {
            return _dao.GetOrdersByShopId(shopId);
        }

        public Order? GetOrderByOrderId(int orderId)
        {
            return _dao.GetOrderByOrderId(orderId);
        }
    }
}
