using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using DAL;

namespace Model.Testing
{
    [TestClass]
    public class DalTests_OrderRepository
    {
        [TestMethod]
        public void OrderTests() 
        {
            InsertOrder_OrderIsInserted();
        }
        private void InsertOrder_OrderIsInserted()
        {
            //Arrange
            PersonRepository perR = new PersonRepository();
            ProductRepository prodR = new ProductRepository();
            OrderRepository ordR = new OrderRepository();

            if (perR.GetPersonByEmail("mail@employee.com") == null) {
                Order order = new Order("Received");

                //Create OrderLine and add to order
                Category category = new Category("Sex Toys");
                prodR.InsertCategory(category);
                Product product1 = new Product("Condom", 34455, 2.99m, 1);
                product1.Category = prodR.GetCategoryByName("Sex Toys");
                prodR.InsertProduct(product1);
                NoSNProduct noSNProduct = new NoSNProduct(prodR.GetProductByName("Condom")[0].ProductId, "Condom", 34455, 2.99m, 1);
                prodR.InsertNoSNProduct(noSNProduct);
                OrderLine orderLine = new OrderLine(12);
                orderLine.NoSNProduct = prodR.GetNoSNProductByProductId(prodR.GetProductByName("Condom")[0].ProductId)[0];
                order.AddOrderLine(orderLine);

                //Create SNProduct and add to order
                Product product2 = new Product("Dildo", 12332545, 450.50m, 20);
                product2.Category = prodR.GetCategoryByName("Sex Toys");
                prodR.InsertProduct(product2);
                SNProduct snProduct = new SNProduct("EHFBNNA7432");
                snProduct.Product = prodR.GetProductByName("Dildo")[0];
                prodR.InsertSNProduct(snProduct);
                order.AddSNProduct(prodR.GetSNProductBySerialNumber("EHFBNNA7432"));

                //Create Customer and Employee and assign to order
                Person customer = new Person("mail@customer.com", "John", "Buyer", 847757784);
                perR.InsertPerson(customer);
                perR.InsertCustomerRoleToPerson(perR.GetPersonByEmail("mail@customer.com"));
                order.Customer = perR.GetPersonByEmail("mail@customer.com");

                Person employee = new Person("mail@employee.com", "Will", "Seller", 33215512);
                perR.InsertPerson(employee);
                perR.InsertEmployeeRoleToPerson(perR.GetPersonByEmail("mail@employee.com"));
                order.Employee = perR.GetPersonByEmail("mail@employee.com");

                //Time and Total Price
                order.PickUpTime = DateTime.Now;
                NoSNProduct tempProduct = prodR.GetNoSNProductByProductId(prodR.GetProductByName("Condom")[0].ProductId)[0];
                order.TotalPrice = prodR.GetSNProductBySerialNumber("EHFBNNA7432").Product.ProductPrice + (tempProduct.ProductPrice * orderLine.Quantity);

                //Act
                bool isSuccessful = ordR.InsertOrder(order);

                //Assert
                Assert.IsTrue(isSuccessful);
            }
            //Assert
            Assert.IsTrue(true);
        }
    }
}
