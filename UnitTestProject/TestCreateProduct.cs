using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class TestCreateProduct
    {
        [TestMethod]
        public void TestValidProductInput() {
            //arrange
            string name = "Milk";
            string barcode = "09567432Z";
            decimal price = 15;
            long stockQuantity = 100;
            string category = "grocery";
            ProductController pCtr = new ProductController();
            bool expected = true;

            //act
            Product p = new Product(name, barcode, price, stockQuantity, category);
            bool status = pCtr.AddProduct(p);

            //assert
            Assert.AreEqual(expected, status, "order created correctly");
        }

        [TestMethod]
        public void TestInValidPriceProductInput()
        {
            //arrange
            string name = "Milk";
            string barcode = "09567432Z";
            decimal price = -15;
            long stockQuantity = 100;
            string category = "grocery";
            ProductController pCtr = new ProductController();
            bool expected = false;

            //act
            var p = new Product(name, barcode, price, stockQuantity, category);
            bool status = pCtr.AddProduct(p);

            //assert
            Assert.AreEqual(expected, status, "order created correctly");
        }

        [TestMethod]
        public void TestInValidStockQuantityProductInput()
        {
            //arrange
            string name = "Milk";
            string barcode = "09567432Z";
            decimal price = 15;
            long stockQuantity = -100;
            string category = "grocery";
            ProductController pCtr = new ProductController();
            bool expected = false;

            //act
            var p = new Product(name, barcode, price, stockQuantity, category);
            bool status = pCtr.AddProduct(p);

            //assert
            Assert.AreEqual(expected, status, "order created correctly");
        }

        [TestMethod]
        public void TestInValidCategoryProductInput()
        {
            //arrange
            string name = "Milk";
            string barcode = "09567432Z";
            decimal price = 15;
            long stockQuantity = 100;
            string category = null;
            ProductController pCtr = new ProductController();
            bool expected = false;

            //act
            var p = new Product(name, barcode, price, stockQuantity, category);
            bool status = pCtr.AddProduct(p);

            //assert
            Assert.AreEqual(expected, status, "order created correctly");
        }

    }

}
