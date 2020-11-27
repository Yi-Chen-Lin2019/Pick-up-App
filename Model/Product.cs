using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Product
    {
        public Product(String ProductName, int Barcode, decimal ProductPrice, int StockQuantity)
        {
            this.ProductName = ProductName;
            this.Barcode = Barcode;
            this.ProductPrice = ProductPrice;
            this.StockQuantity = StockQuantity;
        }

       
        public Product(int ProductId, String ProductName, int Barcode, decimal ProductPrice, int StockQuantity, byte[] RowId, Int64 RowIdBig)
        {
            this.ProductId = ProductId;
            this.ProductName = ProductName;
            this.Barcode = Barcode;
            this.ProductPrice = ProductPrice;
            this.StockQuantity = StockQuantity;
            this.RowId = RowId;
            this.RowIdBig = RowIdBig;
        }
        public Product() { }

        public int ProductId { get; set; }

        public String ProductName { get; set; }

        public int Barcode { get; set; }

        public decimal ProductPrice { get; set; }

        public int StockQuantity { get; set; }

        public Category Category { get; set; }
        public byte[] RowId { get; set; }
        public Int64 RowIdBig { get; set; }
    }
}
