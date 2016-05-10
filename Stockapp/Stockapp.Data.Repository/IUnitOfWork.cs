using Stockapp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepository { get; }
        IRepository<Admin> AdminRepository { get; }
        IRepository<Player> PlayerRepository { get; }
        IRepository<Portfolio> PortfolioRepository { get; }
        IRepository<Stock> StockRepository { get; }
        IRepository<StockHistory> StockHistoryRepository { get; }
        IRepository<StockNews> StockNewsRepository { get; }
        IRepository<Transaction> TransactionRepository { get; }
        IRepository<InvitationCode> InvitationCodeRepository { get; }

        IRepository<GameSettings> GameSettingsRepository { get; }
        void Save();
    }
}
