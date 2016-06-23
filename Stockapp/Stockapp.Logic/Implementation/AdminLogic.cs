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

        public bool CIIsUnique(int ci)
        {
            var adminList = UnitOfWork.AdminRepository.Get();
            if (adminList.Any(a => a.CI == ci))
            {
                return false;
            }
            return true;
        }

        public bool CreateAdmin(Admin admin)
        {
            var existingAdmins = UnitOfWork.AdminRepository.Get();

            if (existingAdmins.IsNotEmpty() && existingAdmins.Any(a => a.Email == admin.Email))
                return false;

            UnitOfWork.AdminRepository.Insert(admin);
            UnitOfWork.Save();
            return true;
        }

        public IEnumerable<Admin> GetAll()
        {
            return UnitOfWork.AdminRepository.Get();
        }

        public bool DeleteAdmin(long adminId)
        {
            UnitOfWork.AdminRepository.Delete(adminId);
            UnitOfWork.Save();
            return true;
        }

        public bool DeleteAdmin(Admin admin)
        {
            UnitOfWork.UserRepository.Delete(admin.User);
            UnitOfWork.AdminRepository.Delete(admin);
            UnitOfWork.Save();
            return true;
        }

        public Admin GetAdmin(long adminId)
        {
            return UnitOfWork.AdminRepository.GetById(adminId);
        }

        public Admin GetUserAdmin(long userId)
        {
            return UnitOfWork.AdminRepository.Get(a => a.UserId == userId, null, "User").FirstOrDefault();
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateAdmin(Admin admin)
        {
            var existingUsers = UnitOfWork.UserRepository.Get();

            if (existingUsers.IsNotEmpty() && existingUsers.Any(a => (a.Email == admin.Email) && (a.Id != admin.UserId)))
                return false;

            if (IsValidEmail(admin.Email))
            {
                UnitOfWork.UserRepository.Update(admin.User);
                UnitOfWork.AdminRepository.Update(admin);
                UnitOfWork.Save();
                return true;
            }
            return false;            
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
