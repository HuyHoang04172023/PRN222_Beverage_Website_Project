using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.Services
{
    public interface IOrderService
    {
        public void AddOrder(Order order);
        public void AddOrderItem(OrderItem orderItem);
    }
}
