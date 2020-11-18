using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class OrderManagement
    {
        public IEnumerable<Order> GetAllOrders()
        {
            IOrderRepository pRepo = new OrderRepository();
            return pRepo.GetAllOrders();

        }

        public Order DeleteOrder(int orderID)
        {
            IOrderRepository pRepo = new OrderRepository();
            return pRepo.DeleteOrder(orderID);
        }

        public Order UpdateOrder(Order order)
        {
            IOrderRepository pRepo = new OrderRepository();
            return pRepo.UpdateOrder(order);
        }

        public Order InsertOrder(Order order)
        {
            IOrderRepository pRepo = new OrderRepository();
            return pRepo.InsertOrder(order);
        }

        public Order GetOrderById(int orderID)
        {
            IOrderRepository pRepo = new OrderRepository();
            return pRepo.GetOrderById(orderID);
        }
    }
}
