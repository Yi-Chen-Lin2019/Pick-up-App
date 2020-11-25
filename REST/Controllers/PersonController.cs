using Dapper;
using Dapper.Contrib.Extensions;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace REST.Controllers
{
    public class PersonController : ApiController
    {
        /*
        private readonly IDbConnection _db;
        public PersonController()
        {
            _db = new SqlConnection(ConfigurationManager.ConnectionStrings["AuthenticationConnection"].ConnectionString);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        [Route("Users/GetAllUsers")]
        public List<Person> GetAllUsers()
        {
            List<Person> output = this._db.Query<Person>
             ("SELECT TOP (1000) [AspNetUsers].[Id],[Email] FROM [PikUpAppAccountData].[dbo].[AspNetUsers]").ToList();
            return output;

        
        }*/
    }
}
