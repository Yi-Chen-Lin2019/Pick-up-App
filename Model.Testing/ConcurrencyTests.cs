using System;
using System.Collections.Generic;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Model.Testing
{
    [TestClass]
    public class ConcurrencyTests
    {
        [TestMethod]
        [ExpectedException(typeof(OutOfStockException))]
        public void InsertOrder_OrderInserted()
        {
            //Arrange
            PersonRepository perR = new PersonRepository();
            ProductRepository prodR = new ProductRepository();
            OrderRepository ordR = new OrderRepository();

            Order order = new Order("Received");
            

            List<OrderLine> orderLines = new List<OrderLine>()
            {
                new OrderLine(){
                    NoSNProduct = prodR.GetNoSNProductByProductId(5)[0],
                    Quantity = 50
                },

                new OrderLine()
                {
                    NoSNProduct = prodR.GetNoSNProductByProductId(6)[0],
                    Quantity = 50
                }
            };
            order.OrderLineList = orderLines;
           

            order.Customer = perR.GetPersonById(1);
            order.Employee = perR.GetPersonById(2);
           

            //Time and Total Price
            order.OrderedTime = DateTime.Now;
            order.PickUpTime = DateTime.Now;
            order.PickUpTime.AddHours(2);
            order.PickUpTime.AddDays(1);
            
            
            

            //Act
            Order retrievedOrder = ordR.InsertOrder(order);
            


            //Assert
            Assert.AreEqual(659.5M, retrievedOrder.TotalPrice);

        }
    }
}
