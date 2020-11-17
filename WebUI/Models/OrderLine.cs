using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class OrderLine
    {
        public int quantity { get; set; }
        public Product product { get; set; }
        public OrderLine(int quantity, Product product)
        {
            this.quantity = quantity;
            this.product = product;
        }
    }
}