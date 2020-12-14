﻿/* 
 * Pick-up API
 *
 * This is an API for pick-up app which is created for 3rd semester project in UCN Computer Science Programme.
 *
 * OpenAPI spec version: 1.0.0
 * Contact: 1081501@ucn.dk
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using BusinessLayer;
using Model;
using Swashbuckle.Swagger.Annotations;
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

    public class ProductController : ApiController
    {
        /// <summary>
        /// Get all products
        /// </summary>
        /// <remarks>
        /// Get a list of products
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>List&lt;Product&gt;</returns>
        /// <response code = "200">Products found</response>
        [Route("Products")]
        [HttpGet]
        [AllowAnonymous]
        [ResponseType(typeof(IEnumerable<Product>))]
        public IHttpActionResult Get()
        {
            try
            {
                ProductManagement pm = new ProductManagement();
                IEnumerable<Product> foundProducts = pm.GetAllProducts();
                return Ok(foundProducts);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }


        /// <summary>
        /// By passing in the product ID, you can get the product of the product ID in the system. 
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="productID">Put in product ID.</param>
        /// <returns>List&lt;Product&gt;</returns>
        /// <response code = "200">Product found</response>
        /// <response code = "404">Product not found</response>
        [Route("Products/{productID}")]
        [HttpGet]
        [AllowAnonymous]
        [ResponseType(typeof(Product))]
        public IHttpActionResult Get(int productID)
        {
            try
            {
                ProductManagement pm = new ProductManagement();
                Product result = pm.GetProductById(productID);
                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Add a product to the system
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="product">Product to add (optional)</param>
        /// <returns></returns>
        /// <response code = "201">Product created</response>
        [Route("Products")]
        [HttpPost]
        [Authorize(Roles = "Employee")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult Post([FromBody] Product product)
        {
            try
            {
                if (null == product){ return BadRequest();}
                ProductManagement pm = new ProductManagement();
                Product result = pm.InsertProduct(product);
                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Update a product by ID
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="productID">Product ID</param>
        /// <param name="product">Product to update</param>
        /// <returns></returns>
        /// <response code = "200">Product updated</response>
        [Route("Products/{productID}")]
        [HttpPut]
        [Authorize(Roles = "Employee")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult Put(int productID, [FromBody] Product product)
        {
            if (productID != product.ProductId || null == product) { return BadRequest(); };
            bool result;
            try
            {
                ProductManagement pm = new ProductManagement();
                result = pm.UpdateProduct(product);
            }
            catch (SqlException)
            {
                return InternalServerError();
            }
            if (result)
            {
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }


        /// <summary>
        /// Delete a product in the system
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="productID">Product ID.</param>
        /// <returns></returns>
        /// <response code = "200">Product deleted</response>
        //[Route("Products/{productID}")]
        //[HttpDelete]
        //// DELETE api/<controller>/5
        //public IHttpActionResult Delete(int productID)
        //{
        //    ProductManagement pm = new ProductManagement();
        //    Product result = pm.DeleteProduct(productID);
        //    if (result == null) { return InternalServerError(); }
        //    else { return Ok(result); }
        //}

    }
}