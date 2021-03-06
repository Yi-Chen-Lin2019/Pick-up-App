﻿
using BusinessLayer;
using DAL;
using Microsoft.AspNet.Identity;
using Model;
using REST.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace REST.Controllers
{
    public class OrderController : BaseApiController
    {
        /// <summary>
        /// Get all  orders. 
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>List&lt;Order&gt;</returns>
        /// <response code = "200">Orders found</response>
        [Route("Orders")]
        [HttpGet]
        [Authorize(Roles = "Employee")]
        [ResponseType(typeof(IEnumerable<Order>))]
        public IHttpActionResult Get()
        {
            try
            {
                OrderManagement om = new OrderManagement();
                IEnumerable<Order> foundOrders = om.GetAllOrders();
                return Ok(foundOrders);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Get specific customer's orders. 
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="userId">Put in user ID.</param>
        /// <returns>List&lt;Order&gt;</returns>
        /// <response code = "200">Orders found</response>
        [Route("Orders/UserId")]
        [HttpGet]
        [Authorize(Roles = "Customer")]
        [ResponseType(typeof(IEnumerable<Order>))]
        public IHttpActionResult GetMyOrder()
        {  
            try
                {
                    OrderManagement om = new OrderManagement();
                    IEnumerable<Order> foundOrders = om.GetMyOrders(RequestContext.Principal.Identity.GetUserId());
                    return Ok(foundOrders);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
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
        [Authorize(Roles = "Employee")]
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
        [Authorize(Roles = "Customer")]
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> PostAsync([FromBody] Order order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest();
                }
                order.Customer = new Person() { Id = RequestContext.Principal.Identity.GetUserId() };
                order.OrderedTime = System.DateTime.Now;
                order.OrderStatus = "Received";
                OrderManagement om = new OrderManagement();
                Order result = om.InsertOrder(order);

                //send confirm email
                await AppUserManager.SendEmailAsync(
                    order.Customer.Id, "Your Order Confirmation",
                    "Hello " + order.Customer.FirstName + " " + order.Customer.LastName + " ! "+
                    "Thank you for your order, your order is received and it is now being processed");
                return Ok(result);
            }
            catch (OutOfStockException ex)
            {
                return Conflict();
            }
            catch (Exception ex)
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
        [Authorize(Roles = "Employee")]
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> PutAsync(int orderID, [FromBody] Order order)
        {
            bool result = false;
            if (orderID != order.OrderId || null == order) { return BadRequest(); };
            try
            {
                OrderManagement om = new OrderManagement();
                order.Employee = new Person() { Id = RequestContext.Principal.Identity.GetUserId() };
                result = om.UpdateOrder(order);
            }
            catch (SqlException)
            {
                return InternalServerError();
            }
            if (result)
            {
                //send update email
                await AppUserManager.SendEmailAsync(
                    order.Customer.Id, "Your order status",
                    "Hello " + order.Customer.FirstName + " " + order.Customer.LastName + " ! " +
                    "Order ID: " + order.OrderId +
                    " is now " + order.OrderStatus);
                return Ok();
            }
            else
            {
                return Conflict();
            }
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

