
using System.Collections.Generic;

namespace BusinessLayer
{
    internal interface IProductRepository
    {
        Product InsertProduct(Product product);
        Product Get(int productID);
        Product GetProductById(int productID);
        List<Product> GetAllProducts();
        Product UpdateProduct(int id, Product product);
        Product DeleteProduct(int id);
        Product UpdateProduct(Product product);
    }
}