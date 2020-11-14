
using REST.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace REST.Controllers
{
    public class ProductController : ApiController
    {
        /// <summary>
        /// Get all  products. 
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>List&lt;Product&gt;</returns>
        [Route("Products")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            IProductRepository pRepo = new ProductRepository();
            IEnumerable<Product> foundProducts = pRepo.GetAllProducts();
            if (foundProducts == null) { return InternalServerError(); }
            else { return Ok(foundProducts); }
        }


        /// <summary>
        /// By passing in the product ID, you can get the product of the product ID in the system. 
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="productID">Put in product ID.</param>
        /// <returns>List&lt;Product&gt;</returns>
        [Route("Products/{productID}")]
        [HttpGet]
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
        [Route("Products")]
        [HttpPost]
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
        /// <param name="product">Product to update (optional)</param>
        /// <returns></returns>
        [Route("Products")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Product product)
        {
            IProductRepository prepo = new ProductRepository();
            Product result = prepo.UpdateProduct(id, product);
            if (result == null) { return InternalServerError(); }
            else { return Ok(result); }
        }

        /// <summary>
        /// Delete a product in the system
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="productID">Product ID.</param>
        /// <returns></returns>
        [Route("Products")]
        [HttpDelete]
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            IProductRepository prepo = new ProductRepository();
            Product result = prepo.DeleteProduct(id);
            if (result == null) { return InternalServerError(); }
            else { return Ok(result); }
        }
    }
}