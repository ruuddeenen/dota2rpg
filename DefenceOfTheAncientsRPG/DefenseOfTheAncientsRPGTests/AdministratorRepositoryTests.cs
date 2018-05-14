using DefenceOfTheAncientsRPG.Data;
using DefenceOfTheAncientsRPG.Logic;
using DefenceOfTheAncientsRPG.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DefenceOfTheAncientsRPG.Exceptions;

namespace DefenseOfTheAncientsRPGTests
{
    [TestClass]
    public class AdministratorRepositoryTests
    {
        private AdministratorRepository adminRepo;

        [TestInitialize]
        public void Initialize()
        {
            adminRepo = new AdministratorRepository(new AdministratorMemoryContext());

            Assert.AreEqual(1, adminRepo.GetAllAdmins().Count);
            Administrator testAdmin = new Administrator("Test123", "Name", "Surname")
            {
                Id = "test-id"
            };
            adminRepo.Insert(testAdmin);
        }

        [TestMethod]
        public void TestInsertAndGetAll()
        {
            Administrator admin = new Administrator("pass123", "John", "Doe");
            adminRepo.Insert(admin);
            Assert.AreEqual(admin.FirstName, adminRepo.GetAllAdmins()[2].FirstName);
            Assert.AreEqual(admin.LastName, adminRepo.GetAllAdmins()[2].LastName);
            Assert.AreEqual(admin.Password, adminRepo.GetAllAdmins()[2].Password);

            try
            {
                adminRepo.Insert(admin);
                Assert.Fail();
            }
            catch (EntryAlreadyExistsException) { }
        }

        [TestMethod]
        public void GetAdminByIdTest()
        {
            Administrator admin = adminRepo.GetAdminById("test-id");
            Assert.AreEqual(admin.Username, "a.NameSurname");

            try
            {
                admin = adminRepo.GetAdminById("this id doesnt exist");
                Assert.Fail();
            }
            catch (EntryDoesNotExistException) { }
        }

        [TestMethod]
        public void GetAdminByUsernameTest()
        {
            Administrator admin = adminRepo.GetAdminByUsername("a.SuperUser");

            Assert.AreEqual("Super", admin.FirstName);
            Assert.AreEqual("User", admin.LastName);

            try
            {
                admin = adminRepo.GetAdminByUsername("this username does not exist");
                Assert.Fail();
            }
            catch (EntryDoesNotExistException) { }
        }

        [TestMethod]
        public void ChangePasswordTest()
        {
            Administrator admin = adminRepo.GetAdminById("test-id");

            adminRepo.ChangePassword(admin.Id, "newPass123");

            Assert.IsTrue(SecurePasswordHasher.Verify("newPass123", adminRepo.GetAdminById("test-id").Password));


            try
            {
                adminRepo.ChangePassword(admin.Id, "newpass123");
                Assert.Fail("Password not in correct format. PasswordFormatException not catched. Upper case character required.");
            }
            catch (PasswordFormatException) { }

            try
            {
                adminRepo.ChangePassword(admin.Id, "newPass");
                Assert.Fail("Password not in correct format. PasswordFormatException not catched. Numeric character required");
            }
            catch (PasswordFormatException) { }

            try
            {
                adminRepo.ChangePassword(admin.Id, "NEWPASS123");
                Assert.Fail("Password not in correct format. PasswordFormatException not catched. Lower case character required");

            }
            catch (PasswordFormatException) { }
        }

        [TestMethod]
        public void TestActivateAdmin()
        {
            Administrator admin = adminRepo.GetAdminById("test-id");
            Assert.IsFalse(admin.Activated);

            adminRepo.Activate(admin);

            admin = adminRepo.GetAdminById("test-id");
            Assert.IsTrue(admin.Activated);
        }

    }
}
