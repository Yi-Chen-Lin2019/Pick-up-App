using REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace REST.Controllers
{
    public class OrderController : ApiController
    {
        /// <summary>
        /// Get all  orders. 
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>List&lt;Order&gt;</returns>
        /// <response code = "200"></response>
        [Route("Orders")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Order>))]
        public IHttpActionResult Get()
        {
            IOrderRepository pRepo = new OrderRepository();
            IEnumerable<Order> foundOrders = pRepo.GetAllOrders();
            if (foundOrders == null) { return InternalServerError(); }
            else { return Ok(foundOrders); }
        }


        /// <summary>
        /// By passing in the order ID, you can get the order of the order ID in the system. 
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="orderID">Put in order ID.</param>
        /// <returns>List&lt;Order&gt;</returns>
        /// <response code = "200">Order found</response>
        /// <response code = "404">Order not found</response>
        [Route("Orders/{orderID}")]
        [HttpGet]
        [ResponseType(typeof(Order))]
        public IHttpActionResult Get(int orderID)
        {
            IOrderRepository prepo = new OrderRepository();
            Order result = prepo.GetOrderById(orderID);
            if (result == null) { return InternalServerError(); }
            else { return Ok(result); }

        }

        /// <summary>
        /// Add an order to the system
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="order">Order to add (optional)</param>
        /// <returns></returns>
        /// <response code = "201">Order created</response>
        [Route("Orders")]
        [HttpPost]
        [ResponseType(typeof(Order))]
        public IHttpActionResult Post([FromBody] Order order)
        {
            IOrderRepository prepo = new OrderRepository();
            Order result = prepo.InsertOrder(order);
            if (result == null) { return InternalServerError(); }
            else { return Ok(result); }
        }

        /// <summary>
        /// Update an order by ID
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="orderID">Order ID</param>
        /// <param name="order">Order to update</param>
        /// <returns></returns>
        /// <response code = "200">Order updated</response>
        [Route("Orders/{orderID}")]
        [HttpPut]
        [ResponseType(typeof(Order))]
        public IHttpActionResult Put([FromBody] Order order)
        {
            IOrderRepository prepo = new OrderRepository();
            Order result = prepo.UpdateOrder(order);
            if (result == null) { return InternalServerError(); }
            else { return Ok(result); }
        }

        /// <summary>
        /// Delete an order in the system
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="orderID">Order ID.</param>
        /// <returns></returns>
        /// <response code = "200">Order deleted</response>
        [Route("Orders/{orderID}")]
        [HttpDelete]
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int orderID)
        {
            IOrderRepository prepo = new OrderRepository();
            Order result = prepo.DeleteOrder(orderID);
            if (result == null) { return InternalServerError(); }
            else { return Ok(result); }
        }
    }
}

