using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Model;

namespace WebUI.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(Person person)
        {
            this.PersonId = person.Id;
            this.FirstName = person.FirstName;
            this.LastName = person.LastName;
            this.Email = person.Email;
            this.Phone = person.PhoneNumber;
        }
        public UserViewModel() { }

        public static UserViewModel Current
        {
            get
            {
                var user = HttpContext.Current.Session["User"] as UserViewModel;
                if (null == user)
                {
                    user = new UserViewModel();
                    HttpContext.Current.Session["User"] = user;
                }
                return user;
            }
        }

        public Person transformToPerson()
        {
            Person person = new Person();
            person.FirstName = FirstName;
            person.LastName = LastName;
            person.Email = Email;
            person.PhoneNumber = Phone;
            return person;
        }

        public string PersonId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        [EmailAddress]
        public String Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        public List<OrderViewModel> Orders { get; set; }
    }
}