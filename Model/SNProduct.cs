using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SNProduct
    {
        Product product;
        String serialNumber;
        public SNProduct(Product product, String serialNumber)
        {
            this.serialNumber = serialNumber;
            this.product = product;
        }

        public String SerialNumber { get; set; }

        public Product Product { get; set; }
    }
}
