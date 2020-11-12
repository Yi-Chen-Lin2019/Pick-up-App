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
            var order = new Order("Received", null, null);

            //Act
            String status = order.Status;

            //Assert
            Assert.AreEqual("Received", status, "Values are not equal.");
        }
        [TestMethod]
        public void OrderStatusSet_SettingStatusToReceived_StatusEqualsToReceived()
        {
            //Arrange
            var order = new Order("", null, null);

            //Act
            order.Status = "Received";

            //Assert
            Assert.AreEqual("Received", order.Status, "Values are not equal.");
        }

        [TestMethod]
        public void ModelCreate_CreatingAllModelObjects_ObjectsCreatedSuccessfuly()
        {
            //Arrange
            Person customer = new Person("John", "Buyer", "nice@email.com", 982838338);
            customer.CustomerRole = new CustomerRole();
            Person employee = new Person("Adam", "Seller", "amazing@email.com", 754474888);
            employee.EmployeeRole = new EmployeeRole();

            Category category = new Category("Sex Toys");
            NoSNProduct noSNProduct = new NoSNProduct("Condom", 34455, 2.99, category);
            SNProduct snProduct = new SNProduct(new Product("Dildo", 56664, 169, category), "A83JKUE24");
            OrderLine orderLine = new OrderLine(12, noSNProduct);

            Order order = new Order("Ready", customer, employee);

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
