using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class OrderLine
    {
        int quantity;
        NoSNProduct noSNProduct;
        public OrderLine(int quantity, NoSNProduct noSNProduct)
        {
            this.quantity = quantity;
            this.noSNProduct = noSNProduct;
        }
        public int Quantity { get; set; }
        public NoSNProduct NoSNProduct { get; set; }
    }
}
