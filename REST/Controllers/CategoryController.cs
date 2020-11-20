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
    //[Authorize]
    public class CategoryController : ApiController
    {
        /// <summary>
        /// Get all Categories
        /// </summary>
        /// <remarks>
        /// Get a list of Categories
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>List&lt;Category&gt;</returns>
        /// <response code = "200">Categories found</response>
        [Route("Categories")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Category>))]
        public IHttpActionResult Get()
        {
            CategoryManagement cm = new CategoryManagement();
            IEnumerable<Category> foundCategories = cm.GetAllCategories();
            if (foundCategories == null) { return InternalServerError(); }
            else { return Ok(foundCategories); }

        }


        /// <summary>
        /// By passing in the Category ID, you can get the Category of the Category ID in the system. 
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="CategoryID">Put in Category ID.</param>
        /// <returns>List&lt;Category&gt;</returns>
        /// <response code = "200">Category found</response>
        /// <response code = "404">Category not found</response>
        [Route("Categories/{CategoryID}")]
        [HttpGet]
        [ResponseType(typeof(Category))]
        public IHttpActionResult Get(string categoryName)
        {
            CategoryManagement cm = new CategoryManagement();
            Category result = cm.GetCategoryByName(categoryName);
            if (result == null) { return InternalServerError(); }
            else { return Ok(result); }

        }

        /// <summary>
        /// Add a Category to the system
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="Category">Category to add (optional)</param>
        /// <returns></returns>
        /// <response code = "201">Category created</response>
        [Route("Categories")]
        [HttpPost]
        [ResponseType(typeof(Category))]
        public IHttpActionResult Post([FromBody] Category Category)
        {
            CategoryManagement cm = new CategoryManagement();
            Category result = cm.InsertCategory(Category);
            if (result == null) { return InternalServerError(); }
            else { return Ok(result); }
        }

        /// <summary>
        /// Update a Category by ID
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="CategoryID">Category ID</param>
        /// <param name="Category">Category to update</param>
        /// <returns></returns>
        /// <response code = "200">Category updated</response>
        [Route("Categories/{CategoryID}")]
        [HttpPut]
        [ResponseType(typeof(Category))]
        public IHttpActionResult Put(int CategoryID, [FromBody] Category Category)
        {
            if (CategoryID != Category.CategoryId) { return BadRequest(); };
            try
            {
                CategoryManagement cm = new CategoryManagement();
                bool result = cm.UpdateCategory(Category);
            }
            catch (SqlException)
            {

                throw;
            }
            return Ok();
        }

        /*
        /// <summary>
        /// Delete a Category in the system
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="CategoryID">Category ID.</param>
        /// <returns></returns>
        /// <response code = "200">Category deleted</response>
        [Route("Categories/{CategoryID}")]
        [HttpDelete]
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int CategoryID)
        {
            CategoryManagement cm = new CategoryManagement();
            Category result = cm.DeleteCategory(CategoryID);
            if (result == null) { return InternalServerError(); }
            else { return Ok(result); }
        }
        */
        
    }
}
