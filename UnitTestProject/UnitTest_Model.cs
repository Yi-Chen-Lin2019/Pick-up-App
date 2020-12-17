using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest_Model
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
            //Act
            Person customer = new Person("1", "John", "Buyer", "nice@email.com", "982838338");
            Person employee = new Person("2", "Adam", "Seller", "amazing@email.com", "754474888");
            Category category = new Category(0, "Beauty");
            Product product = new Product("Perfume", 555, 500, 30, "./perfume.png");
            product.Category = category;
            OrderLine orderLine = new OrderLine(2, product);            
            Order order = new Order("Receive");
            order.AddOrderLine(orderLine);
  
            //Assert
            Assert.IsNotNull(customer, "Creation Fail!");
            Assert.IsNotNull(employee, "Creation Fail!");

            Assert.IsNotNull(category, "Creation Fail!");
            Assert.IsNotNull(orderLine, "Creation Fail!");

            Assert.IsNotNull(order, "Creation Fail!");
            Assert.IsNotNull(order.OrderLineList, "List is equal to null!");
        }
    }
}
    

