using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class NoSNProduct : Product
    {
        public NoSNProduct(String name, int barcode, double price, Category category) : base(name, barcode, price, category)
        { }
    }
}
