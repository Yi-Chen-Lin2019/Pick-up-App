﻿
using REST.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace REST.Controllers
{
    public class ProductController : ApiController
    {
        private static readonly IEnumerable<Product> Products = new List<Product>
        {
            new Product (
                "Milk",
                "12345",
                100,
                500,
                new Category("grocery")
                ),
                new Product (
                "chocolate",
                "6666",
                 100,
                 40,
                new Category( "snack")
                )
        };
        /// <summary>
        /// Get all products
        /// </summary>
        /// <remarks>
        /// Get a list of products
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>List&lt;Product&gt;</returns>
        /// <response code = "200"></response>
        [Route("Products")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Product>))]
        public IHttpActionResult Get()
        {
            
            IProductRepository pRepo = new ProductRepository();
            IEnumerable<Product> foundProducts = pRepo.GetAllProducts();
            if (foundProducts == null) { return InternalServerError(); }
            else { return Ok(foundProducts); } 
            

            //test
            //return Ok(Products);
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
        [ResponseType(typeof(Product))]
        public IHttpActionResult Get(int productID)
        {
            IProductRepository prepo = new ProductRepository();
            Product result = prepo.GetProductById(productID);
            if (result == null) { return InternalServerError(); }
            else { return Ok(result); }
           
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
        [ResponseType(typeof(Product))]
        public IHttpActionResult Post([FromBody] Product product)
        {
            IProductRepository prepo = new ProductRepository();
            Product result = prepo.InsertProduct(product);
            if (result == null) { return InternalServerError(); }
            else { return Ok(result); }
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
        [ResponseType(typeof(Product))]
        public IHttpActionResult Put([FromBody] Product product)
        {
            IProductRepository prepo = new ProductRepository();
            Product result = prepo.UpdateProduct(product);
            if (result == null) { return InternalServerError(); }
            else { return Ok(result); }
        }

        /// <summary>
        /// Delete a product in the system
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="productID">Product ID.</param>
        /// <returns></returns>
        /// <response code = "200">Product deleted</response>
        [Route("Products/{productID}")]
        [HttpDelete]
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int productID)
        {
            IProductRepository prepo = new ProductRepository();
            Product result = prepo.DeleteProduct(productID);
            if (result == null) { return InternalServerError(); }
            else { return Ok(result); }
        }
    }
}