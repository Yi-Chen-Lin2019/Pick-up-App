using DAL;
using Model;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class CategoryManagement
    {
        public IEnumerable<Category> GetAllCategories()
        {
            IProductRepository pRepo = new ProductRepository();
            return pRepo.GetAllCategories();

        }

        /*
        public bool DeleteCategory(int categoryID)
        {
            IProductRepository pRepo = new ProductRepository();
            return pRepo.DeleteCategory(productID);
        }
        */
        public bool UpdateCategory(Category product)
        {
            IProductRepository pRepo = new ProductRepository();
            return pRepo.UpdateCategory(product);
        }

        public Category InsertCategory(Category product)
        {
            IProductRepository pRepo = new ProductRepository();
            return pRepo.InsertCategory(product);
        }

        public Category GetCategoryByName(string categoryName)
        {
            IProductRepository pRepo = new ProductRepository();
            return pRepo.GetCategoryByName(categoryName);
        }
    }
}