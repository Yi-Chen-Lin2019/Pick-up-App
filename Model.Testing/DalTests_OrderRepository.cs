using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using DAL;
using System.Collections.Generic;

namespace Model.Testing
{
    [TestClass]
    public class DalTests_OrderRepository
    {
        [TestMethod]
        public void OrderTests() 
        {
            InsertOrder_OrderIsInserted();
            GetOrderById_OrderIsRetrieved();
            GetOrderByStatus_OrderIsRetrieved();
            GetAllOrders_OrderIsRetrieved();
            UpdateOrder_OrderIsUpdated();
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
                order.OrderedTime = DateTime.Now;
                order.PickUpTime = DateTime.Now;
                order.PickUpTime.AddHours(2);
                order.PickUpTime.AddDays(1);
                NoSNProduct tempProduct = prodR.GetNoSNProductByProductId(prodR.GetProductByName("Condom")[0].ProductId)[0];
                order.TotalPrice = prodR.GetSNProductBySerialNumber("EHFBNNA7432").Product.ProductPrice + (tempProduct.ProductPrice * orderLine.Quantity);

                //Act
                Order retrievedOrder = ordR.InsertOrder(order);

                //Assert
                Assert.IsNotNull(retrievedOrder);
            }
            //Assert
            Assert.IsTrue(true);
        }
        private void GetOrderById_OrderIsRetrieved()
        {
            //Arrange
            OrderRepository or = new OrderRepository();

            //Act
            Order order = or.GetOrderById(1);

            //Assert
            Assert.IsNotNull(order);
        }
        private void GetOrderByStatus_OrderIsRetrieved()
        {
            //Arrange
            OrderRepository or = new OrderRepository();

            //Act
            List<Order> orders = or.GetOrdersByStatus("Received");

            //Assert
            Assert.IsNotNull(orders);
        }
        private void GetAllOrders_OrderIsRetrieved()
        {
            //Arrange
            OrderRepository or = new OrderRepository();

            //Act
            List<Order> orders = or.GetAllOrders();

            //Assert
            Assert.IsNotNull(orders);
        }
        private void UpdateOrder_OrderIsUpdated()
        {
            ProductRepository prodR = new ProductRepository();
            OrderRepository or = new OrderRepository();
            //if (prodR.GetNoSNProductByProductId(prodR.GetProductByName("Glow in Dark Condom")[0].ProductId)[0] == null) {
                //Arrange
                Order order = or.GetOrderById(1);
                order.OrderStatus = "Confirmed";
                order.OrderLineList[0].Quantity = 40;

                //Create SNProduct and add to order
                Product product2 = new Product("Vibrator", 1234045, 690.50m, 32);
                product2.Category = prodR.GetCategoryByName("Sex Toys");
                prodR.InsertProduct(product2);
                SNProduct snProduct = new SNProduct("EHTTT06");
                snProduct.Product = prodR.GetProductByName("Vibrator")[0];
                prodR.InsertSNProduct(snProduct);
                order.AddSNProduct(prodR.GetSNProductBySerialNumber("EHTTT06"));

                //Create OrderLine and add to order
                Product product1 = new Product("Glow in Dark Condom", 3445005, 10.00m, 945);
                product1.Category = prodR.GetCategoryByName("Sex Toys");
                prodR.InsertProduct(product1);
                NoSNProduct noSNProduct = new NoSNProduct(prodR.GetProductByName("Glow in Dark Condom")[0].ProductId, "Glow in Dark Condom", 3445005, 10.00m, 945);
                prodR.InsertNoSNProduct(noSNProduct);
                OrderLine orderLine = new OrderLine(90);
                orderLine.NoSNProduct = prodR.GetNoSNProductByProductId(prodR.GetProductByName("Glow in Dark Condom")[0].ProductId)[0];
                order.AddOrderLine(orderLine);

                //Act
                bool isSuccess = or.UpdateOrder(order);

                //Assert
                Assert.IsTrue(isSuccess);
            //}
            Assert.IsTrue(true);
        }
    }
}
