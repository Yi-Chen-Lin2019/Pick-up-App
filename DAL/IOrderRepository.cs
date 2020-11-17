using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        List<Order> GetOrdersByStatus();
        Order GetOrderById();
        bool InsertOrder(Order order);
        bool UpdateOrder(Order order);
    }
}
