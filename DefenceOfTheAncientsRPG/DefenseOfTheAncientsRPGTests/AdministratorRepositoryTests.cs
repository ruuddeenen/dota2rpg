using DefenceOfTheAncientsRPG.Data;
using DefenceOfTheAncientsRPG.Logic;
using DefenceOfTheAncientsRPG.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        }

        [TestMethod]
        public void TestInsertAndGetAll()
        {
            Administrator admin = new Administrator("pass123", "John", "Doe");
            adminRepo.Insert(admin);
            Assert.AreEqual(admin.FirstName, adminRepo.GetAllAdmins()[0].FirstName);
            Assert.AreEqual(admin.LastName, adminRepo.GetAllAdmins()[0].LastName);
            Assert.AreEqual(admin.Password, adminRepo.GetAllAdmins()[0].Password);
        }
    }
}
