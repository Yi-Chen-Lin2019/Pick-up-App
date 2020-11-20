using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Model
{
    /// <summary>
    /// Product
    /// </summary>
    [DataContract]
    public class Product
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product" /> class.
        /// </summary>
        /// <param name="ProductName">name (required).</param>
        /// <param name="Barcode">barcode (required).</param>
        /// <param name="ProductPrice">price (required).</param>
        /// <param name="StockQuantity">stockQuantity (required).</param>
        public Product(String ProductName, int Barcode, decimal ProductPrice, int StockQuantity)
        {
            this.ProductName = ProductName;
            this.Barcode = Barcode;
            this.ProductPrice = ProductPrice;
            this.StockQuantity = StockQuantity;
        }
        public Product(int ProductId, String ProductName, int Barcode, decimal ProductPrice, int StockQuantity)
        {
            this.ProductId = ProductId;
            this.ProductName = ProductName;
            this.Barcode = Barcode;
            this.ProductPrice = ProductPrice;
            this.StockQuantity = StockQuantity;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Product" /> class.
        /// </summary>
        /// <param name="name">name (required).</param>
        /// <param name="barcode">barcode (required).</param>
        /// <param name="price">price (required).</param>
        /// <param name="stockQuantity">stockQuantity (required).</param>
        /// <param name="category">category (required).</param>
        public Product(string name, int barcode, decimal price, int stockQuantity, Category category = default(Category))
        {
            // to ensure "name" is required (not null)
            if (name == null)
            {
                throw new InvalidDataException("name is a required property for Product and cannot be null");
            }
            else
            {
                this.ProductName = name;
            }
            // to ensure "barcode" is required (not null)
            if (barcode == null)
            {
                throw new InvalidDataException("barcode is a required property for Product and cannot be null");
            }
            else
            {
                this.Barcode = barcode;
            }
            // to ensure "price" is required (not null)
            if (price == null)
            {
                throw new InvalidDataException("price is a required property for Product and cannot be null");
            }
            else
            {
                this.ProductPrice = price;
            }
            // to ensure "stockQuantity" is required (not null)
            if (stockQuantity == null)
            {
                throw new InvalidDataException("stockQuantity is a required property for Product and cannot be null");
            }
            else
            {
                this.StockQuantity = stockQuantity;
            }
            // to ensure "category" is required (not null)
            if (category == null)
            {
                throw new InvalidDataException("category is a required property for Product and cannot be null");
            }
            else
            {
                this.Category = category;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Product" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        public Product() { }

        public int ProductId { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "Product Name", EmitDefaultValue = false)]
        public String ProductName { get; set; }

        /// <summary>
        /// Gets or Sets Barcode
        /// </summary>
        [DataMember(Name = "Barcode", EmitDefaultValue = false)]
        public int Barcode { get; set; }

        /// <summary>
        /// Gets or Sets Price
        /// </summary>
        [DataMember(Name = "Product Price", EmitDefaultValue = false)]
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// Gets or Sets Quantity of Stock
        /// </summary>
        [DataMember(Name = "Quantity of Stock", EmitDefaultValue = false)]
        public int StockQuantity { get; set; }

        /// <summary>
        /// Gets or Sets Category
        /// </summary>
        [DataMember(Name = "Category", EmitDefaultValue = false)]
        public Category Category { get; set; }


        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Product {\n");
            sb.Append("  Name: ").Append(ProductName).Append("\n");
            sb.Append("  Barcode: ").Append(Barcode).Append("\n");
            sb.Append("  Price: ").Append(ProductPrice).Append("\n");
            sb.Append("  StockQuantity: ").Append(StockQuantity).Append("\n");
            sb.Append("  Category: ").Append(Category).Append("\n");
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
