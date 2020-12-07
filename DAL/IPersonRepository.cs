using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace DAL
{
    public interface IPersonRepository
    {
        List<Person> GetPeople();

        Person GetPersonById(string id);

        Person GetPersonByEmail(String email);

        Person GetPersonByPhone(String phone);

        /*
        CustomerRole InsertCustomerRoleToPerson(Person person);

        EmployeeRole InsertEmployeeRoleToPerson(Person person);
        */
        Role InsertRole(Role role);
        
        //Person InsertPerson(Person person);

        bool UpdatePerson(Person person);
    }
}
