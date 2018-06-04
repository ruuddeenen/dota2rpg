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
        [ExpectedException(typeof(EntryDoesNotExistException))]
        public void GetAdminByIdTest()
        {
            Administrator admin = adminRepo.GetAdminById("test-id");
            Assert.AreEqual(admin.Username, "a.NameSurname");
            admin = adminRepo.GetAdminById("this id doesnt exist");
        }

        [TestMethod]
        [ExpectedException(typeof(EntryDoesNotExistException))]
        public void GetAdminByUsernameTest()
        {
            Administrator admin = adminRepo.GetAdminByUsername("a.SuperUser");

            Assert.AreEqual("Super", admin.FirstName);
            Assert.AreEqual("User", admin.LastName);

            admin = adminRepo.GetAdminByUsername("this username does not exist");
        }

        [TestMethod]
        public void ChangePasswordTest()
        {
            Administrator admin = adminRepo.GetAdminById("test-id");

            adminRepo.ChangePassword(admin.Id, "newPass123");

            Assert.IsTrue(SecurePasswordHasher.Verify("newPass123", adminRepo.GetAdminById("test-id").Password));
        }

        [TestMethod]
        [ExpectedException(typeof(PasswordFormatException))]
        public void TestPasswordNoUppercase()
        {
            Administrator admin = adminRepo.GetAdminById("test-id");
            adminRepo.ChangePassword(admin.Id, "newpass123");
        }

        [TestMethod]
        [ExpectedException(typeof(PasswordFormatException))]
        public void TestPasswordNumeric()
        {
            Administrator admin = adminRepo.GetAdminById("test-id");
            adminRepo.ChangePassword(admin.Id, "newPass");
        }

        [TestMethod]
        [ExpectedException(typeof(PasswordFormatException))]
        public void TestPasswordNoLowercase()
        {
            Administrator admin = adminRepo.GetAdminById("test-id");
            adminRepo.ChangePassword(admin.Id, "NEWPASS123");
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
