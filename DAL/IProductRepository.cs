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
        bool InsertCategory(Category category);
        bool DeleteCategory(Category category);

        List<SNProduct> GetAllSNProducts();
        SNProduct GetSNProductBySerialNumber(String serialNumber);
        bool InsertSNProduct(SNProduct snProduct);
        bool UpdateSNProduct(SNProduct snProduct);

        List<NoSNProduct> GetAllNoSNProduct();
        List<NoSNProduct> GetNoSNProductByProductId(int productId);
        bool InsertNoSNProduct(NoSNProduct noSNProduct);
        bool UpdateNoSNProduct(NoSNProduct noSNProduct);

        List<Product> GetAllProducts();
        Product GetProductById(int productId);
        List<Product> GetProductByName(String productName);
        List<Product> GetAllProductsFromCategory(String categoryName);
        bool InsertProduct(Product product);
        bool UpdateProduct(Product product);
        
    }
}
