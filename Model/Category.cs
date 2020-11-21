using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
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

        public Category() { }
        public int CategoryId { get; set; }
  
        public String CategoryName { get; set; }


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Category {\n");
            sb.Append("  Name: ").Append(CategoryName).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }


        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
