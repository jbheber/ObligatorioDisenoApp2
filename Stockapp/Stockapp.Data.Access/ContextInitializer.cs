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
            var users = new List<User>()
            {
                new User()
                {
                    Name = "jbheber",
                    Password = "Jb.12345",
                    Email = "juanbheber@outlook.com",
                    IsAdmin = false,
                    IsDeleted = false,
                     Id = Guid.NewGuid()
                },
                new User()
                {
                    Name = "fartolaa",
                    Password = "Art.12345",
                    Email = "artolaa@outlook.com",
                    IsAdmin = false,
                    IsDeleted = false,
                     Id = Guid.NewGuid()
                },
                new User()
                {
                    Name = "jheber",
                    Password = "Jh.1234554",
                    Email = "juanbautistaheber@gmail.com",
                    IsAdmin = true,
                    IsDeleted = false,
                     Id = Guid.NewGuid()
                },
                new User()
                {
                    Name = "arto",
                    Password = "Artoo.1234554",
                    Email = "arto@gmail.com",
                    IsAdmin = true,
                    IsDeleted = true,
                     Id = Guid.NewGuid()
                },
                new User()
                {
                    Name = "maca",
                    Password = "Maluso.1234554",
                    Email = "macaluso@gmail.com",
                    IsAdmin = false,
                    IsDeleted = false,
                     Id = Guid.NewGuid()
                }
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

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
                     Id = Guid.NewGuid()
                },
                new Player()
                {
                    CI = 46640520,
                    Email = users.ElementAt(1).Email,
                    IsDeleted = users.ElementAt(1).IsDeleted,
                    Name = "Fernando",
                    Surname = "Artola",
                    User = users.ElementAt(1),
                     Id = Guid.NewGuid()
                },
                new Player()
                {
                    CI = 46640521,
                    Email = users.ElementAt(2).Email,
                    IsDeleted = users.ElementAt(2).IsDeleted,
                    Name = "Juan",
                    Surname = "Heber",
                    User = users.ElementAt(2),
                     Id = Guid.NewGuid()
                },
                new Player()
                {
                    CI = 46640522,
                    Email = users.ElementAt(3).Email,
                    IsDeleted = users.ElementAt(3).IsDeleted,
                    Name = "Fernando",
                    Surname = "Artola",
                    User = users.ElementAt(3),
                     Id = Guid.NewGuid()
                },
                 new Player()
                {
                    CI = 46640523,
                    Email = users.ElementAt(4).Email,
                    IsDeleted = users.ElementAt(4).IsDeleted,
                    Name = "Damian",
                    Surname = "Macaluso",
                    User = users.ElementAt(4),
                     Id = Guid.NewGuid()
                },
            };
            players.ForEach(p => context.Players.Add(p));
            context.SaveChanges();

            var admins = new List<Admin>()
            {
                new Admin()
                {
                    CI = 46640529,
                    Email = users.ElementAt(0).Email,
                    IsDeleted = users.ElementAt(0).IsDeleted,
                    Name = "Juan Bautista",
                    Surname = "Heber",
                    User = users.ElementAt(0),
                     Id = Guid.NewGuid()
                },
                new Admin()
                {
                    CI = 46640520,
                    Email = users.ElementAt(1).Email,
                    IsDeleted = users.ElementAt(1).IsDeleted,
                    Name = "Fernando",
                    Surname = "Artola",
                    User = users.ElementAt(1),
                     Id = Guid.NewGuid()
                },
                new Admin()
                {
                    CI = 46640521,
                    Email = users.ElementAt(2).Email,
                    IsDeleted = users.ElementAt(2).IsDeleted,
                    Name = "Juan",
                    Surname = "Heber",
                    User = users.ElementAt(2),
                     Id = Guid.NewGuid()
                },
                new Admin()
                {
                    CI = 46640522,
                    Email = users.ElementAt(3).Email,
                    IsDeleted = users.ElementAt(3).IsDeleted,
                    Name = "Fernando",
                    Surname = "Artola",
                    User = users.ElementAt(3),
                     Id = Guid.NewGuid()
                },
                 new Admin()
                {
                    CI = 46640523,
                    Email = users.ElementAt(4).Email,
                    IsDeleted = users.ElementAt(4).IsDeleted,
                    Name = "Damian",
                    Surname = "Macaluso",
                    User = users.ElementAt(4),
                     Id = Guid.NewGuid()
                },
            };
            admins.ForEach(a => context.Admins.Add(a));
            context.SaveChanges();

            var invitationCodes = new List<InvitationCode>()
            {
                new InvitationCode()
                {
                    Code = "AA245GJ1",
                    IsDeleted = users.ElementAt(0).IsDeleted,
                    ParentUser = users.ElementAt(0),
                     Id = Guid.NewGuid()
                },
                new InvitationCode()
                {
                    Code = "AA245GJ2",
                    IsDeleted = users.ElementAt(1).IsDeleted,
                    ParentUser = users.ElementAt(1),
                     Id = Guid.NewGuid()
                },
                new InvitationCode()
                {
                    Code = "AA245GJ3",
                    IsDeleted = users.ElementAt(2).IsDeleted,
                    ParentUser = users.ElementAt(2),
                     Id = Guid.NewGuid()
                },
                new InvitationCode()
                {
                    Code = "AA245GJ4",
                    IsDeleted = users.ElementAt(3).IsDeleted,
                    ParentUser = users.ElementAt(3),
                     Id = Guid.NewGuid()
                },
                 new InvitationCode()
                {
                    Code = "AA245GJ5",
                    IsDeleted = users.ElementAt(4).IsDeleted,
                    ParentUser = users.ElementAt(4),
                     Id = Guid.NewGuid()
                },
            };
            invitationCodes.ForEach(ic => context.InvitationCodes.Add(ic));
            context.SaveChanges();

            var stockHistories = new List<StockHistory>()
            {
                new StockHistory()
                {
                    DateOfChange = DateTimeOffset.Now,
                    RecordedValue = 0,
                    IsDeleted = false,
                     Id = Guid.NewGuid()
                },
                new StockHistory()
                {
                    DateOfChange = DateTimeOffset.Now,
                    RecordedValue = 1,
                    IsDeleted = false,
                     Id = Guid.NewGuid()
                },
                new StockHistory()
                {
                    DateOfChange = DateTimeOffset.Now,
                    RecordedValue = 2,
                    IsDeleted = false,
                     Id = Guid.NewGuid()
                },
                new StockHistory()
                {
                    DateOfChange = DateTimeOffset.Now,
                    RecordedValue = 3,
                    IsDeleted = false,
                     Id = Guid.NewGuid()
                },
                 new StockHistory()
                {
                    DateOfChange = DateTimeOffset.Now,
                    RecordedValue = 4,
                    IsDeleted = false,
                     Id = Guid.NewGuid()
                },
            };

            var stockNews = new List<StockNews>()
            {
                new StockNews()
                {
                    ReferencedStocks = new List<Stock>(),
                    PublicationDate = DateTimeOffset.Now,
                    Title = "News1",
                    Content = "This is the news number 1",
                    IsDeleted = false,
                     Id = Guid.NewGuid()
                },
                new StockNews()
                {
                    ReferencedStocks = new List<Stock>(),
                    PublicationDate = DateTimeOffset.Now,
                    Title = "News2",
                    Content = "This is the news number 2",
                    IsDeleted = false,
                     Id = Guid.NewGuid()
                },
                new StockNews()
                {
                    ReferencedStocks = new List<Stock>(),
                    PublicationDate = DateTimeOffset.Now,
                    Title = "News3",
                    Content = "This is the news number 3",
                    IsDeleted = false,
                     Id = Guid.NewGuid()
                },
                new StockNews()
                {
                    ReferencedStocks = new List<Stock>(),
                    PublicationDate = DateTimeOffset.Now,
                    Title = "News4",
                    Content = "This is the news number 4",
                    IsDeleted = false,
                     Id = Guid.NewGuid()
                },
                 new StockNews()
                {
                    ReferencedStocks = new List<Stock>(),
                    PublicationDate = DateTimeOffset.Now,
                    Title = "News5",
                    Content = "This is the news number 5",
                    IsDeleted = false,
                     Id = Guid.NewGuid()
                },
            };

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
                     Id = Guid.NewGuid()
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
                     Id = Guid.NewGuid()
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
                     Id = Guid.NewGuid()
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
                     Id = Guid.NewGuid()
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
                     Id = Guid.NewGuid()
                },
            };
            stocks.ForEach(s => context.Stocks.Add(s));
            context.SaveChanges();

            var portfolios = new List<Portfolio>()
            {
                new Portfolio()
                {
                    AvailableMoney = 0,
                    ActionsValue = 0,
                    Transactions = new List<Transaction>(),
                    IsDeleted = players.ElementAt(0).IsDeleted,
                     Id = Guid.NewGuid()
                },
                new Portfolio()
                {
                    AvailableMoney = 5,
                    ActionsValue = 5,
                    Transactions = new List<Transaction>(),
                    IsDeleted = players.ElementAt(1).IsDeleted,
                     Id = Guid.NewGuid()
                },
                new Portfolio()
                {
                    AvailableMoney = 10,
                    ActionsValue = 10,
                    Transactions = new List<Transaction>(),
                    IsDeleted = players.ElementAt(2).IsDeleted,
                     Id = Guid.NewGuid()
                },
                new Portfolio()
                {
                    AvailableMoney = 15,
                    ActionsValue = 15,
                    Transactions = new List<Transaction>(),
                    IsDeleted = players.ElementAt(3).IsDeleted,
                     Id = Guid.NewGuid()
                },
                 new Portfolio()
                {
                    AvailableMoney = 20,
                    ActionsValue = 20,
                    Transactions = new List<Transaction>(),
                    IsDeleted = players.ElementAt(4).IsDeleted,
                     Id = Guid.NewGuid()
                },
            };
            portfolios.ForEach(p => context.Portfolios.Add(p));
            context.SaveChanges();

            var transactions = new List<Transaction>()
            {
                new Transaction()
                {
                    Stock = stocks.ElementAt(0),
                    NetVariation = 0,
                    PercentageVariation = 0,
                    MarketCapital = 0,
                    StockQuantity = 0,
                    TotalValue = 0,
                    TransactionDate = DateTimeOffset.Now,
                    Type = new TransactionType(),
                    Portfolio = portfolios.ElementAt(0),
                    IsDeleted = false,
                     Id = Guid.NewGuid()
                },
                new Transaction()
                {
                    Stock = stocks.ElementAt(1),
                    NetVariation = 1,
                    PercentageVariation = 1,
                    MarketCapital = 1,
                    StockQuantity = 1,
                    TotalValue = 1,
                    TransactionDate = DateTimeOffset.Now,
                    Type = new TransactionType(),
                    Portfolio = portfolios.ElementAt(1),
                    IsDeleted = false,
                     Id = Guid.NewGuid()
                },
                new Transaction()
                {
                    Stock = stocks.ElementAt(2),
                    NetVariation = 2,
                    PercentageVariation = 2,
                    MarketCapital = 2,
                    StockQuantity = 2,
                    TotalValue = 2,
                    TransactionDate = DateTimeOffset.Now,
                    Type = new TransactionType(),
                    Portfolio = portfolios.ElementAt(2),
                    IsDeleted = false,
                     Id = Guid.NewGuid()
                },
                new Transaction()
                {
                    Stock = stocks.ElementAt(3),
                    NetVariation = 3,
                    PercentageVariation = 3,
                    MarketCapital = 3,
                    StockQuantity = 3,
                    TotalValue = 3,
                    TransactionDate = DateTimeOffset.Now,
                    Type = new TransactionType(),
                    Portfolio = portfolios.ElementAt(3),
                    IsDeleted = false,
                     Id = Guid.NewGuid()
                },
                 new Transaction()
                {
                    Stock = stocks.ElementAt(4),
                    NetVariation = 4,
                    PercentageVariation = 4,
                    MarketCapital = 4,
                    StockQuantity = 4,
                    TotalValue = 4,
                    TransactionDate = DateTimeOffset.Now,
                    Type = new TransactionType(),
                    Portfolio = portfolios.ElementAt(0),
                    IsDeleted = false,
                     Id = Guid.NewGuid()
                },
            };
            transactions.ForEach(t => context.Transactions.Add(t));
            context.SaveChanges();

            context.GameSettings.Add(new GameSettings()
            {
                InitialMoney = 1000000,
                MaxTransactionsPerDay = 50,
                IsDeleted = false,
                Id = Guid.NewGuid()
            });
        }
    }
}

