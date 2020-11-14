using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace REST.Controllers
{
    public class OrderController : ApiController
    {
        /// <summary>
        /// Get all  orders. 
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>List&lt;Order&gt;</returns>
        [Route("Orders")]
        [HttpGet]
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
        [Route("Orders/{orderID}")]
        [HttpGet]
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
        [Route("Orders")]
        [HttpPost]
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
        /// <param name="order">Order to update (optional)</param>
        /// <returns></returns>
        [Route("Orders")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Order order)
        {
            IOrderRepository prepo = new OrderRepository();
            Order result = prepo.UpdateOrder(id, order);
            if (result == null) { return InternalServerError(); }
            else { return Ok(result); }
        }

        /// <summary>
        /// Delete an order in the system
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="orderID">Order ID.</param>
        /// <returns></returns>
        [Route("Orders")]
        [HttpDelete]
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            IOrderRepository prepo = new OrderRepository();
            Order result = prepo.DeleteOrder(id);
            if (result == null) { return InternalServerError(); }
            else { return Ok(result); }
        }
    }
}
}
