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

        public string PersonId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public string Phone { get; set; }
        public List<OrderViewModel> Orders { get; set; }
    }
}