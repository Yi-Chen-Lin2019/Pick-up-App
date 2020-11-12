using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Product
    {
        String name;
        int barcode;
        double price;
        int stockQuantity;
        Category category;
        public Product(String name, int barcode, double price, Category category)
        {
            this.name = name;
            this.barcode = barcode;
            this.price = price;
            this.category = category;
        }

        public String Name { get; set; }

        public int Barcode { get; set; }

        public double Price { get; set; }

        public int StockQuantity { get; set; }

        public Category Category { get; set; }
    }
}
