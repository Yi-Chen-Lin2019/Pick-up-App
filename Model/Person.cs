using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Person
    {
        public Person()
        {

        }
        public Person(String Email, String FirstName, String LastName, int Phone)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
        }
        public Person(int PersonId, String Email, String FirstName, String LastName, int Phone)
        {
            this.PersonId = PersonId;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
        }

        public int PersonId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public int Phone { get; set; }

        /*
        public CustomerRole CustomerRole { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
        */
        public List<Role> Roles { get; set; }
        public void AddRole(Role role)
        {
            if(role != null)
            {
                Roles.Add(role);
            }
        }
        public string UserId { get; set; }
    }
}
