using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class OrderLine
    {
        public OrderLine(int Quantity)
        {
            this.Quantity = Quantity;
        }
        public OrderLine(int OrderLineId, int Quantity, int OrderId)
        {
            this.OrderLineId = OrderLineId;
            this.Quantity = Quantity;
            this.OrderId = OrderId;
        }
        public int OrderLineId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public NoSNProduct NoSNProduct { get; set; }
    }
}
