using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class OrderLineManagement
    {
        public IEnumerable<OrderLine> GetAll()
        {
            IOrderLineRepository pRepo = new OrderLineRepository();
            return pRepo.GetAllOrderLines();

        }

        public OrderLine DeleteOrderLine(int orderLineID)
        {
            IOrderLineRepository pRepo = new OrderLineRepository();
            return pRepo.DeleteOrderLine(orderLineID);
        }

        public OrderLine UpdateOrderLine(OrderLine orderLine)
        {
            IOrderLineRepository pRepo = new OrderLineRepository();
            return pRepo.UpdateOrderLine(orderLine);
        }

        public OrderLine InsertOrderLine(OrderLine orderLine)
        {
            IOrderLineRepository pRepo = new OrderLineRepository();
            return pRepo.InsertOrderLine(orderLine);
        }

        public OrderLine GetOrderLineById(int orderLineID)
        {
            IOrderLineRepository pRepo = new OrderLineRepository();
            return pRepo.GetOrderLineById(orderLineID);
        }
    }
}
