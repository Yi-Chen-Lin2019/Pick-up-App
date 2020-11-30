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
            try
            {
                CategoryManagement cm = new CategoryManagement();
                IEnumerable<Category> foundCategories = cm.GetAllCategories();
                return Ok(foundCategories);
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }


        /// <summary>
        /// By passing in the Category name, you can get the Category by the Category name in the system. 
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="CategoryName">Put in Category ID.</param>
        /// <returns>List&lt;Category&gt;</returns>
        /// <response code = "200">Category found</response>
        /// <response code = "404">Category not found</response>
        [Route("Categories/{CategoryName}")]
        [HttpGet]
        [ResponseType(typeof(Category))]
        public IHttpActionResult Get(string categoryName)
        {
            try
            {
                if (null == categoryName)
                {
                    throw new Exception();
                }
                CategoryManagement cm = new CategoryManagement();
                Category result = cm.GetCategoryByName(categoryName);
                return Ok(result);
            }
            catch (Exception)
            {

                return InternalServerError();
            }
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
        public IHttpActionResult Post([FromBody] Category category)
        {
            try
            {
                if (null == category)
                {
                    throw new Exception();
                }
                CategoryManagement cm = new CategoryManagement();
                Category result = cm.InsertCategory(category);
                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
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
        public IHttpActionResult Put(int CategoryID, [FromBody] Category category)
        {

            if (CategoryID != category.CategoryId || null == category) { return BadRequest(); };
            try
            {
                CategoryManagement cm = new CategoryManagement();
                bool result = cm.UpdateCategory(category);
            }
            catch (SqlException)
            {

                return InternalServerError();
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
