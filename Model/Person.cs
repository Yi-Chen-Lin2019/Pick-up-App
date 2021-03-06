﻿using Newtonsoft.Json;
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
        public Person(String Email, String FirstName, String LastName, string Phone) 
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.PhoneNumber = Phone;
            this.UserName = Email;
        }
        public Person(String PersonId, String Email, String FirstName, String LastName, string Phone)
        {
            this.Id = PersonId;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.PhoneNumber = Phone;
            this.UserName = Email;
        }
        
        public String Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public String UserName { get; set; }


    }
}
