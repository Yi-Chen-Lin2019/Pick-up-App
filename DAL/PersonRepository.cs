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
            List<int> ids = conn.Query<int>("SELECT [PersonId] FROM [Person]").ToList();
            conn.Close();
            foreach (int i in ids)
            {
                result.Add(GetPersonById(i));
            }

            return result;
        }

        public Person GetPersonById(int id)
        {
            conn.Open();

            Person result = conn.Query<Person>("SELECT [PersonId], [Email], [FirstName], [LastName], [Phone] FROM [Person] WHERE PersonId =@PersonId", new { PersonId = id }).SingleOrDefault();

            result.CustomerRole = conn.Query<CustomerRole>("SELECT [CustomerRole].[CustomerRoleId] FROM [CustomerRole] INNER JOIN [Person] ON [CustomerRole].[CustomerRoleId] = [Person].[CustomerRoleId] WHERE [PersonId]=@PersonId", new { PersonId = id }).SingleOrDefault();
            result.EmployeeRole = conn.Query<EmployeeRole>("SELECT [EmployeeRole].[EmployeeRoleId] FROM [EmployeeRole] INNER JOIN [Person] ON [EmployeeRole].[EmployeeRoleId] = [Person].[EmployeeRoleId] WHERE [PersonId]=@PersonId", new { PersonId = id }).SingleOrDefault();

            conn.Close();
            return result;
        }

        public Person GetPersonByEmail(string email)
        {
            conn.Open();

            Person result = conn.Query<Person>("SELECT [PersonId], [Email], [FirstName], [LastName], [Phone] FROM [Person] WHERE Email =@Email", new { Email = email }).SingleOrDefault();

            if (result != null)
            {
                result.CustomerRole = conn.Query<CustomerRole>("SELECT [CustomerRole].[CustomerRoleId] FROM [CustomerRole] INNER JOIN [Person] ON [CustomerRole].[CustomerRoleId] = [Person].[CustomerRoleId] WHERE [Email]=@Email", new { Email = email }).SingleOrDefault();
                result.EmployeeRole = conn.Query<EmployeeRole>("SELECT [EmployeeRole].[EmployeeRoleId] FROM [EmployeeRole] INNER JOIN [Person] ON [EmployeeRole].[EmployeeRoleId] = [Person].[EmployeeRoleId] WHERE [Email]=@Email", new { Email = email }).SingleOrDefault();
            }

            conn.Close();
            return result;
        }

        public Person GetPersonByPhone(int phone)
        {
            conn.Open();

            Person result = conn.Query<Person>("SELECT [PersonId], [Email], [FirstName], [LastName], [Phone] FROM [Person] WHERE Phone =@Phone", new { Phone = phone }).SingleOrDefault();

            result.CustomerRole = conn.Query<CustomerRole>("SELECT [CustomerRole].[CustomerRoleId] FROM [CustomerRole] INNER JOIN [Person] ON [CustomerRole].[CustomerRoleId] = [Person].[CustomerRoleId] WHERE [Phone]=@Phone", new { Phone = phone }).SingleOrDefault();
            result.EmployeeRole = conn.Query<EmployeeRole>("SELECT [EmployeeRole].[EmployeeRoleId] FROM [EmployeeRole] INNER JOIN [Person] ON [EmployeeRole].[EmployeeRoleId] = [Person].[EmployeeRoleId] WHERE [Phone]=@Phone", new { Phone = phone }).SingleOrDefault();

            conn.Close();
            return result;
        }
        
        public bool InsertCustomerRoleToPerson(Person person)
        {
            conn.Open();

            if (person.CustomerRole == null)
            {
                conn.Execute("INSERT INTO [CustomerRole] DEFAULT VALUES");
                int id = conn.Query<int>("SELECT SCOPE_IDENTITY()").SingleOrDefault();
                conn.Execute("UPDATE [Person] SET CustomerRoleId = @CustomerRoleId WHERE PersonId = @PersonId", new { CustomerRoleId = id, PersonId = person.PersonId});
                conn.Close();
                person.CustomerRole = new CustomerRole(id);
                return true;
            }
            else { conn.Close(); return false; }
        }

        public bool InsertEmployeeRoleToPerson(Person person)
        {
            conn.Open();

            if (person.EmployeeRole == null)
            {
                conn.Execute("INSERT INTO [EmployeeRole] DEFAULT VALUES");
                int id = conn.Query<int>("SELECT SCOPE_IDENTITY()").SingleOrDefault();
                conn.Execute("UPDATE [Person] SET EmployeeRoleId = @EmployeeRoleId WHERE PersonId = @PersonId", new { EmployeeRoleId = id, PersonId = person.PersonId });
                conn.Close();
                person.EmployeeRole = new EmployeeRole(id);
                return true;
            }
            else { conn.Close(); return false; }

            
        }

        public bool InsertPerson(Person person)
        {
            conn.Open();
            //Set string variables to either existing customer/employee id or to null, further passed in person insert query
            String customerRoleId, employeeRoleId;

            if(person.CustomerRole != null) {customerRoleId = person.CustomerRole.CustomerRoleId.ToString();}
            else { customerRoleId = null; }

            if (person.EmployeeRole != null) {employeeRoleId = person.EmployeeRole.EmployeeRoleId.ToString();}
            else { employeeRoleId = null; }

            int rowsAffected = conn.Execute(@"INSERT INTO [Person] VALUES(@Email, @FirstName, @LastName, @Phone, @CustomerRoleId, @EmployeeRoleId, @Password)",
                new { Email = person.Email, FirstName = person.FirstName, LastName = person.LastName, Phone = person.Phone, CustomerRoleId = customerRoleId, EmployeeRoleId = employeeRoleId, Password = "default"});
            
            conn.Close();

            if (rowsAffected >= 1) { return true; }
            else { return false; }
        }

        public bool UpdatePerson(Person person)
        {
            conn.Open();
            if (person.CustomerRole != null){
                //If CustomerRole would have variables to update
                 //conn.Execute("UPDATE [CustomerRole] SET ... WHERE CustomerRoleId = @CustomerRoleId", new { CustomerRoleId = person.CustomerRole.CustomerRoleId });
            }

            if (person.EmployeeRole != null){
                //If EmployeeRole would have variables to update
                //conn.Execute("UPDATE [EmployeeRole] SET ... WHERE EmployeeRoleId = @EmployeeRoleId", new { EmployeeRoleId = person.EmployeeRole.EmployeeRoleId });
            }

            int rowsAffected = conn.Execute("UPDATE [Person] SET Email = @Email, FirstName = @FirstName, LastName = @LastName, Phone = @Phone, Password = @Password WHERE PersonId = @PersonId",
                new { Email = person.Email, FirstName = person.FirstName, LastName = person.LastName, Phone = person.Phone, Password = "default", PersonId = person.PersonId });
            conn.Close();
            if (rowsAffected >= 1) { return true; }
            else { return false; }
        }
    }
}
