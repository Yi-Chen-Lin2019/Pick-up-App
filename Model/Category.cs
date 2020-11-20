using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Model
{
    /// <summary>
    /// Category
    /// </summary>
    [DataContract]
    public class Category
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Category" /> class.
        /// </summary>
        /// <param name="Category Name">Category Name (required).</param>
        public Category(String CategoryName)
        {
            this.CategoryName = CategoryName;
        }
        public Category(int CategoryId, String CategoryName)
        {
            this.CategoryId = CategoryId;
            this.CategoryName = CategoryName;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Category" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        public Category() { }
        public int CategoryId { get; set; }
        /// <summary>
        /// Gets or Sets Category Name
        /// </summary>
        public String CategoryName { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Category {\n");
            sb.Append("  Name: ").Append(CategoryName).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
