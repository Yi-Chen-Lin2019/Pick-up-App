using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class SNProduct : Product
    {
        public string SerialNumber { get; set; }

        public SNProduct()
        {

        }
        public SNProduct(int id, string name, double price, string image, Category category, string serialNumber) : base(id, name, price, image, category)
        {
            SerialNumber = serialNumber;
        }
    }
}