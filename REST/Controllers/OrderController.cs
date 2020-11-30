
using BusinessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        /// <response code = "200">Orders found</response>
        [Route("Orders")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Order>))]
        public IHttpActionResult Get()
        {
            try
            {
                OrderManagement om = new OrderManagement();
                IEnumerable<Order> foundOrders = om.GetAllOrders();
                return Ok(foundOrders);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
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
            try
            {
                OrderManagement om = new OrderManagement();
                Order result = om.GetOrderById(orderID);
                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
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
            try
            {
                if (null == order)
                {
                    throw new Exception();
                }
                OrderManagement om = new OrderManagement();
                Order result = om.InsertOrder(order);
                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
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
        public IHttpActionResult Put(int orderID, [FromBody] Order order)
        {
            if (orderID != order.OrderId || null == order) { return BadRequest(); };
            try
            {
                OrderManagement om = new OrderManagement();
                bool result = om.UpdateOrder(order);
            }
            catch (SqlException)
            {
                return InternalServerError();
            }
            return Ok();
        }

        /*
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
            OrderManagement om = new OrderManagement();
            Order result = om.DeleteOrder(orderID);
            if (result == null) { return InternalServerError(); }
            else { return Ok(result); }
        }
        */
    }
}

