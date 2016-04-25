using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data.Access.Interfaces
{
    /// <summary>
    /// Context interface
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// All Users from database
        /// </summary>
        IDbSet<User> Users { get; set; }

        /// <summary>
        /// All Players from database
        /// </summary>
        IDbSet<Player> Players { get; set; }

        /// <summary>
        /// All Administrators from database
        /// </summary>
        IDbSet<Admin> Admins { get; set; }

        /// <summary>
        /// All portfolios from database
        /// </summary>
        IDbSet<Portfolio> Portfolios { get; set; }

        /// <summary>
        /// All Actions from database
        /// </summary>
        IDbSet<Action> Actions { get; set; }

        /// <summary>
        /// All non deleted Users
        /// </summary>
        IQueryable<User> UsersGet { get; }

        /// <summary>
        /// All non deleted Players
        /// </summary>
        IQueryable<Player> PlayersGet { get; }

        /// <summary>
        /// All non deleted Administrators
        /// </summary>
        IQueryable<Admin> AdminsGet { get; }

        /// <summary>
        /// All non deleted Portfolios
        /// </summary>
        IQueryable<Portfolio> PortfoliosGet { get; }

        /// <summary>
        /// All non deleted Actions
        /// </summary>
        IQueryable<Action> ActionsGet { get; }
    }
}
