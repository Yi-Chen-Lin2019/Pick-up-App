using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Person
    {
        String firstName;
        String lastName;
        String email;
        int phoneNumber;
        CustomerRole customerRole = null;
        EmployeeRole employeeRole = null;

        public Person(String firstName, String lastName, String email, int phoneNumber)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phoneNumber = phoneNumber;
        }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public int PhoneNumber { get; set; }
        public CustomerRole CustomerRole { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
    }
}
