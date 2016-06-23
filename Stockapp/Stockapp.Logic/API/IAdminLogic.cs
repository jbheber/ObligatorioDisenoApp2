using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stockapp.Data;

namespace Stockapp.Logic.API
{
    public interface IAdminLogic
    {
        bool CIIsUnique(int ci);
        bool CreateAdmin(Admin admin);
        IEnumerable<Admin> GetAll();
        bool DeleteAdmin(long adminId);
        bool DeleteAdmin(Admin admin);
        Admin GetAdmin(long adminId);
        Admin GetUserAdmin(long userId);
        bool IsValidEmail(string email);
        bool UpdateAdmin(Admin admin);

        void Dispose();

    }
}
