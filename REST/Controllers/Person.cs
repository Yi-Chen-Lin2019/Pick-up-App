using System;
using System.Collections.Generic;

namespace REST.Controllers
{
    public class Person
    {
        //public String FirstName { get; set; }
        //public String LastName { get; set; }
        public String Email { get; set; }
        public int PhoneNumber { get; set; }
        //public CustomerRole CustomerRole { get; set; }
        //public EmployeeRole EmployeeRole { get; set; }
        public List<Role> Roles { get; set; }
        public string Id { get; set; }
        public Person(string id, String email, int phoneNumber)
        {
            //this.FirstName = firstName;
            //this.LastName = lastName;
            this.Id = id;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }
        public Person() { }

    }
}