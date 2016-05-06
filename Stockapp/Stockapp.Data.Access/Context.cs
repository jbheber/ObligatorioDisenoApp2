using Stockapp.Data.Interfaces;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Infrastructure;

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
            modelBuilder.Entity<Player>().HasRequired(x => x.User);
            modelBuilder.Entity<Admin>().HasRequired(x => x.User);
            modelBuilder.Entity<InvitationCode>().HasOptional(x => x.ParentUser);
            modelBuilder.Entity<Portfolio>().HasOptional(x => x.Transactions).WithRequired();
            modelBuilder.Entity<Stock>()
                .HasOptional(x => x.StockHistory)
                .WithRequired();
            modelBuilder.Entity<Stock>()
                .HasOptional(x => x.StockNews)
                .WithMany();

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
