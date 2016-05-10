using Stockapp.Logic.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stockapp.Data;
using Stockapp.Data.Repository;
using Stockapp.Data.Extensions;

namespace Stockapp.Logic.Implementation
{
    public class AdminLogic: IAdminLogic
    {
        private readonly IUnitOfWork UnitOfWork;

        public AdminLogic(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }

        public bool CreateAdmin(Admin admin)
        {
            var existingAdmins = UnitOfWork.AdminRepository.Get();

            if (existingAdmins.isNotEmpty() && existingAdmins.Any(a => a.Email == admin.Email))
                return false;

            UnitOfWork.AdminRepository.Insert(admin);
            UnitOfWork.Save();
            return true;
        }

        public bool DeleteAdmin(Guid adminId)
        {
            UnitOfWork.AdminRepository.Delete(adminId);
            UnitOfWork.Save();
            return true;
        }

        public bool DeleteAdmin(Admin admin)
        {
            UnitOfWork.AdminRepository.Delete(admin);
            UnitOfWork.Save();
            return true;
        }

        public Admin GetAdmin(Guid adminId)
        {
            return UnitOfWork.AdminRepository.GetById(adminId);
        }

        public bool UpdateAdmin(Admin admin)
        {
            UnitOfWork.AdminRepository.Update(admin);
            UnitOfWork.Save();
            return true;
        }
    }
}
