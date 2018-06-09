using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Logic;
using DefenceOfTheAncientsRPG.Data;

namespace DefenceOfTheAncientsRPG.Models.AdminViewModel
{
    public class AdminManageAdminsViewModel
    {
        private AdministratorRepository adminRepo;
        public List<Administrator> Admins;

        public AdminManageAdminsViewModel()
        {
            adminRepo = new AdministratorRepository(new AdministratorSQLContext());
            Admins = adminRepo.GetAllAdmins();
        }
    }
}
