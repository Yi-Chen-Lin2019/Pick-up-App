using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string image { get; set; }
        public Category category { get; set; }

        public Product(int id, string name, double price, string image, Category category)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.image = image;
            this.category = category;
        }
    }
}