using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public interface IProductRepository
    {
        List<Category> GetAllCategories();
        Category GetCategoryByName(String name);
        bool UpdateCategory(Category category);
        Category InsertCategory(Category category);
        //bool DeleteCategory(Category category);

        //List<SNProduct> GetAllSNProducts();
        //SNProduct GetSNProductBySerialNumber(String serialNumber);
        //SNProduct InsertSNProduct(SNProduct snProduct);
        //bool UpdateSNProduct(SNProduct snProduct);

        //List<NoSNProduct> GetAllNoSNProduct();
        //List<NoSNProduct> GetNoSNProductByProductId(int productId);
        //NoSNProduct InsertNoSNProduct(NoSNProduct noSNProduct);
        //bool UpdateNoSNProduct(NoSNProduct noSNProduct);

        List<Product> GetAllProducts();
        Product GetProductById(int productId);
        List<Product> GetProductByName(String productName);
        List<Product> GetAllProductsFromCategory(String categoryName);
        Product InsertProduct(Product product);
        bool UpdateProduct(Product product);
        
    }
}
