using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Product
    {
        public Product(String ProductName, int Barcode, decimal ProductPrice, int StockQuantity, string ImageUrl)
        {
            this.ProductName = ProductName;
            this.Barcode = Barcode;
            this.ProductPrice = ProductPrice;
            this.StockQuantity = StockQuantity;
            this.ImageUrl = ImageUrl;
        }

       
        public Product(int ProductId, String ProductName, int Barcode, decimal ProductPrice, int StockQuantity, string ImageUrl, byte[] RowId, Int64 RowIdBig)
        {
            this.ProductId = ProductId;
            this.ProductName = ProductName;
            this.Barcode = Barcode;
            this.ProductPrice = ProductPrice;
            this.StockQuantity = StockQuantity;
            this.ImageUrl = ImageUrl;
            this.RowId = RowId;
            this.RowIdBig = RowIdBig;
        }
        public Product() { }

        public int ProductId { get; set; }

        public string ImageUrl { get; set; }

        public override string ToString()
        {
            //return this.ProductName + ", " + this.ProductId + ", "+ this.Barcode + ", "+ this.ProductPrice + ", "+ this.StockQuantity;
            return " Id= " + this.ProductId + ", Name= " + this.ProductName + ", Barcode= " + this.Barcode + ", Price= " + this.ProductPrice + ", Quantity= " + this.StockQuantity + ", Category name: "+this.Category.CategoryName + ", Category Id: "+this.Category.CategoryId;
        }

        public String ProductName { get; set; }

        public int Barcode { get; set; }

        public decimal ProductPrice { get; set; }

        public int StockQuantity { get; set; }

        public Category Category { get; set; }
        public byte[] RowId { get; set; }
        public Int64 RowIdBig { get; set; }
    }
}
