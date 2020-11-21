using Model;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class ProductManagement
    {

        public IEnumerable<Product> GetAllProducts()
        {
            IProductRepository pRepo = new ProductRepository();
            return  pRepo.GetAllProducts();
            
        }

        /*
        public bool DeleteProduct(int productID)
        {
            IProductRepository pRepo = new ProductRepository();
            return pRepo.DeleteProduct(productID);
        }
        */
        public bool UpdateProduct(Product product)
        {
            IProductRepository pRepo = new ProductRepository();
            return pRepo.UpdateProduct(product);
        }

        public Product InsertProduct(Product product)
        {
            IProductRepository pRepo = new ProductRepository();
            return pRepo.InsertProduct(product);
        }

        public Product GetProductById(int productID)
        {
            IProductRepository pRepo = new ProductRepository();
            return pRepo.GetProductById(productID);
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId) {
            IProductRepository pRepo = new ProductRepository();
            return pRepo.GetProductsByCategoryId(categoryId);
        }
    }
}
