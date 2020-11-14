using REST.Models;
using System.Collections.Generic;

namespace REST.Controllers
{
    internal interface IOrderRepository
    {
        Order InsertOrder(Order order);
        Order UpdateOrder(int id, Order order);
        Order DeleteOrder(int orderID);
        Order GetOrderById(int orderID);
        IEnumerable<Order> GetAllOrders();
    }
}