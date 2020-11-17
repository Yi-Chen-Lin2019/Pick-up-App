using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using DAL;
using System.Collections.Generic;

namespace Model.Testing
{
    [TestClass]
    public class DalTests_PersonRepository
    {
        [TestMethod]
        public void DBConnectionTest_ConnectionIsRunning_ConnectionEqualsTo()
        {
            //Arrange
            PersonRepository pr = new PersonRepository();

            //Act


            //Assert
            Assert.AreEqual("Open", pr.GetConnectionState());
        }

        [TestMethod]
        public void DALTest_PersonIsReceived_PersonWorks()
        {
            //Arrange
            PersonRepository pr = new PersonRepository();

            //Act
            Person person = pr.GetPersonByEmail("mail@mail.com");

            //Assert
            Assert.AreEqual(2, person.PersonId);
            Assert.AreEqual("John", person.FirstName);
            Assert.AreEqual(1, person.CustomerRole.CustomerRoleId);

        }

        [TestMethod]
        public void DALTest_PersonListIsReceived_PersonListWorks()
        {
            //Arrange
            PersonRepository pr = new PersonRepository();

            //Act
            List<Person> people = pr.GetPeople();

            //Assert
            Assert.IsNotNull(people);
            Assert.AreEqual("John", people[0].FirstName);

        }

        [TestMethod]
        public void DALTest_PersonIsInserted_InstertedPersonIsInDatabase()
        {
            //Arrange
            PersonRepository pr = new PersonRepository();
            Person person = new Person(0, "mail@cool.net", "Marek", "Bean", 647);
            pr.InsertEmployeeRoleToPerson(person);

            //Act
            pr.InsertPerson(person);

            //Assert
            Person personTest = pr.GetPersonByPhone(647);
            Assert.AreEqual("Marek", personTest.FirstName);
        }

        [TestMethod]
        public void DALTest_PersonIsUpdated_PersonWasUpdatedInDatabase()
        {
            //Arrange
            PersonRepository pr = new PersonRepository();
            Person person = pr.GetPersonById(1);
            person.Email = "mail@notsocool.net";
            pr.InsertCustomerRoleToPerson(person);

            //Act
            pr.UpdatePerson(person);

            //Assert
            Person personTest = pr.GetPersonById(1);
            Assert.AreEqual("mail@notsocool.net", personTest.Email);
            Assert.AreEqual(1, personTest.CustomerRole.CustomerRoleId);

        }
    }
}
