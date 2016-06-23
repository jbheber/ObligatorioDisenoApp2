using Moq;
using Stockapp.Data;
using Stockapp.Data.Access;
using Stockapp.Data.Extensions;
using Stockapp.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Stockapp.Test
{
    public class TransactionRepositoryTest
    {
        [Fact]
        public void GetAllTransactionsTest()
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);
            var stockData = GetStockList();
            var stockSet = new Mock<DbSet<Stock>>().SetupData(stockData);
            var portfolioData = GetPortfolioList();
            var portfolioSet = new Mock<DbSet<Portfolio>>().SetupData(portfolioData);
            var transactionData = GetTransactionList();
            var transactionSet = new Mock<DbSet<Transaction>>().SetupData(transactionData);

            var context = new Mock<Context>() { CallBase = true };
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);
            context.Setup(ctx => ctx.Set<Stock>()).Returns(stockSet.Object);
            context.Setup(ctx => ctx.Set<Portfolio>()).Returns(portfolioSet.Object);
            context.Setup(ctx => ctx.Set<Transaction>()).Returns(transactionSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<Transaction> result = unitOfWork.TransactionRepository.GetAll();

            Assert.Equal(result.SafeCount(), transactionData.Count);
        }

        [Fact]
        public void GetFilterTransactionTest()
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);
            var stockData = GetStockList();
            var stockSet = new Mock<DbSet<Stock>>().SetupData(stockData);
            var portfolioData = GetPortfolioList();
            var portfolioSet = new Mock<DbSet<Portfolio>>().SetupData(portfolioData);
            var transactionData = GetTransactionList();
            var transactionSet = new Mock<DbSet<Transaction>>().SetupData(transactionData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);
            context.Setup(ctx => ctx.Set<Stock>()).Returns(stockSet.Object);
            context.Setup(ctx => ctx.Set<Portfolio>()).Returns(portfolioSet.Object);
            context.Setup(ctx => ctx.Set<Transaction>()).Returns(transactionSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<Transaction> result = unitOfWork.TransactionRepository.Get(p => p.Stock.Code == "SAP", null, "Stock");

            Assert.Equal(
                result.SafeCount(),
                transactionData.Where(d => d.Stock.Code == "SAP" && !d.IsDeleted).SafeCount());
        }

        [Fact]
        public void GetNonDeletedTransactionTest()
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);
            var stockData = GetStockList();
            var stockSet = new Mock<DbSet<Stock>>().SetupData(stockData);
            var portfolioData = GetPortfolioList();
            var portfolioSet = new Mock<DbSet<Portfolio>>().SetupData(portfolioData);
            var transactionData = GetTransactionList();
            var transactionSet = new Mock<DbSet<Transaction>>().SetupData(transactionData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);
            context.Setup(ctx => ctx.Set<Stock>()).Returns(stockSet.Object);
            context.Setup(ctx => ctx.Set<Portfolio>()).Returns(portfolioSet.Object);
            context.Setup(ctx => ctx.Set<Transaction>()).Returns(transactionSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<Transaction> result = unitOfWork.TransactionRepository.Get();

            Assert.Equal(result.SafeCount(), transactionData.Where(d => d.IsDeleted == false).SafeCount());
        }

        [Theory]
        [InlineData(3)]
        [InlineData(1)]
        public void GetByIdTest(int index)
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);
            var stockData = GetStockList();
            var stockSet = new Mock<DbSet<Stock>>().SetupData(stockData);
            var portfolioData = GetPortfolioList();
            var portfolioSet = new Mock<DbSet<Portfolio>>().SetupData(portfolioData);
            var transactionData = GetTransactionList();
            var transactionSet = new Mock<DbSet<Transaction>>().SetupData(transactionData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);
            context.Setup(ctx => ctx.Set<Stock>()).Returns(stockSet.Object);
            context.Setup(ctx => ctx.Set<Portfolio>()).Returns(portfolioSet.Object);
            context.Setup(ctx => ctx.Set<Transaction>()).Returns(transactionSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            var allTransactions = unitOfWork.TransactionRepository.Get();
            var entity = allTransactions.ElementAt(index);

            Assert.Equal(entity, unitOfWork.TransactionRepository.GetById(entity.Id));
        }

        [Fact]
        public void InsertTransactionTest()
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);
            var stockData = GetStockList();
            var stockSet = new Mock<DbSet<Stock>>().SetupData(stockData);
            var portfolioData = GetPortfolioList();
            var portfolioSet = new Mock<DbSet<Portfolio>>().SetupData(portfolioData);
            var transactionData = GetTransactionList();
            var transactionSet = new Mock<DbSet<Transaction>>().SetupData(transactionData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);
            context.Setup(ctx => ctx.Set<Stock>()).Returns(stockSet.Object);
            context.Setup(ctx => ctx.Set<Portfolio>()).Returns(portfolioSet.Object);
            context.Setup(ctx => ctx.Set<Transaction>()).Returns(transactionSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var transaction = new Transaction()
            {
                Stock = new Stock()
                {
                    Code = "SET",
                    Name = "Stock2",
                    Description = "Este es el stock2",
                    UnityValue = 2,
                    StockNews = new List<StockNews>(),
                    StockHistory = new List<StockHistory>(),
                    IsDeleted = false,
                   
                },
                StockQuantity = 3,
                TotalValue = 3,
                TransactionDate = DateTimeOffset.Now,
                Type = new TransactionType(),
                Portfolio = new Portfolio()
                {
                    AvailableMoney = 0,
                    ActionsValue = 0,
                    Transactions = new List<Transaction>(),
                    IsDeleted = false,
                   
                },
                IsDeleted = false,
               
            };

            unitOfWork.TransactionRepository.Insert(transaction);

            var result = unitOfWork.TransactionRepository.GetAll();

            Assert.Equal(transaction, unitOfWork.TransactionRepository.GetById(transaction.Id));
        }

        [Fact]
        public void InsertSingleTransactionTest()
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);
            var stockData = GetStockList();
            var stockSet = new Mock<DbSet<Stock>>().SetupData(stockData);
            var portfolioData = GetPortfolioList();
            var portfolioSet = new Mock<DbSet<Portfolio>>().SetupData(portfolioData);
            var transactionData = GetTransactionList();
            var transactionSet = new Mock<DbSet<Transaction>>().SetupData(transactionData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);
            context.Setup(ctx => ctx.Set<Stock>()).Returns(stockSet.Object);
            context.Setup(ctx => ctx.Set<Portfolio>()).Returns(portfolioSet.Object);
            context.Setup(ctx => ctx.Set<Transaction>()).Returns(transactionSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var transaction = new Transaction()
            {
                Stock = new Stock()
                {
                    Code = "SET",
                    Name = "Stock2",
                    Description = "Este es el stock2",
                    UnityValue = 2,
                    StockNews = new List<StockNews>(),
                    StockHistory = new List<StockHistory>(),
                    IsDeleted = false,
                   
                },
                StockQuantity = 3,
                TotalValue = 3,
                TransactionDate = DateTimeOffset.Now,
                Type = new TransactionType(),
                Portfolio = new Portfolio()
                {
                    AvailableMoney = 0,
                    ActionsValue = 0,
                    Transactions = new List<Transaction>(),
                    IsDeleted = false,
                   
                },
                IsDeleted = false,
               
            };

            unitOfWork.TransactionRepository.Insert(transaction);

            var result = unitOfWork.TransactionRepository.GetAll();

            Assert.True(result.IsNotEmpty());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeleteTransactionByIdTest(int index)
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);
            var stockData = GetStockList();
            var stockSet = new Mock<DbSet<Stock>>().SetupData(stockData);
            var portfolioData = GetPortfolioList();
            var portfolioSet = new Mock<DbSet<Portfolio>>().SetupData(portfolioData);
            var transactionData = GetTransactionList();
            var transactionSet = new Mock<DbSet<Transaction>>().SetupData(transactionData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);
            context.Setup(ctx => ctx.Set<Stock>()).Returns(stockSet.Object);
            context.Setup(ctx => ctx.Set<Portfolio>()).Returns(portfolioSet.Object);
            context.Setup(ctx => ctx.Set<Transaction>()).Returns(transactionSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var elementId = transactionData.ElementAt(index).Id;

            unitOfWork.TransactionRepository.Delete(elementId);
            unitOfWork.Save();

            var allTransactions = unitOfWork.TransactionRepository.GetAll();

            Assert.True(allTransactions.First(u => u.Id == elementId).IsDeleted);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeleteTransactionTest(int index)
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);
            var stockData = GetStockList();
            var stockSet = new Mock<DbSet<Stock>>().SetupData(stockData);
            var portfolioData = GetPortfolioList();
            var portfolioSet = new Mock<DbSet<Portfolio>>().SetupData(portfolioData);
            var transactionData = GetTransactionList();
            var transactionSet = new Mock<DbSet<Transaction>>().SetupData(transactionData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);
            context.Setup(ctx => ctx.Set<Stock>()).Returns(stockSet.Object);
            context.Setup(ctx => ctx.Set<Portfolio>()).Returns(portfolioSet.Object);
            context.Setup(ctx => ctx.Set<Transaction>()).Returns(transactionSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var element = transactionData.ElementAt(index);

            unitOfWork.TransactionRepository.Delete(element);
            unitOfWork.Save();

            var allTransactions = unitOfWork.TransactionRepository.GetAll();

            Assert.True(allTransactions.First(p => p.Id == element.Id).IsDeleted);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void UpdateStockNewsTest(int index)
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);
            var stockData = GetStockList();
            var stockSet = new Mock<DbSet<Stock>>().SetupData(stockData);
            var portfolioData = GetPortfolioList();
            var portfolioSet = new Mock<DbSet<Portfolio>>().SetupData(portfolioData);
            var transactionData = GetTransactionList();
            var transactionSet = new Mock<DbSet<Transaction>>().SetupData(transactionData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);
            context.Setup(ctx => ctx.Set<Stock>()).Returns(stockSet.Object);
            context.Setup(ctx => ctx.Set<Portfolio>()).Returns(portfolioSet.Object);
            context.Setup(ctx => ctx.Set<Transaction>()).Returns(transactionSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var element = transactionData.ElementAt(index);

            var originalNetVariation = element.StockQuantity;

            element.StockQuantity = 1000;

            unitOfWork.TransactionRepository.Update(element);
            unitOfWork.Save();

            var allTransactions = unitOfWork.TransactionRepository.GetAll();

            Assert.NotEqual(allTransactions.First(u => u.Id == element.Id).StockQuantity, originalNetVariation);
        }

        private List<Stock> GetStockList()
        {
            return new List<Stock>()
            {
                new Stock()
                {
                    Code = "MSFT",
                    Name = "Stock1",
                    Description = "Este es el stock1",
                    UnityValue = 1,
                    StockNews = new List<StockNews>(),
                    StockHistory = new List<StockHistory>(),
                    IsDeleted = false,
                   Id = 1
                },
                new Stock()
                {
                    Code = "SET",
                    Name = "Stock2",
                    Description = "Este es el stock2",
                    UnityValue = 2,
                    StockNews = new List<StockNews>(),
                    StockHistory = new List<StockHistory>(),
                    IsDeleted = false,
                   Id = 2
                },
                new Stock()
                {
                    Code = "GET",
                    Name = "Stock3",
                    Description = "Este es el stock3",
                    UnityValue = 3,
                    StockNews = new List<StockNews>(),
                    StockHistory = new List<StockHistory>(),
                    IsDeleted = false,
                   Id = 3
                },
                new Stock()
                {
                    Code = "SAP",
                    Name = "Stock4",
                    Description = "Este es el stock4",
                    UnityValue = 4,
                    StockNews = new List<StockNews>(),
                    StockHistory = new List<StockHistory>(),
                    IsDeleted = false,
                   Id = 4
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
                   Id = 5
                },
            };
        }

        private List<User> GetUserList()
        {
            return new List<User>
            {
                new User()
                {
                    Name = "jbheber",
                    Password = "Jb12345",
                    Email = "juanbheber@outlook.com",
                    IsAdmin = false,
                    IsDeleted = false,
                   Id = 1
                },
                new User()
                {
                    Name = "fartolaa",
                    Password = "Art12345",
                    Email = "artolaa@outlook.com",
                    IsAdmin = false,
                    IsDeleted = false,
                   Id = 2
                },
                new User()
                {
                    Name = "jheber",
                    Password = "Jh1234554",
                    Email = "juanbautistaheber@gmail.com",
                    IsAdmin = true,
                    IsDeleted = false,
                   Id = 3
                },
                new User()
                {
                    Name = "arto",
                    Password = "Artoo1234554",
                    Email = "arto@gmail.com",
                    IsAdmin = true,
                    IsDeleted = true,
                   Id = 4
                },
                new User()
                {
                    Name = "maca",
                    Password = "Maluso1234554",
                    Email = "macaluso@gmail.com",
                    IsAdmin = false,
                    IsDeleted = false,
                   Id = 5
                }
            };
        }

        private List<Player> GetPlayerList()
        {
            var users = this.GetUserList();
            return new List<Player>()
            {
                new Player()
                {
                    CI = 46640529,
                    Email = users.ElementAt(0).Email,
                    IsDeleted = users.ElementAt(0).IsDeleted,
                    Name = "Juan Bautista",
                    Surname = "Heber",
                    User = users.ElementAt(0),
                    UserId = users.ElementAt(0).Id,
                   Id = 1
                },
                new Player()
                {
                    CI = 46640520,
                    Email = users.ElementAt(1).Email,
                    IsDeleted = users.ElementAt(1).IsDeleted,
                    Name = "Fernando",
                    Surname = "Artola",
                    User = users.ElementAt(1),
                    UserId = users.ElementAt(1).Id,
                   Id = 2
                },
                new Player()
                {
                    CI = 46640521,
                    Email = users.ElementAt(2).Email,
                    IsDeleted = users.ElementAt(2).IsDeleted,
                    Name = "Juan",
                    Surname = "Heber",
                    User = users.ElementAt(2),
                    UserId = users.ElementAt(2).Id,
                   Id = 3
                },
                new Player()
                {
                    CI = 46640522,
                    Email = users.ElementAt(3).Email,
                    IsDeleted = users.ElementAt(3).IsDeleted,
                    Name = "Fernando",
                    Surname = "Artola",
                    User = users.ElementAt(3),
                    UserId = users.ElementAt(3).Id,
                   Id = 4
                },
                 new Player()
                {
                    CI = 46640523,
                    Email = users.ElementAt(4).Email,
                    IsDeleted = users.ElementAt(4).IsDeleted,
                    Name = "Damian",
                    Surname = "Macaluso",
                    User = users.ElementAt(4),
                    UserId = users.ElementAt(4).Id,
                   Id = 5
                },
            };
        }

        private List<Portfolio> GetPortfolioList()
        {
            var players = this.GetPlayerList();
            return new List<Portfolio>()
            {
                new Portfolio()
                {
                    AvailableMoney = 0,
                    ActionsValue = 0,
                    Transactions = new List<Transaction>(),
                    IsDeleted = players.ElementAt(0).IsDeleted,
                   Id = 1
                },
                new Portfolio()
                {
                    AvailableMoney = 5,
                    ActionsValue = 5,
                    Transactions = new List<Transaction>(),
                    IsDeleted = players.ElementAt(1).IsDeleted,
                   Id = 2
                },
                new Portfolio()
                {
                    AvailableMoney = 10,
                    ActionsValue = 10,
                    Transactions = new List<Transaction>(),
                    IsDeleted = players.ElementAt(2).IsDeleted,
                   Id = 3
                },
                new Portfolio()
                {
                    AvailableMoney = 15,
                    ActionsValue = 15,
                    Transactions = new List<Transaction>(),
                    IsDeleted = players.ElementAt(3).IsDeleted,
                   Id = 4
                },
                 new Portfolio()
                {
                    AvailableMoney = 20,
                    ActionsValue = 20,
                    Transactions = new List<Transaction>(),
                    IsDeleted = players.ElementAt(4).IsDeleted,
                   Id = 5
                },
            };
        }

        private List<Transaction> GetTransactionList()
        {
            var stocks = this.GetStockList();
            var portfolios = this.GetPortfolioList();
            return new List<Transaction>()
            {
                new Transaction()
                {
                    Stock = stocks.ElementAt(0),
                    StockId = stocks.ElementAt(0).Id,
                    StockQuantity = 0,
                    TotalValue = 0,
                    TransactionDate = DateTimeOffset.Now,
                    Type = new TransactionType(),
                    Portfolio = portfolios.ElementAt(0),
                    PortfolioId = portfolios.ElementAt(0).Id,
                    IsDeleted = false,
                   Id = 1
                },
                new Transaction()
                {
                    Stock = stocks.ElementAt(1),
                    StockId = stocks.ElementAt(1).Id,
                    StockQuantity = 1,
                    TotalValue = 1,
                    TransactionDate = DateTimeOffset.Now,
                    Type = new TransactionType(),
                    Portfolio = portfolios.ElementAt(1),
                    PortfolioId = portfolios.ElementAt(1).Id,
                    IsDeleted = false,
                   Id = 2
                },
                new Transaction()
                {
                    Stock = stocks.ElementAt(2),
                    StockId = stocks.ElementAt(2).Id,
                    StockQuantity = 2,
                    TotalValue = 2,
                    TransactionDate = DateTimeOffset.Now,
                    Type = new TransactionType(),
                    Portfolio = portfolios.ElementAt(2),
                    PortfolioId = portfolios.ElementAt(2).Id,
                    IsDeleted = false,
                   Id = 3
                },
                new Transaction()
                {
                    Stock = stocks.ElementAt(3),
                    StockId = stocks.ElementAt(3).Id,
                    StockQuantity = 3,
                    TotalValue = 3,
                    TransactionDate = DateTimeOffset.Now,
                    Type = new TransactionType(),
                    Portfolio = portfolios.ElementAt(3),
                    PortfolioId = portfolios.ElementAt(3).Id,
                    IsDeleted = false,
                   Id = 4
                },
                 new Transaction()
                {
                    Stock = stocks.ElementAt(4),
                    StockId = stocks.ElementAt(4).Id,
                    StockQuantity = 4,
                    TotalValue = 4,
                    TransactionDate = DateTimeOffset.Now,
                    Type = new TransactionType(),
                    Portfolio = portfolios.ElementAt(0),
                    PortfolioId = portfolios.ElementAt(0).Id,
                    IsDeleted = false,
                   Id = 5
                },
            };
        }
    }
}

