using REST.Models;
using System.Collections.Generic;

namespace REST.Controllers
{
    internal class OrderRepository : IOrderRepository
    {
        public Order DeleteOrder(int orderID)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            throw new System.NotImplementedException();
        }

        public Order GetOrderById(int orderID)
        {
            throw new System.NotImplementedException();
        }

        public Order InsertOrder(Order order)
        {
            throw new System.NotImplementedException();
        }

        public Order UpdateOrder(int id, Order order)
        {
            throw new System.NotImplementedException();
        }

        public Order UpdateOrder(Order order)
        {
            throw new System.NotImplementedException();
        }
    }
}