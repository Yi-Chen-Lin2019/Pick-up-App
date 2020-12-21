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
            IEnumerable<Category> foundCategories;
            try
            {
                IProductRepository pRepo = new ProductRepository();
                foundCategories = pRepo.GetAllCategories();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                foundCategories = null;
            }
            return foundCategories;

        }

        /*
        public bool DeleteCategory(int categoryID)
        {
            IProductRepository pRepo = new ProductRepository();
            return pRepo.DeleteCategory(productID);
        }
        */
        public bool UpdateCategory(Category category)
        {
            IProductRepository pRepo = new ProductRepository();
            return pRepo.UpdateCategory(category);
        }

        public Category InsertCategory(Category category)
        {
            IProductRepository pRepo = new ProductRepository();
            return pRepo.InsertCategory(category);
        }

        public Category GetCategoryByName(string categoryName)
        {
            IProductRepository pRepo = new ProductRepository();
            return pRepo.GetCategoryByName(categoryName);
        }
    }
}