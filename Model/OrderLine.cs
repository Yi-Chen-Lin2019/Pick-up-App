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
        public OrderLine(int Quantity, int OrderId)
        {
            this.Quantity = Quantity;
            this.OrderId = OrderId;
        }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public NoSNProduct NoSNProduct { get; set; }
    }
}
