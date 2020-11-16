using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class ProductManagement
    {

        public IEnumerable<Product> GetAll()
        {
            IProductRepository pRepo = new ProductRepository();
            return  pRepo.GetAllProducts();
            
        }

        public Product DeleteProduct(int productID)
        {
            throw new NotImplementedException();
        }

        public Product UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Product InsertProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int productID)
        {
            throw new NotImplementedException();
        }
    }
}
