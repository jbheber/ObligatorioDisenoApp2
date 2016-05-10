using Stockapp.Data.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace Stockapp.Data.Access
{
    /// <summary>
    /// Context implementation
    /// </summary>
    public class Context : DbContext
    {
        public Context() : base()
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
        public IDbSet<Stock> Stocks { get; set; }

        /// <summary>
        /// All Transactions from database
        /// </summary>
        public IDbSet<Transaction> Transactions { get; set; }

        /// <summary>
        /// All stock changes from database
        /// </summary>
        public IDbSet<StockHistory> StockHistories { get; set; }

        /// <summary>
        /// All stock recorded news from database.
        /// </summary>
        public IDbSet<StockNews> StockNews { get; set; }

        public IDbSet<InvitationCode> InvitationCodes { get; set; }
        #endregion

        // Additional conventions to help EF understand our data relationships
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasKey(x => x.Id);
            modelBuilder.Entity<InvitationCode>().HasKey(x => x.Id);
            modelBuilder.Entity<Player>().HasKey(x => x.Id);
            modelBuilder.Entity<Portfolio>().HasKey(x => x.Id);
            modelBuilder.Entity<Stock>().HasKey(x => x.Id);
            modelBuilder.Entity<StockHistory>().HasKey(x => x.Id);
            modelBuilder.Entity<Transaction>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasKey(x => x.Id);

            modelBuilder.Entity<Player>().HasRequired(x => x.User);
            modelBuilder.Entity<Player>().HasRequired(x => x.Portfolio);

            modelBuilder.Entity<Admin>().HasRequired(x => x.User);
            modelBuilder.Entity<InvitationCode>().HasRequired(x => x.ParentUser);
        }
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
