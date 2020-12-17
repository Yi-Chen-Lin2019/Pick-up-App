using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;

namespace UnitTestProject
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
            Person person = pr.GetPersonByEmail("superadmin@pickup.com");

            //Assert
            Assert.AreEqual("Group 4",person.FirstName );

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

        }

        
        [TestMethod]
        public void DALTest_PersonIsUpdated_PersonWasUpdatedInDatabase()
        {
            //Arrange
            PersonRepository pr = new PersonRepository();
            Person person = pr.GetPersonByEmail("superadmin@pickup.com");
            person.PhoneNumber = "56788";

            //Act
            bool isSuccess = pr.UpdatePerson(person);

            //Assert
            Assert.IsTrue(isSuccess);

        }
    }
}
