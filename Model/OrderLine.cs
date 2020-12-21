using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class OrderLine
    {
        public OrderLine()
        {
        }

        public OrderLine(int Quantity, Product product)
        {
            this.Quantity = Quantity;
            this.Product = product;
        }
        [JsonIgnore]
        public int OrderLineId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}
