using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SNProduct
    {
        public SNProduct(String SerialNumber)
        {
            this.SerialNumber = SerialNumber;
        }
        public SNProduct(int SNProductId, String SerialNumber)
        {
            this.SNProductId = SNProductId;
            this.SerialNumber = SerialNumber;
        }

        public int SNProductId { get; set; }

        public String SerialNumber { get; set; }

        public int OrderId { get; set; }

        public Product Product { get; set; }
    }
}
