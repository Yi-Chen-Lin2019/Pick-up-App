using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class NoSNProduct : Product
    {
        public NoSNProduct(int ProductId, String ProductName, int Barcode, decimal ProductPrice, int StockQuantity) : base(ProductId, ProductName, Barcode, ProductPrice, StockQuantity)
        { }
        public NoSNProduct(int NoSNProductId, int ProductId, String ProductName, int Barcode, decimal ProductPrice, int StockQuantity) : base(ProductId, ProductName, Barcode, ProductPrice, StockQuantity)
        {
            this.NoSNProductId = NoSNProductId;
        }

        public int NoSNProductId { get; set; }
    }
}
