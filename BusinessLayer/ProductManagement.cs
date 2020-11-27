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
            IEnumerable<Product> foundProducts;
            try
            {
                IProductRepository pRepo = new ProductRepository();
                foundProducts = pRepo.GetAllProducts();
            }
            catch
            {
                foundProducts = null;
            }
            return foundProducts;
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
            return pRepo.UpdateProduct(product, product.RowIdBig);
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
    }
}
