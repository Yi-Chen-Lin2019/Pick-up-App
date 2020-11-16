/* 
 * Pick-up API
 *
 * This is an API for pick-up app which is created for 3rd semester project in UCN Computer Science Programme.
 *
 * OpenAPI spec version: 1.0.0
 * Contact: 1081501@ucn.dk
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace REST.Models
{
    /// <summary>
    /// Product
    /// </summary>
    [DataContract]
    public partial class Product :  IEquatable<Product>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected Product() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Product" /> class.
        /// </summary>
        /// <param name="name">name (required).</param>
        /// <param name="barcode">barcode (required).</param>
        /// <param name="price">price (required).</param>
        /// <param name="stockQuantity">stockQuantity (required).</param>
        /// <param name="category">category (required).</param>
        public Product(string name = default(string), string barcode = default(string), int? price = default(int?), int? stockQuantity = default(int?), Category category = default(Category))
        {
            // to ensure "name" is required (not null)
            if (name == null)
            {
                throw new InvalidDataException("name is a required property for Product and cannot be null");
            }
            else
            {
                this.Name = name;
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
                this.Price = price;
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
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Barcode
        /// </summary>
        [DataMember(Name="barcode", EmitDefaultValue=false)]
        public string Barcode { get; set; }

        /// <summary>
        /// Gets or Sets Price
        /// </summary>
        [DataMember(Name="price", EmitDefaultValue=false)]
        public int? Price { get; set; }

        /// <summary>
        /// Gets or Sets StockQuantity
        /// </summary>
        [DataMember(Name="stockQuantity", EmitDefaultValue=false)]
        public int? StockQuantity { get; set; }

        /// <summary>
        /// Gets or Sets Category
        /// </summary>
        [DataMember(Name="category", EmitDefaultValue=false)]
        public Category Category { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Product {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Barcode: ").Append(Barcode).Append("\n");
            sb.Append("  Price: ").Append(Price).Append("\n");
            sb.Append("  Quantity of stock: ").Append(StockQuantity).Append("\n");
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

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as Product);
        }

        /// <summary>
        /// Returns true if Product instances are equal
        /// </summary>
        /// <param name="input">Instance of Product to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Product input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.Barcode == input.Barcode ||
                    (this.Barcode != null &&
                    this.Barcode.Equals(input.Barcode))
                ) && 
                (
                    this.Price == input.Price ||
                    (this.Price != null &&
                    this.Price.Equals(input.Price))
                ) && 
                (
                    this.StockQuantity == input.StockQuantity ||
                    (this.StockQuantity != null &&
                    this.StockQuantity.Equals(input.StockQuantity))
                ) && 
                (
                    this.Category == input.Category ||
                    (this.Category != null &&
                    this.Category.Equals(input.Category))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.Barcode != null)
                    hashCode = hashCode * 59 + this.Barcode.GetHashCode();
                if (this.Price != null)
                    hashCode = hashCode * 59 + this.Price.GetHashCode();
                if (this.StockQuantity != null)
                    hashCode = hashCode * 59 + this.StockQuantity.GetHashCode();
                if (this.Category != null)
                    hashCode = hashCode * 59 + this.Category.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
