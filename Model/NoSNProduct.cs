using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class NoSNProduct : Product
    {
        public NoSNProduct(int ProductId, String ProductName, int Barcode, decimal ProductPrice, int StockQuantity, byte[] RowId, Int64 RowIdBig) : base(ProductId, ProductName, Barcode, ProductPrice, StockQuantity, RowId, RowIdBig)
        { }
        public NoSNProduct(int NoSNProductId, int ProductId, String ProductName, int Barcode, decimal ProductPrice, int StockQuantity, byte[] RowId, Int64 RowIdBig) : base(ProductId, ProductName, Barcode, ProductPrice, StockQuantity, RowId, RowIdBig)
        {
            this.NoSNProductId = NoSNProductId;
        }

        public int NoSNProductId { get; set; }
    }
}
