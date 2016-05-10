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
        bool CreateAdmin(Admin admin);
        bool DeleteAdmin(Guid adminId);
        bool DeleteAdmin(Admin admin);
        Admin GetAdmin(Guid adminId);
        bool UpdateAdmin(Admin admin);

        void Dispose();

    }
}
