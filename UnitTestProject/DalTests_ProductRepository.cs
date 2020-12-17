using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace UnitTestProject
{
    [TestClass()]
    public class DalTests_ProductRepository
    {
        Category category;
        Product product;


        public DalTests_ProductRepository()
        {
            category = new Category() { CategoryName = "TestCategory" + Guid.NewGuid().ToString().Substring(0,8)};
            product = new Product();
            product.Barcode = DateTime.Now.Millisecond;
            product.ImageUrl = "asdf.jpg";
            product.ProductName = "TestProduct" + Guid.NewGuid().ToString().Substring(0, 8);
            product.ProductPrice = 99;
            product.StockQuantity = 99;
        }
        [TestMethod]
        public void DBConnectionTest_ConnectionIsRunning_ConnectionEqualsTo()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();

            //Act

            //Assert
            Assert.AreEqual("Open", pr.GetConnectionState());
        }

        [TestMethod()]
        public void InsertCategoryTest()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();

            //Act
            Category insertedCat = pr.InsertCategory(category);
            //Assert
            Assert.IsNotNull(insertedCat.CategoryId);
            Assert.AreEqual(category.CategoryName, insertedCat.CategoryName);

        }

        [TestMethod()]
        public void UpdateCategoryTest()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();

            //Act
            Category cat = pr.GetCategoryByName("Dairy");
            cat.CategoryName = "Diary";
            bool isUpdated = pr.UpdateCategory(cat);

            //Assert
            Assert.IsTrue(isUpdated);

            //Set it back
            cat.CategoryName = "Dairy";
            pr.UpdateCategory(cat);

        }

        [TestMethod()]
        public void GetAllCategoriesTest()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();

            //Act
            List<Category> cats = pr.GetAllCategories();

            //Assert
            Assert.IsTrue(cats.Count > 0);
        }

        [TestMethod()]
        public void GetAllProductsTest()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();

            //Act
            List<Product> products = pr.GetAllProducts();

            //Assert
            Assert.IsTrue(products.Count > 0);
        }

        [TestMethod()]
        public void GetProductByIdTest()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();

            //Act
            Product product = pr.GetProductById(1);

            //Assert
            Assert.IsTrue(product.ProductId > 0);
        }

        [TestMethod()]
        public void GetProductByNameTest()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();

            //Act
            List<Product> products = pr.GetProductByName("Milk");

            //Assert
            Assert.IsTrue(products.Count > 0);
        }


        [TestMethod()]
        public void InsertProductTest()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();

            //Act
            product.Category = pr.GetCategoryByName("Dairy");
            Product insertedProduct = pr.InsertProduct(product);
            //Assert
            Assert.IsNotNull(insertedProduct.ProductId);
        }

        [TestMethod()]
        public void UpdateProductTest()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();

            //Act
            Product oldProduct = pr.GetProductById(1);
            Product newProduct = oldProduct;
            newProduct.ProductPrice += 1;
            bool isUpdated = pr.UpdateProduct(newProduct);

            //Assert
            Assert.IsTrue(isUpdated);
        }
    }
}