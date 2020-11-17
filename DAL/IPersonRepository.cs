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

        bool InsertCustomerRoleToPerson(Person person);

        bool InsertEmployeeRoleToPerson(Person person);

        bool InsertPerson(Person person);

        bool UpdatePerson(Person person);
    }
}
