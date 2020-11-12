using System;
using System.Collections.Concurrent;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Model
{
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void StatusGet_StatusIsReceived_ReturnsReceived()
        {
            //Arrange
            var order = new Order("Received");
            //Act

            //Assert
        }
    }
}
