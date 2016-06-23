using Stockapp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data.Access
{
    public class ContextInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            #region Users
            var users = new List<User>()
            {
                new User()
                {
                    Name = "jbheber",
                    Password = "Jb.12345",
                    Email = "juanbheber@outlook.com",
                    IsAdmin = false,
                    IsDeleted = false
                },
                new User()
                {
                    Name = "fartolaa",
                    Password = "Art.12345",
                    Email = "artolaa@outlook.com",
                    IsAdmin = false,
                    IsDeleted = false
                },
                new User()
                {
                    Name = "jheber",
                    Password = "Jh.1234554",
                    Email = "juanbautistaheber@gmail.com",
                    IsAdmin = true,
                    IsDeleted = false
                },
                new User()
                {
                    Name = "arto",
                    Password = "Artoo.1234554",
                    Email = "arto@gmail.com",
                    IsAdmin = true,
                    IsDeleted = false
                },
                new User()
                {
                    Name = "maca",
                    Email = "maca@hotmail.com",
                    Password = "Maca12345",
                    IsAdmin = false,
                    IsDeleted = false
                }
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();
            #endregion

            #region players
            var players = new List<Player>()
            {
                new Player()
                {
                    CI = 46640529,
                    Email = users.ElementAt(0).Email,
                    IsDeleted = users.ElementAt(0).IsDeleted,
                    Name = "Juan Bautista",
                    Surname = "Heber",
                    User = users.ElementAt(0),
                    Portfolio = new Portfolio()
                        {
                            AvailableMoney = 1000,
                            Transactions = new List<Transaction>(),
                        },
                },
                new Player()
                {
                    CI = 46640520,
                    Email = users.ElementAt(1).Email,
                    IsDeleted = users.ElementAt(1).IsDeleted,
                    Name = "Fernando",
                    Surname = "Artola",
                    User = users.ElementAt(1),
                    Portfolio = new Portfolio()
                        {
                            AvailableMoney = 20005,
                            Transactions = new List<Transaction>(),
                        }
                },
                new Player()
                {
                    CI = 46640521,
                    Email = users.ElementAt(2).Email,
                    IsDeleted = users.ElementAt(2).IsDeleted,
                    Name = "Juan",
                    Surname = "Heber",
                    User = users.ElementAt(2),
                    Portfolio = new Portfolio()
                        {
                            AvailableMoney = 20000000,
                            Transactions = new List<Transaction>(),
                        }
                },
                new Player()
                {
                    CI = 46640522,
                    Email = users.ElementAt(3).Email,
                    IsDeleted = users.ElementAt(3).IsDeleted,
                    Name = "Fernando",
                    Surname = "Artola",
                    User = users.ElementAt(3),
                    Portfolio = new Portfolio()
                        {
                            AvailableMoney = 15,
                            ActionsValue = 15,
                            Transactions = new List<Transaction>(),
                        }
                },
                 new Player()
                {
                    CI = 46640523,
                    Email = users.ElementAt(4).Email,
                    IsDeleted = users.ElementAt(4).IsDeleted,
                    Name = "Damian",
                    Surname = "Macaluso",
                    User = users.ElementAt(4),
                    Portfolio =  new Portfolio()
                        {
                            AvailableMoney = 3000,
                            Transactions = new List<Transaction>()
                        }
                },
            };
            players.ForEach(p => context.Players.Add(p));
            context.SaveChanges();
            #endregion

            #region admins
            var admins = new List<Admin>()
            {
                new Admin()
                {
                    CI = 46640529,
                    Email = users.ElementAt(0).Email,
                    IsDeleted = users.ElementAt(0).IsDeleted,
                    Name = "Juan Bautista",
                    Surname = "Heber",
                    UserId = users.ElementAt(0).Id
                },
                new Admin()
                {
                    CI = 46640520,
                    Email = users.ElementAt(1).Email,
                    IsDeleted = users.ElementAt(1).IsDeleted,
                    Name = "Fernando",
                    Surname = "Artola",
                    UserId = users.ElementAt(1).Id
                },
                new Admin()
                {
                    CI = 46640521,
                    Email = users.ElementAt(2).Email,
                    IsDeleted = users.ElementAt(2).IsDeleted,
                    Name = "Juan",
                    Surname = "Heber",
                    UserId = users.ElementAt(2).Id
                },
                new Admin()
                {
                    CI = 46640522,
                    Email = users.ElementAt(3).Email,
                    IsDeleted = users.ElementAt(3).IsDeleted,
                    Name = "Fernando",
                    Surname = "Artola",
                    UserId = users.ElementAt(3).Id
                },
                 new Admin()
                {
                    CI = 46640523,
                    Email = users.ElementAt(4).Email,
                    IsDeleted = users.ElementAt(4).IsDeleted,
                    Name = "Damian",
                    Surname = "Macaluso",
                    UserId = users.ElementAt(4).Id
                },
            };
            admins.ForEach(a => context.Admins.Add(a));
            context.SaveChanges();
            #endregion

            #region codes
            var invitationCodes = new List<InvitationCode>()
            {
                new InvitationCode()
                {
                    Code = "AA245GJ1",
                    IsDeleted = users.ElementAt(0).IsDeleted,
                    ParentUserId = users.ElementAt(0).Id
                },
                new InvitationCode()
                {
                    Code = "AA245GJ2",
                    IsDeleted = users.ElementAt(1).IsDeleted,
                    ParentUserId = users.ElementAt(1).Id
                },
                new InvitationCode()
                {
                    Code = "AA245GJ3",
                    IsDeleted = users.ElementAt(2).IsDeleted,
                    ParentUserId = users.ElementAt(2).Id
                },
                new InvitationCode()
                {
                    Code = "AA245GJ4",
                    IsDeleted = users.ElementAt(3).IsDeleted,
                    ParentUserId = users.ElementAt(3).Id
                },
                 new InvitationCode()
                {
                    Code = "AA245GJ5",
                    IsDeleted = users.ElementAt(4).IsDeleted,
                    ParentUserId = users.ElementAt(4).Id
                },
            };
            invitationCodes.ForEach(ic => context.InvitationCodes.Add(ic));
            context.SaveChanges();
            #endregion

            #region histories
            var stockHistories = new List<StockHistory>()
            {
                new StockHistory()
                {
                    DateOfChange = DateTimeOffset.Now,
                    RecordedValue = 0,
                    IsDeleted = false
                },
                new StockHistory()
                {
                    DateOfChange = DateTimeOffset.Now,
                    RecordedValue = 1,
                    IsDeleted = false
                },
                new StockHistory()
                {
                    DateOfChange = DateTimeOffset.Now,
                    RecordedValue = 2,
                    IsDeleted = false
                },
                new StockHistory()
                {
                    DateOfChange = DateTimeOffset.Now,
                    RecordedValue = 3,
                    IsDeleted = false
                },
                 new StockHistory()
                {
                    DateOfChange = DateTimeOffset.Now,
                    RecordedValue = 4,
                    IsDeleted = false
                }
            };
            #endregion

            #region news
            var stockNews = new List<StockNews>()
            {
                new StockNews()
                {
                    ReferencedStocks = new List<Stock>(),
                    PublicationDate = DateTimeOffset.Now,
                    Title = "News1",
                    Content = "This is the news number 1",
                    IsDeleted = false
                },
                new StockNews()
                {
                    ReferencedStocks = new List<Stock>(),
                    PublicationDate = DateTimeOffset.Now,
                    Title = "News2",
                    Content = "This is the news number 2",
                    IsDeleted = false,

                },
                new StockNews()
                {
                    ReferencedStocks = new List<Stock>(),
                    PublicationDate = DateTimeOffset.Now,
                    Title = "News3",
                    Content = "This is the news number 3",
                    IsDeleted = false,

                },
                new StockNews()
                {
                    ReferencedStocks = new List<Stock>(),
                    PublicationDate = DateTimeOffset.Now,
                    Title = "News4",
                    Content = "This is the news number 4",
                    IsDeleted = false,

                },
                 new StockNews()
                {
                    ReferencedStocks = new List<Stock>(),
                    PublicationDate = DateTimeOffset.Now,
                    Title = "News5",
                    Content = "This is the news number 5",
                    IsDeleted = false,

                },
            };
            #endregion

            #region stocks
            var stocks = new List<Stock>()
            {
                new Stock()
                {
                    Code = "MSFT",
                    Name = "Stock1",
                    Description = "Este es el stock1",
                    UnityValue = 1,
                    StockNews = new List<StockNews>(){stockNews.ElementAt(0), stockNews.ElementAt(1)},
                    StockHistory = new List<StockHistory>(){stockHistories.ElementAt(0), stockHistories.ElementAt(1)},
                    IsDeleted = false,
                    QuantiyOfActions = 5000
                },
                new Stock()
                {
                    Code = "SET",
                    Name = "Stock2",
                    Description = "Este es el stock2",
                    UnityValue = 2,
                    StockNews = new List<StockNews>(){stockNews.ElementAt(2), stockNews.ElementAt(3)},
                    StockHistory = new List<StockHistory>(),
                    IsDeleted = false,
                    QuantiyOfActions = 5000

                },
                new Stock()
                {
                    Code = "GET",
                    Name = "Stock3",
                    Description = "Este es el stock3",
                    UnityValue = 3,
                    StockNews = new List<StockNews>(){stockNews.ElementAt(0), stockNews.ElementAt(1), stockNews.ElementAt(2)},
                    StockHistory = new List<StockHistory>(){stockHistories.ElementAt(2), stockHistories.ElementAt(3), stockHistories.ElementAt(2)},
                    IsDeleted = false,
                    QuantiyOfActions = 5000

                },
                new Stock()
                {
                    Code = "SAP",
                    Name = "Stock4",
                    Description = "Este es el stock4",
                    UnityValue = 4,
                    StockNews = new List<StockNews>(){stockNews.ElementAt(0), stockNews.ElementAt(1), stockNews.ElementAt(2), stockNews.ElementAt(3)},
                    StockHistory = new List<StockHistory>(){stockHistories.ElementAt(4)},
                    IsDeleted = false,
                    QuantiyOfActions = 5000

                },
                 new Stock()
                {
                    Code = "SAT",
                    Name = "Stock5",
                    Description = "Este es el stock5",
                    UnityValue = 5,
                    StockNews = new List<StockNews>(),
                    StockHistory = new List<StockHistory>(),
                    IsDeleted = false,
                    QuantiyOfActions = 5000

                },
            };
            stocks.ForEach(s => context.Stocks.Add(s));
            context.SaveChanges();
            #endregion

            #region transactions
            var transactions = new List<Transaction>()
            {
                new Transaction()
                {
                    Stock = stocks.ElementAt(0),
                    StockQuantity = 5,
                    TotalValue = 1000,
                    TransactionDate = DateTimeOffset.Now,
                    Type = new TransactionType(),
                    PortfolioId = 1,
                    IsDeleted = false
                },
                new Transaction()
                {
                    Stock = stocks.ElementAt(1),
                    StockQuantity = 60,
                    TotalValue = 1000,
                    TransactionDate = DateTimeOffset.Now,
                    Type = new TransactionType(),
                    PortfolioId = 1,
                    IsDeleted = false
                },
                new Transaction()
                {
                    Stock = stocks.ElementAt(2),
                    StockQuantity = 2,
                    TotalValue = 2,
                    TransactionDate = DateTimeOffset.Now,
                    Type = new TransactionType(),
                    PortfolioId = 2,
                    IsDeleted = false
                },
                new Transaction()
                {
                    Stock = stocks.ElementAt(3),
                    StockQuantity = 3,
                    TotalValue = 3,
                    TransactionDate = DateTimeOffset.Now,
                    Type = new TransactionType(),
                    PortfolioId = 2,
                    IsDeleted = false
                },
                 new Transaction()
                {
                    Stock = stocks.ElementAt(4),
                    StockQuantity = 4,
                    TotalValue = 4,
                    TransactionDate = DateTimeOffset.Now,
                    Type = new TransactionType(),
                    PortfolioId = 3,
                    IsDeleted = false
                },
            };
            transactions.ForEach(t => context.Transactions.Add(t));
            context.SaveChanges();
            #endregion

            #region actions
            var actions = new List<Actions>()
            {
                new Actions()
                {
                    IsDeleted = false,
                    PortfolioId = 1,
                    StockId = 1,
                    QuantityOfActions = 40
                },
                new Actions()
                {
                    IsDeleted = false,
                    PortfolioId = 1,
                    StockId = 2,
                    QuantityOfActions = 60
                }
            };
            actions.ForEach(a => context.Actions.Add(a));
            context.SaveChanges();
            #endregion

            context.GameSettings.Add(new GameSettings()
            {
                InitialMoney = 1000000,
                MaxTransactionsPerDay = 50,
                IsDeleted = false
            });
            context.SaveChanges();
        }
    }
}

