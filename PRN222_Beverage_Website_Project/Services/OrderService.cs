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
    }
}
