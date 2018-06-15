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
            // For mockdatabase testing 
            Database.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DotaTEst;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            Assert.IsTrue(Database.CleanTable("Administrators"));
            // For mockdatabase testing */


            adminRepo = new AdministratorRepository(new AdministratorSQLContext());
            
            Administrator testAdmin = new Administrator("Test123", "Name", "Surname")
            {
                Id = "test-id"
            };
            adminRepo.Insert(testAdmin);
            Assert.AreEqual(1, adminRepo.GetAllAdmins().Count);
        }

        [TestMethod]
        public void TestInsertAndGetAll()
        {
            Administrator newAdmin = new Administrator("pass123", "John", "Doe");
            adminRepo.Insert(newAdmin);
            Administrator adminFromContext = adminRepo.GetAdminByUsername("a.JohnDoe");
            Assert.AreEqual(newAdmin.FirstName, adminFromContext.FirstName);
            Assert.AreEqual(newAdmin.Password, adminFromContext.Password);
            Assert.AreEqual(newAdmin.CreatedOn, adminFromContext.CreatedOn);
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
