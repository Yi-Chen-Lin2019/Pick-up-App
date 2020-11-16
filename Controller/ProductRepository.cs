
using Model;
using System.Collections.Generic;

namespace BusinessLayer
{
    internal class ProductRepository : IProductRepository
    {
        public Product DeleteProduct(int id)
        {
            throw new System.NotImplementedException();
        }

        public Product Get(int productID)
        {
            throw new System.NotImplementedException();
        }

        public List<Product> GetAllProducts()
        {
            return new List<Product> 
                {
            new Product {
                Name = "Milk",
                Barcode = 12345,
                Price = 100,
                StockQuantity = 500,
                Category = new Category ("grocery")
        },
                new Product {
                 Name = "Chocolate",
                Barcode = 66666,
                Price = 100,
                StockQuantity = 500,
                Category = new Category ("grocery")
                }
        
            };
        }

        public Product GetProductById(int productID)
        {
            throw new System.NotImplementedException();
        }

        public Product InsertProduct(Product product)
        {
            throw new System.NotImplementedException();
        }

        public Product UpdateProduct(int id, Product product)
        {
            throw new System.NotImplementedException();
        }

        public Product UpdateProduct(Product product)
        {
            throw new System.NotImplementedException();
        }
    }
}