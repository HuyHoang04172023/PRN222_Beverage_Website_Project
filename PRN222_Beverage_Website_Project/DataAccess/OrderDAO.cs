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
    }
}
