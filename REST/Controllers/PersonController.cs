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
using Microsoft.AspNet.Identity;
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
    [Authorize]
    public class PersonController : ApiController
    {
        /// <summary>
        /// Get all users
        /// </summary>
        /// <remarks>
        /// Get a list of users
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>List&lt;Person&gt;</returns>
        /// <response code = "200">Persons found</response>
        [Route("Person")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(IEnumerable<Person>))]
        public IHttpActionResult Get()
        {
            try
            {
                PersonManagement pm = new PersonManagement();
                IEnumerable<Person> foundPersons = pm.GetPeople();
                return Ok(foundPersons);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return InternalServerError();
            }
        }


        /// <summary>
        /// By passing in the username, you can get the person of the username in the system. 
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="userName">Put in person username.</param>
        /// <returns>List&lt;Person&gt;</returns>
        /// <response code = "200">Person found</response>
        /// <response code = "404">Person not found</response>
        [Route("Persons/UserName")]
        [HttpGet]
        [Authorize(Roles = "Customer")]
        [ResponseType(typeof(Person))]
        public IHttpActionResult Get(String userName)
        {
            if (RequestContext.Principal.Identity.GetUserName().Equals(userName))
            {
                try
                {
                    PersonManagement pm = new PersonManagement();
                    Person result = pm.GetPersonByUserName(userName);
                    return Ok(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return InternalServerError();
                }
            } else
            {
                return BadRequest();
            }
                
        }

        /// <summary>
        /// Update personal info by ID
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="personID">Person ID</param>
        /// <param name="person">Person to update</param>
        /// <returns></returns>
        /// <response code = "200">Person updated</response>
        [Route("Persons/{personID}")]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(Person))]
        public IHttpActionResult Put(string personID, [FromBody] Person person)
        {
            if (personID != person.Id || null == person) { return BadRequest(); };
            try
            {
                PersonManagement pm = new PersonManagement();
                bool result = pm.UpdatePerson(person);
            }
            catch (SqlException)
            {

                return InternalServerError();
            }

            return Ok();
        }


        /// <summary>
        /// Delete a person in the system
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="personID">Person ID.</param>
        /// <returns></returns>
        /// <response code = "200">Person deleted</response>
        //[Route("Persons/{personID}")]
        //[HttpDelete]
        //// DELETE api/<controller>/5
        //public IHttpActionResult Delete(int personID)
        //{
        //    PersonManagement pm = new PersonManagement();
        //    Person result = pm.DeletePerson(personID);
        //    if (result == null) { return InternalServerError(); }
        //    else { return Ok(result); }
        //}

    }
}