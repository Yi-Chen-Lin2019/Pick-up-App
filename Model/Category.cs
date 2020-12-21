using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Category
    {
        public Category(String CategoryName)
        {
            this.CategoryName = CategoryName;
        }

        
        public Category(int CategoryId, String CategoryName)
        {
            this.CategoryId = CategoryId;
            this.CategoryName = CategoryName;
        }
        public Category()
        {

        }

        public override string ToString()
        {
            return this.CategoryName + ", Id: "+this.CategoryId;
        }
        public int CategoryId { get; set; }
        public String CategoryName { get; set; }
    }
}
