using Stockapp.Data.Access.Interfaces;
using Stockapp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data.Access
{
    /// <summary>
    /// Context implementation
    /// </summary>
    public class Context : DbContext, IContext
    {
        public Context() : base("name=Context")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        #region DbSets
        /// <summary>
        /// All Users from database
        /// </summary>
        public IDbSet<User> Users { get; set; }

        /// <summary>
        /// All Players from database
        /// </summary>
        public IDbSet<Player> Players { get; set; }

        /// <summary>
        /// All Administrators from database
        /// </summary>
        public IDbSet<Admin> Admins { get; set; }

        /// <summary>
        /// All portfolios from database
        /// </summary>
        public IDbSet<Portfolio> Portfolios { get; set; }

        /// <summary>
        /// All Actions from database
        /// </summary>
        public IDbSet<Action> Actions { get; set; }
        #endregion

        #region Get non-deleted
        /// <summary>
        /// All non deleted Users
        /// </summary>
        public IQueryable<User> UsersGet
        {
            get
            {
                return Users.GetNonDeleted<User>();
            }
        }

        /// <summary>
        /// All non deleted Players
        /// </summary>
        public IQueryable<Player> PlayersGet
        {
            get
            {
                return Players.GetNonDeleted<Player>();
            }
        }

        /// <summary>
        /// All non deleted Administrators
        /// </summary>
        public IQueryable<Admin> AdminsGet
        {
            get
            {
                return Admins.GetNonDeleted<Admin>();
            }
        }

        /// <summary>
        /// All non deleted Portfolios
        /// </summary>
        public IQueryable<Portfolio> PortfoliosGet
        {
            get
            {
                return Portfolios.GetNonDeleted<Portfolio>();
            }
        }

        /// <summary>
        /// All non deleted Actions
        /// </summary>
        public IQueryable<Action> ActionsGet
        {
            get
            {
                return Actions.GetNonDeleted<Action>();
            }
        }
        #endregion
    }

    #region Extension methods for getting non-deleted
    public static class ContextExtensions
    {
        /// <summary>
        /// Automatically applies the filter for deleted entries.
        /// </summary>
        /// <typeparam name="TEntity">Entity Class Name</typeparam>
        /// <param name="set">Entity DbSet</param>
        /// <returns></returns>
        public static IQueryable<TEntity> GetNonDeleted<TEntity>(this IQueryable<TEntity> set) where TEntity : class, ISoftDelete
        {
            return set.Where(entity => entity.IsDeleted == false);
        }
    }
    #endregion
}
