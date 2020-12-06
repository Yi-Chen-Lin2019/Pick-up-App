using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class PersonManagement
    {
        public IEnumerable<Person> GetAllPersons()
        {
            IPersonRepository pRepo = new PersonRepository();
            return pRepo.GetPeople();
        }

        public Person GetPersonById(int personID)
        {
            IPersonRepository pRepo = new PersonRepository();
            return pRepo.GetPersonById(personID);
        }
        /*
        public Person Insert(Person person)
        {
            IPersonRepository pRepo = new PersonRepository();
            return pRepo.InsertPerson(person);
        }
        */
        public bool UpdatePerson(Person person)
        {
            IPersonRepository pRepo = new PersonRepository();
            return pRepo.UpdatePerson(person);
        }

        public Person GetPersonByUserName(string userName)
        {
            IPersonRepository pRepo = new PersonRepository();
            return pRepo.GetPersonByEmail(userName);
        }
    }
}
