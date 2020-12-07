using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    public class PersonRepository : IPersonRepository
    {
        IDbConnection conn;
        public PersonRepository()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public String GetConnectionState() {

            conn.Open();
            String result = conn.State.ToString();
            conn.Close();
            return result;
        }

        public List<Person> GetPeople()
        {
            //Gets ID of all people, then gets each person by this id
            conn.Open();
            List<Person> result = new List<Person>();
            List<string> ids = conn.Query<string>("SELECT [Id] FROM [AspNetUsers]").ToList();
            conn.Close();
            foreach (string i in ids)
            {
                result.Add(GetPersonById(i));
            }

            return result;
        }

        public Person GetPersonById(string id)
        {
            conn.Open();

            Person result = conn.Query<Person>("SELECT [Id], [Email], [FirstName], [LastName], [PhoneNumber], [UserName] FROM [AspNetUsers] WHERE Id =@Id", new { Id = id }).SingleOrDefault();         
            conn.Close();
            return result;
        }

        public Person GetPersonByEmail(string email)
        {
            conn.Open();

            Person result = conn.Query<Person>("SELECT [Id], [Email], [FirstName], [LastName], [PhoneNumber], [UserName] FROM [AspNetUsers] WHERE Email =@Email", new { Email = email }).SingleOrDefault();


            conn.Close();
            return result;
        }

        public Person GetPersonByPhone(String phone)
        {
            conn.Open();

            Person result = conn.Query<Person>("SELECT [Id], [Email], [FirstName], [LastName], [PhoneNumber], [UserName] FROM [AspNetUsers] WHERE PhoneNumber =@Phone", new { Phone = phone }).SingleOrDefault();

            //result.CustomerRole = conn.Query<CustomerRole>("SELECT [CustomerRole].[CustomerRoleId] FROM [CustomerRole] INNER JOIN [Person] ON [CustomerRole].[CustomerRoleId] = [Person].[CustomerRoleId] WHERE [Phone]=@Phone", new { Phone = phone }).SingleOrDefault();
            //result.EmployeeRole = conn.Query<EmployeeRole>("SELECT [EmployeeRole].[EmployeeRoleId] FROM [EmployeeRole] INNER JOIN [Person] ON [EmployeeRole].[EmployeeRoleId] = [Person].[EmployeeRoleId] WHERE [Phone]=@Phone", new { Phone = phone }).SingleOrDefault();

            conn.Close();
            return result;
        }
    
        /*
        public Person InsertPerson(Person person)
        {
            conn.Open();
            int rowsAffected = conn.Execute(@"INSERT INTO [Person] VALUES(@Email, @FirstName, @LastName, @Phone, @UserId)",
                new { Email = person.Email, FirstName = person.FirstName, LastName = person.LastName, Phone = person.Phone, UserId = person.UserId});
            
            int id = conn.Query<int>("SELECT @@IDENTITY").SingleOrDefault();

            // set default role as Customer when register
            conn.Execute(@"INSERT INTO [dbo].[AspNetUserRoles] ([UserId],[RoleId]) VALUES (@UserId, @RoleId)",
                new { UserId = person.UserId, RoleId = 3 });
            conn.Close();

            if (rowsAffected >= 1) { person.PersonId = id; return person; }
            else { return null; }
        }
        */
        public bool UpdatePerson(Person person)
        {
            conn.Open();
            //if (person.CustomerRole != null){
            //    //If CustomerRole would have variables to update
            //     //conn.Execute("UPDATE [CustomerRole] SET ... WHERE CustomerRoleId = @CustomerRoleId", new { CustomerRoleId = person.CustomerRole.CustomerRoleId });
            //}

            //if (person.EmployeeRole != null){
            //    //If EmployeeRole would have variables to update
            //    //conn.Execute("UPDATE [EmployeeRole] SET ... WHERE EmployeeRoleId = @EmployeeRoleId", new { EmployeeRoleId = person.EmployeeRole.EmployeeRoleId });
            //}

            int rowsAffected = conn.Execute("UPDATE [Person] SET Email = @Email, FirstName = @FirstName, LastName = @LastName, Phone = @Phone, Password = @Password WHERE PersonId = @PersonId",
                new { Email = person.Email, FirstName = person.FirstName, LastName = person.LastName, Phone = person.PhoneNumber, Password = "default", PersonId = person.Id });
            
            conn.Close();

            if (rowsAffected >= 1) { return true; }
            else { return false; }
        }

        public Role InsertRole(Role role)
        {
            Role result = null;
            int rowsAffected;
            using (conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                rowsAffected = conn.Execute("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (@Id, @Name)",
                    new { Id = role.RoleId, Name = role.RoleName });
            }
            if (rowsAffected != 0) { result = role; }
            return result;
        }
    }
}
