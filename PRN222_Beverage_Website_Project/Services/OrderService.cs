using PRN222_Beverage_Website_Project.Models;
using PRN222_Beverage_Website_Project.Repositories;

namespace PRN222_Beverage_Website_Project.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        public OrderService()
        {
            _repository = new OrderRepository();
        }
        public void AddOrder(Order order)
        {
            _repository.AddOrder(order);
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            _repository.AddOrderItem(orderItem);
        }

        public List<Order> GetOrdersByShopId(int shopId)
        {
            return _repository.GetOrdersByShopId(shopId);
        }
        public Order? GetOrderByOrderId(int orderId)
        {
            return _repository.GetOrderByOrderId(orderId);
        }

        public List<Order> GetOrdersByUserId(int userId)
        {
            return _repository.GetOrdersByUserId(userId);
        }

        public void UpdateStatusOrderByOrderId(int orderId, int idStatusOrder)
        {
            _repository.UpdateStatusOrderByOrderId(orderId, idStatusOrder);
        }
    }
}
