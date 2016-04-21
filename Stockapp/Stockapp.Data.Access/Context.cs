using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data.Access
{
    public class Context: DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Player> Player { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Portfolio> Portfolios { get; set; }

        public DbSet<Action> Actions { get; set; }
    }
}
