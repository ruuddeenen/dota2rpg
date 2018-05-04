using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using DefenceOfTheAncientsRPG.Logic;
using DefenceOfTheAncientsRPG.Models;

namespace DefenseOfTheAncientsRPGTests
{
    [TestClass]
    public class SecurePasswordHasherTests
    {
        [TestMethod]
        public void TestHashAndVerify()
        {
            string password = "the password";
            string hashedPass = SecurePasswordHasher.Hash(password);
            Assert.AreNotEqual(hashedPass, password);
            Assert.IsTrue(SecurePasswordHasher.Verify(password, hashedPass));
        }

        [TestMethod]
        public void Testh()
        {
            throw new NotImplementedException();
        }
    }
}
