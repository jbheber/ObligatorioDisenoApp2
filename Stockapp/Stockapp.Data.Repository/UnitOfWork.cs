﻿using Stockapp.Data.Access;
using Stockapp.Data.Entities;
using System;

namespace Stockapp.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private Context context;
        private GenericRepository<User> userRepository;
        private GenericRepository<Admin> adminRepository;
        private GenericRepository<Player> playerRepository;
        private GenericRepository<Portfolio> portfolioRepository;
        private GenericRepository<Stock> stockRepository;
        private GenericRepository<StockHistory> stockHistoryRepository;
        private GenericRepository<StockNews> stockNewsRepository;
        private GenericRepository<Transaction> transactionRepository;
        private GenericRepository<InvitationCode> invitationCodeRepository;
        private GenericRepository<GameSettings> gameSettingsRepository;
        private GenericRepository<Actions> actionsRepository;


        public UnitOfWork(Context context)
        {
            this.context = context;
        }

        public IRepository<Admin> AdminRepository
        {
            get
            {
                if (this.adminRepository == null)
                {
                    this.adminRepository = new GenericRepository<Admin>(context);
                }
                return adminRepository;
            }
        }

        public IRepository<InvitationCode> InvitationCodeRepository
        {
            get
            {
                if (this.invitationCodeRepository == null)
                {
                    this.invitationCodeRepository = new GenericRepository<InvitationCode>(context);
                }
                return invitationCodeRepository;
            }
        }

        public IRepository<Player> PlayerRepository
        {
            get
            {
                if (this.playerRepository == null)
                {
                    this.playerRepository = new GenericRepository<Player>(context);
                }
                return playerRepository;
            }
        }

        public IRepository<Portfolio> PortfolioRepository
        {
            get
            {
                if (this.portfolioRepository == null)
                {
                    this.portfolioRepository = new GenericRepository<Portfolio>(context);
                }
                return portfolioRepository;
            }
        }

        public IRepository<StockHistory> StockHistoryRepository
        {
            get
            {
                if (this.stockHistoryRepository == null)
                {
                    this.stockHistoryRepository = new GenericRepository<StockHistory>(context);
                }
                return stockHistoryRepository;
            }
        }

        public IRepository<StockNews> StockNewsRepository
        {
            get
            {
                if (this.stockNewsRepository == null)
                {
                    this.stockNewsRepository = new GenericRepository<StockNews>(context);
                }
                return stockNewsRepository;
            }
        }

        public IRepository<Stock> StockRepository
        {
            get
            {
                if (this.stockRepository == null)
                {
                    this.stockRepository = new GenericRepository<Stock>(context);
                }
                return stockRepository;
            }
        }

        public IRepository<Transaction> TransactionRepository
        {
            get
            {
                if (this.transactionRepository == null)
                {
                    this.transactionRepository = new GenericRepository<Transaction>(context);
                }
                return transactionRepository;
            }
        }

        public IRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }

        public IRepository<GameSettings> GameSettingsRepository
        {
            get
            {
                if (this.gameSettingsRepository == null)
                {
                    this.gameSettingsRepository = new GenericRepository<GameSettings>(context);
                }
                return gameSettingsRepository;
            }
        }

        public IRepository<Actions> ActionsRepository
        {
            get
            {
                if (this.actionsRepository == null)
                {
                    this.actionsRepository = new GenericRepository<Actions>(context);
                }
                return actionsRepository;
            }
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
