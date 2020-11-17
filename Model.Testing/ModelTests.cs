using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace Model.Testing
{
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void OrderStatusReturn_StatusIsReceived_StatusEqualsToReceived()
        {
            //Arrange
            var order = new Order("Received");

            //Act
            String status = order.OrderStatus;

            //Assert
            Assert.AreEqual("Received", status, "Values are not equal.");
        }
        [TestMethod]
        public void OrderStatusSet_SettingStatusToReceived_StatusEqualsToReceived()
        {
            //Arrange
            var order = new Order("");

            //Act
            order.OrderStatus = "Received";

            //Assert
            Assert.AreEqual("Received", order.OrderStatus);
        }

        [TestMethod]
        public void ModelCreate_CreatingAllModelObjects_ObjectsCreatedSuccessfuly()
        {
            //Arrange
            Person customer = new Person(1, "John", "Buyer", "nice@email.com", 982838338);
            customer.CustomerRole = new CustomerRole(1);
            Person employee = new Person(2, "Adam", "Seller", "amazing@email.com", 754474888);
            employee.EmployeeRole = new EmployeeRole(1);

            Category category = new Category(0, "Sex Toys");
            NoSNProduct noSNProduct = new NoSNProduct(0, "Condom", 34455, 2.99m, 1);
            SNProduct snProduct = new SNProduct("234234");
            snProduct.Product = new Product(1, "Dildo", 56664, 169m, 1);
            OrderLine orderLine = new OrderLine(12);
            orderLine.NoSNProduct = noSNProduct;

            Order order = new Order("Ready");

            order.AddOrderLine(orderLine);
            order.AddSNProduct(snProduct);

            //Act

            //Assert
            Assert.IsNotNull(customer, "Creation Fail!");
            Assert.IsNotNull(employee, "Creation Fail!");

            Assert.IsNotNull(category, "Creation Fail!");
            Assert.IsNotNull(noSNProduct, "Creation Fail!");
            Assert.IsNotNull(snProduct, "Creation Fail!");
            Assert.IsNotNull(orderLine, "Creation Fail!");

            Assert.IsNotNull(order, "Creation Fail!");
            Assert.IsNotNull(order.OrderLineList, "List is equal to null!");
            Assert.IsNotNull(order.SnProductList, "List is equal to null!");
        }
    }
}
