using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.ViewModels
{
    public class OrderLineViewModel
    {
        public int Quantity { get; set; }
        public ProductViewModel Product { get; set; }
        public OrderLineViewModel(int Quantity, ProductViewModel Product)
        {
            this.Quantity = Quantity;
            this.Product = Product;
        }
    }
}