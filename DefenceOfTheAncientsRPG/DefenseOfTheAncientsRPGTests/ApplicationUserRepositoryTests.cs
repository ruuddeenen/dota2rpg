using System;
using System.Collections.Generic;
using System.Text;
using DefenceOfTheAncientsRPG.Logic;
using DefenceOfTheAncientsRPG.Data;
using DefenceOfTheAncientsRPG.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DefenceOfTheAncientsRPG.Exceptions;

namespace DefenseOfTheAncientsRPGTests
{
    [TestClass]
    public class ApplicationUserRepositoryTests
    {
        private ApplicationUserRepository userRepo;

        [TestInitialize]
        public void Initialize()
        {
            userRepo = new ApplicationUserRepository(new ApplicationUserMemoryContext());
            ApplicationUser user = new ApplicationUser("tester", "Pass123", "test@test.com", "Mister", "Test");
        }

        [TestMethod]
        public void TestInsertAndGetAllUsers()
        {
            ApplicationUser user = new ApplicationUser("john-doe", "Pass123", "john@doe.com", "John", "Doe");
            userRepo.Insert(user);
            Assert.AreEqual(user.FirstName, userRepo.GetAllUsers()[0].FirstName);
            Assert.AreEqual(user.LastName, userRepo.GetAllUsers()[0].LastName);
            Assert.AreEqual(user.Password, userRepo.GetAllUsers()[0].Password);
        }

        [TestMethod]
        public void TestGetUserById()
        {
            string testId = "testId-123-abc";
            ApplicationUser user = new ApplicationUser("john-doe", "Pass123", "test@test.com", "John", "Doe")
            {
                Id = testId
            };
            userRepo.Insert(user);

            Assert.AreEqual(user, userRepo.GetUserById(testId));

            try
            {
                testId = "randomId";
                userRepo.GetUserById(testId);
                Assert.Fail("The user Id does not exist. UserDoesNotExistException not catched.");
            }
            catch (UserDoesNotExistException) { }
        }

        [TestMethod]
        public void TestGetUserByUsername()
        {
            string testUsername = "john-doe";
            ApplicationUser user = new ApplicationUser(testUsername, "Pass123", "test@test.com", "John", "Doe");
            userRepo.Insert(user);

            Assert.AreEqual(user, userRepo.GetUserByUsername(testUsername));

            try
            {
                testUsername = "randomUsername";
                userRepo.GetUserByUsername(testUsername);
                Assert.Fail("The username does not exist. UserDoesNotExistException not catched.");
            }
            catch (UserDoesNotExistException) { }
        }

        [TestMethod]
        public void TestEdit()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestChangePassword()
        {
            ApplicationUser user = userRepo.GetUserByUsername("john-doe");
            user.Password = "123456";
            userRepo.ChangePassword(user);
        }

        [TestMethod]
        public void TestLogin()
        {
            string username = "usernameTest";
            string password = "Pass123";
            ApplicationUser user = new ApplicationUser(username, password, "test@test.com", "John", "Doe");
            userRepo.Insert(user);

            Assert.IsTrue(userRepo.Login(username, password));

            try
            {
                userRepo.Login(username, "wrongPassword");
                Assert.Fail("The password is not correct. IncorrectPasswordException not catched.");
            }
            catch (IncorrectPasswordException) { }

            try
            {
                userRepo.Login("usernamefail", password);
                Assert.Fail("The username does not exist. UserDoesNotExistException not catched");
            }
            catch (UserDoesNotExistException) { }
        }

        [TestMethod]
        public void TestBlockAndUnblock()
        {
            throw new NotImplementedException();
        }
    }
}
