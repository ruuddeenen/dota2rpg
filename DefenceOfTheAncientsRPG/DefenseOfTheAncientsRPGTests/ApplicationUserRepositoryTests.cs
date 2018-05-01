using System;
using System.Collections.Generic;
using System.Text;
using DefenceOfTheAncientsRPG.Logic;
using DefenceOfTheAncientsRPG.Data;
using DefenceOfTheAncientsRPG.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DefenseOfTheAncientsRPGTests
{
    class ApplicationUserRepositoryTests
    {
        private ApplicationUserRepository userRepo;


        public ApplicationUserRepositoryTests(IApplicationUserContext context)
        {
            userRepo = new ApplicationUserRepository(new ApplicationUserMemoryContext());
        }

        [TestMethod]
        public void MyTestMethod()
        {

        }
    }
}
