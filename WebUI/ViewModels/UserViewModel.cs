using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

namespace WebUI.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(Person person)
        {
            this.PersonId = person.PersonId;
            this.FirstName = person.FirstName;
            this.LastName = person.LastName;
            this.Email = person.Email;
            this.Phone = person.Phone;
        }

        public int PersonId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public int Phone { get; set; }
    }
}