﻿using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.Repositories
{
    public interface IOrderRepository
    {
        public void AddOrder(Order order);
        public void AddOrderItem(OrderItem orderItem);
        public List<Order> GetOrdersByShopId(int shopId);
        public Order? GetOrderByOrderId(int orderId);
        public List<Order> GetOrdersByUserId(int userId);
        public void UpdateStatusOrderByOrderId(int orderId, int idStatusOrder);
    }
}
