using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class OrderLineViewModel
    {
        public int quantity { get; set; }
        public ProductViewModel product { get; set; }
        public OrderLineViewModel(int quantity, ProductViewModel product)
        {
            this.quantity = quantity;
            this.product = product;
        }
    }
}