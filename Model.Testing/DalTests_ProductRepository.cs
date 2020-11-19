using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using Model;
using System.Collections.Generic;

namespace Model.Testing
{
    [TestClass]
    public class DalTests_ProductRepository
    {
        //Category TESTS
        [TestMethod]
        public void CategoryTests()
        {
            CreateCategoryTest_CategoryIsCreated();
            UpdateCategoryTest_CategoryIsUpdated();
            GetAllCategories_AllCategoriesAreInTheList();
            DeleteCategory_CategoryIsDeleted();
        }
        private void CreateCategoryTest_CategoryIsCreated()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();
            Category category = new Category("Meat");

            //Act
            Category retrievedCategory = pr.InsertCategory(category);

            //Assert
            Assert.IsNotNull(retrievedCategory);
            Assert.AreEqual("Meat", pr.GetCategoryByName("Meat").CategoryName);
        }

        private void UpdateCategoryTest_CategoryIsUpdated()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();
            Category category = pr.GetCategoryByName("Meat");
            category.CategoryName = "Toys";

            //Act
            bool ifSuccess = pr.UpdateCategory(category);

            //Assert
            Assert.IsTrue(ifSuccess);
            Assert.AreEqual("Toys", pr.GetCategoryByName("Toys").CategoryName);
        }

        private void GetAllCategories_AllCategoriesAreInTheList()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();
            List<Category> list = new List<Category>();

            //Act
            list = pr.GetAllCategories();

            //Assert
            Assert.AreEqual("Toys", list[0].CategoryName);
        }

        private void DeleteCategory_CategoryIsDeleted()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();
            Category category;

            //Act
            category = pr.GetCategoryByName("Toys");
            bool isSuccess = pr.DeleteCategory(category);

            //Assert
            Assert.IsTrue(isSuccess);
        }

        //--------------------------------------------------------------------------------------
        //SN Products Test
        [TestMethod]
        public void SNProductTest()
        {
            InsertProduct_ProductIsInserted();
            InsertSNProduct_SNProductIsInserted();
            //Needs order to function
            //UpdateSNProduct_SNProductIsUpdated();
            GetSnProduct_SNProductIsRetreived();
            GetAllSNProduct_AllSnProductsAreRetreived();
        }
        
        private void InsertProduct_ProductIsInserted()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();
            if (pr.GetProductByName("Milk") == null)
            {
                //Arrange
                Product product = new Product("Milk", 933434, 2.99m, 500);
                if (pr.GetCategoryByName("Dairy") == null)
                {
                    Category category = new Category("Dairy");
                    pr.InsertCategory(category);
                }
                product.Category = pr.GetCategoryByName("Dairy");

                //Act
                Product retrievedProduct = pr.InsertProduct(product);
                //Assert
                Assert.IsNotNull(retrievedProduct);
            }
            //Assert
            Assert.IsNotNull(pr.GetProductByName("Milk"));
            Assert.AreEqual("Milk", pr.GetProductByName("Milk")[0].ProductName);
        }

        private void InsertSNProduct_SNProductIsInserted()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();

            if (pr.GetSNProductBySerialNumber("ENFN4883") == null)
            {
                //Arrange
                SNProduct snProduct = new SNProduct("ENFN4883");
                snProduct.Product = pr.GetProductByName("Milk")[0];

                //Act
                SNProduct retrievedSNProduct = pr.InsertSNProduct(snProduct);

                //Assert
                Assert.IsNotNull(retrievedSNProduct);
            }
            //Assert
            Assert.AreEqual("ENFN4883", pr.GetSNProductBySerialNumber("ENFN4883").SerialNumber);
        }
        private void UpdateSNProduct_SNProductIsUpdated()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();
            SNProduct snProduct = pr.GetSNProductBySerialNumber("ENFN4883");
            snProduct.OrderId = 1;

            //Act
            bool isSuccess = pr.UpdateSNProduct(snProduct);

            //Assert
            Assert.IsTrue(isSuccess);
            Assert.AreEqual("1", pr.GetSNProductBySerialNumber("ENFN4883").OrderId);
        }

        private void GetSnProduct_SNProductIsRetreived()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();

            //Act
            SNProduct snProduct = pr.GetSNProductBySerialNumber("ENFN4883");

            //Assert
            Assert.IsNotNull(snProduct);
        }
        private void GetAllSNProduct_AllSnProductsAreRetreived()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();

            //Act
            List<SNProduct> snProduct = pr.GetAllSNProducts();

            //Assert
            Assert.IsNotNull(snProduct);
        }
        //--------------------------------------------------------------------------------------------------
        //NoSNProductTEST
        [TestMethod]
        public void NoSNProductTest()
        {
            InsertNoSNProduct_NoSNProductIsInserted();
            //NoSNProduct has no additional variables to update yet
            //UpdateNoSNProduct_NoSNProductIsUpdated();
            GetAllNoSNProducts_NoSNProductListIsRetrieved();
        }

        private void InsertNoSNProduct_NoSNProductIsInserted()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();
            if (pr.GetAllNoSNProduct() != null)
            {
                NoSNProduct product = new NoSNProduct(0, "Bread", 666464, 0.8m, 100);
                Category category = new Category("Bakery");
                pr.InsertCategory(category);
                product.Category = pr.GetCategoryByName("Bakery");

                //Act
                pr.InsertProduct((Product)product);
                product.ProductId = pr.GetProductByName("Bread")[0].ProductId;
                NoSNProduct retrievedNoSNProduct = pr.InsertNoSNProduct(product);

                //Assert
                Assert.IsNotNull(retrievedNoSNProduct);
            }
            //Assert
            Assert.AreEqual("Bread", pr.GetAllNoSNProduct()[0].ProductName);
        }

        private void UpdateNoSNProduct_NoSNProductIsUpdated()
        {
            throw new NotImplementedException();
            //Arrange
            ProductRepository pr = new ProductRepository();
            NoSNProduct product = pr.GetAllNoSNProduct()[0];
            //CHANGE ONE VARIABLE IN HERE

            //Act
            bool isSuccess = pr.UpdateNoSNProduct(product);

            //Assert
            Assert.IsTrue(isSuccess);
            //ASSERT CHANGED VARIABLE
            //Assert.AreEqual(*HERE*, pr.GetAllNoSNProduct()[0].ProductPrice);
        }

        private void GetAllNoSNProducts_NoSNProductListIsRetrieved()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();

            //Act
            List<NoSNProduct> productList = pr.GetAllNoSNProduct();

            //Assert
            Assert.IsNotNull(productList);
            Assert.AreEqual("Bread", productList[0].ProductName);
        }


        //--------------------------------------------------------------------------------------------------
        //Product TEST
        [TestMethod]
        public void ProductTest()
        {
            ProductUpdateTest_ProductWasUpdated();
            GetAllProducts_ProductListIsRetrieved();
            GetAllProductsFromCategory_ProductListIsRetrieved();
        }
        private void ProductUpdateTest_ProductWasUpdated()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();
            Product product = pr.GetProductByName("Milk")[0];
            product.ProductPrice = 1.99m;

            //Act
            bool isSuccessful = pr.UpdateProduct(product);

            //Assert
            Assert.IsTrue(isSuccessful);
            Assert.AreEqual(1.99m, pr.GetProductByName("Milk")[0].ProductPrice);
        }

        private void GetAllProducts_ProductListIsRetrieved()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();
            List<Product> productList;

            //Act
            productList = pr.GetAllProducts();

            //Assert
            Assert.IsNotNull(productList);
            Assert.AreEqual("Milk", productList[0].ProductName);
        }

        private void GetAllProductsFromCategory_ProductListIsRetrieved()
        {
            //Arrange
            ProductRepository pr = new ProductRepository();
            List<Product> productList;

            //Act
            productList = pr.GetAllProductsFromCategory("Dairy");

            //Assert
            Assert.IsNotNull(productList);
            Assert.AreEqual("Milk", productList[0].ProductName);
        }
    }
}
