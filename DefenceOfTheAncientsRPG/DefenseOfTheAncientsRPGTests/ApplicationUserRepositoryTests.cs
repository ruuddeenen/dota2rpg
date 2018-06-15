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
            ApplicationUser testUser = new ApplicationUser("testUser", "Test123", "test@test.com", "Mr.", "Test");
            userRepo.Insert(testUser);
        }

        [TestMethod]
        public void TestInsertAndGetAllUsers()
        {
            Assert.AreEqual(1, userRepo.GetAllUsers().Count);
            Assert.AreEqual("testUser", userRepo.GetAllUsers()[0].Username);

            ApplicationUser user = new ApplicationUser("john-doe", "Pass123", "john@doe.com", "John", "Doe");
            userRepo.Insert(user);
            Assert.AreEqual(user.FirstName, userRepo.GetAllUsers()[1].FirstName);
            Assert.AreEqual(user.LastName, userRepo.GetAllUsers()[1].LastName);
            Assert.AreEqual(user.Password, userRepo.GetAllUsers()[1].Password);
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
            catch (EntryDoesNotExistException) { }
        }

        [TestMethod]
        public void TestEdit()
        {
            string firstName = "Ruud";
            string lastName = "Deenen";
            string email = "ruuddeenen@mail.nl";

            ApplicationUser user = userRepo.GetUserByUsername("testUser");
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;

            Assert.IsTrue(userRepo.Edit(user));

            user = userRepo.GetUserByUsername("testUser");

            Assert.AreEqual(user.FirstName, firstName);
            Assert.AreEqual(user.LastName, lastName);
            Assert.AreEqual(user.Email, email);
        }

        [TestMethod]
        public void TestChangePassword()
        {
            ApplicationUser user = userRepo.GetUserByUsername("testUser");

            Assert.IsTrue(userRepo.ChangePassword(user.Id, "newPass123"));

            try
            {
                userRepo.ChangePassword(user.Id, "newpass123");
                Assert.Fail("Password not in correct format. PasswordFormatException not catched. UpperCase required.");
            }
            catch (PasswordFormatException) { }

            try
            {
                userRepo.ChangePassword(user.Id, "newPass");
                Assert.Fail("Password not in correct format. PasswordFormatException not catched. Numeric required");
            }
            catch (PasswordFormatException) { }

            try
            {
                userRepo.ChangePassword(user.Id, "NEWPASS123");
                Assert.Fail("Password not in correct format. PasswordFormatException not catched. LowerCase required");

            }
            catch (PasswordFormatException) { }
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
            catch (EntryDoesNotExistException) { }
        }

        [TestMethod]
        [ExpectedException(typeof(EntryAlreadyExistsException))]
        public void TestBlockingBlockedUser()
        {
            BlockedUserInfo info = new BlockedUserInfo("test", userRepo.GetUserByUsername("testUser").Id, "fakeId");
            userRepo.BlockUser(info);
            userRepo.BlockUser(info);
        }

        [TestMethod]
        [ExpectedException(typeof(EntryDoesNotExistException))]
        public void TestGettingBlockerUserInfoByWrongId()
        {
            userRepo.GetBlockedUserInfoByUserId("no user");
        }


        [TestMethod]
        [ExpectedException(typeof(EntryAlreadyExistsException))]
        public void TestInsertExistingUsername()
        {
            ApplicationUser testUser = new ApplicationUser("testUser", "Test123", "test@test.com", "Mr.", "Test");
            userRepo.Insert(testUser);
        }
    }
}
