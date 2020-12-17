using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class DalTests_OrderRepository
    {
        private PersonRepository perR;
        private ProductRepository prodR;
        private OrderRepository ordR;
        private Order order;
        private Product product;
        private Person customer;
        private Person employee;

        
        public DalTests_OrderRepository()
        {
            perR = new PersonRepository();
            prodR = new ProductRepository();
            ordR = new OrderRepository();

            order = new Order("Received");

            //Create OrderLine and add to order
            product = prodR.GetProductById(5);
            OrderLine orderLine = new OrderLine(2, product);
            order.AddOrderLine(orderLine);

            customer = perR.GetPersonByEmail("superadmin@pickup.com");
            employee = perR.GetPersonByEmail("superadmin@pickup.com");
            order.Customer = customer;
            order.Employee = employee;

            //Time and Total Price
            order.OrderedTime = DateTime.Now;
            order.PickUpTime = DateTime.Now;
            order.PickUpTime.AddHours(2);
            order.PickUpTime.AddDays(1);


        }

        
        
        [TestMethod]
        public void InsertOrder_OrderIsInserted()
        {           
            //Act
            Order retrievedOrder = ordR.InsertOrder(order);

            //Assert
            Assert.IsNotNull(retrievedOrder);
            Assert.AreEqual(product.ProductPrice*2, retrievedOrder.TotalPrice);
            
        }
        [TestMethod]
        public void GetOrderById_OrderIsRetrieved()
        {
            //Arrange
            Order inserted = ordR.InsertOrder(order);

            //Act
            Order retrievedOrder = ordR.GetOrderById(inserted.OrderId);

            //Assert
            Assert.IsNotNull(retrievedOrder);
            Assert.AreEqual(product.ProductPrice * 2, retrievedOrder.TotalPrice);

        }
        [TestMethod]
        public void GetOrderByStatus_OrderIsRetrieved()
        {
            
            //Act
            List<Order> orders = ordR.GetOrdersByStatus("Received");

            //Assert
            Assert.IsNotNull(orders);
        }
        [TestMethod]
        public void GetAllOrders_OrderIsRetrieved()
        {
            //Arrange
            OrderRepository or = new OrderRepository();

            //Act
            List<Order> orders = or.GetAllOrders();

            //Assert
            Assert.IsNotNull(orders);
        }
        [TestMethod]
        public void UpdateOrder_OrderIsUpdated()
        {
            //Arrange
            Order insert = ordR.InsertOrder(order);

            Order retrievedorder = ordR.GetOrderById(insert.OrderId);
            retrievedorder.Employee = employee;
            retrievedorder.OrderStatus = "Confirmed";

            //Act
            bool isSuccess = ordR.UpdateOrder(retrievedorder);

            //Assert
            Assert.IsTrue(isSuccess);          
        }
    }

}
