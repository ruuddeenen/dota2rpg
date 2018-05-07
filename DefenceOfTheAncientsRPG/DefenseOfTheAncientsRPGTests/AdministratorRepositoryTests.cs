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

            Administrator testAdmin = new Administrator("Test123", "Name", "Surname")
            {
                ID = "test-id"
            };
            adminRepo.Insert(testAdmin);
        }

        [TestMethod]
        public void AutomaticSuperUserAddedTest()
        {
            Assert.AreEqual(1, adminRepo.GetAllAdmins().Count);
        }

        [TestMethod]
        public void TestInsertAndGetAll()
        {
            Administrator admin = new Administrator("pass123", "John", "Doe");
            adminRepo.Insert(admin);
            Assert.AreEqual(admin.FirstName, adminRepo.GetAllAdmins()[1].FirstName);
            Assert.AreEqual(admin.LastName, adminRepo.GetAllAdmins()[1].LastName);
            Assert.AreEqual(admin.Password, adminRepo.GetAllAdmins()[1].Password);

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
                admin = adminRepo.GetAdminById("this is doesnt exist");
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

            adminRepo.ChangePassword(admin.ID, "newPass123");

            Assert.IsTrue(SecurePasswordHasher.Verify("newPass123", adminRepo.GetAdminById("test-id").Password));
        }


    }
}
