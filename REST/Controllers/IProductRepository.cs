using REST.Models;
using System.Collections.Generic;

namespace REST.Controllers
{
    internal interface IProductRepository
    {
        void InsertProduct(Product product);
        Product Get(int productID);
        Product GetProductById(int productID);
        List<Product> GetAllProducts();
    }
}