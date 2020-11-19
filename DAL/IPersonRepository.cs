using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace DAL
{
    public interface IPersonRepository
    {
        List<Person> GetPeople();

        Person GetPersonById(int id);

        Person GetPersonByEmail(String email);

        Person GetPersonByPhone(int phone);

        CustomerRole InsertCustomerRoleToPerson(Person person);

        EmployeeRole InsertEmployeeRoleToPerson(Person person);

        Person InsertPerson(Person person);

        bool UpdatePerson(Person person);
    }
}
