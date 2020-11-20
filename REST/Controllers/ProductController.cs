
using REST.Models;
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
        public List<Product> Get()
        {
            IProductRepository prepo = new ProductRepository();
            return prepo.GetAllProducts();
        }


        /// <summary>
        /// By passing in the product ID, you can get the product of the product ID in the system. 
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="productID">Put in product ID.</param>
        /// <returns>List&lt;Product&gt;</returns>
        [Route("Products/{productID}")]
        [HttpGet]
        public Product Get(int productID)
        {
            IProductRepository prepo = new ProductRepository();
            return prepo.GetProductById(productID);
           
        }

        /// <summary>
        /// Adds a product to the system
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="product">Product to add (optional)</param>
        /// <returns></returns>
        [Route("Products")]
        [HttpPost]
        public void Post([FromBody] Product product)
        {
            IProductRepository prepo = new ProductRepository();
            prepo.InsertProduct(product);
        }

        [Route("Products")]
        [HttpPut]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}