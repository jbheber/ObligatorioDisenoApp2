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
    public class PortfolioRepositoryTest
    {
        [Fact]
        public void GetAllPortfolioTest()
        {
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);
            var portfolioData = GetPortfolioList();
            var portfolioSet = new Mock<DbSet<Portfolio>>().SetupData(portfolioData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);
            context.Setup(ctx => ctx.Set<Portfolio>()).Returns(portfolioSet.Object);


            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<Portfolio> result = unitOfWork.PortfolioRepository.GetAll();

            Assert.Equal(result.SafeCount(), portfolioData.Count);
        }

        [Fact]
        public void GetFilterPortfolioTest()
        {
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);
            var portfolioData = GetPortfolioList();
            var portfolioSet = new Mock<DbSet<Portfolio>>().SetupData(portfolioData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);
            context.Setup(ctx => ctx.Set<Portfolio>()).Returns(portfolioSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<Portfolio> result = unitOfWork.PortfolioRepository.Get(p => p.AvailableMoney > 100, null, "User");

            Assert.Equal(
                result.SafeCount(),
                portfolioData.Where(d => d.AvailableMoney > 100 && !d.IsDeleted).SafeCount());
        }

        [Fact]
        public void GetNonDeletedPortfolioTest()
        {
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);
            var portfolioData = GetPortfolioList();
            var portfolioSet = new Mock<DbSet<Portfolio>>().SetupData(portfolioData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);
            context.Setup(ctx => ctx.Set<Portfolio>()).Returns(portfolioSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<Portfolio> result = unitOfWork.PortfolioRepository.Get();

            Assert.Equal(result.SafeCount(), portfolioData.Where(d => d.IsDeleted == false).SafeCount());
        }

        [Theory]
        [InlineData(3)]
        [InlineData(1)]
        public void GetByIdTest(int index)
        {
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);
            var portfolioData = GetPortfolioList();
            var portfolioSet = new Mock<DbSet<Portfolio>>().SetupData(portfolioData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);
            context.Setup(ctx => ctx.Set<Portfolio>()).Returns(portfolioSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            var allPortfolios = unitOfWork.PortfolioRepository.Get();
            var entity = allPortfolios.ElementAt(index);

            Assert.Equal(entity, unitOfWork.PortfolioRepository.GetById(entity.Id));
        }

        [Fact]
        public void InsertPortfolioTest()
        {
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);
            var portfolioData = GetPortfolioList();
            var portfolioSet = new Mock<DbSet<Portfolio>>().SetupData(portfolioData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);
            context.Setup(ctx => ctx.Set<Portfolio>()).Returns(portfolioSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var portfolio = new Portfolio()
            {
                AvailableMoney = 0,
                ActionsValue = 0,
                Transactions = new List<Transaction>(),
                IsDeleted = false,

            };

            unitOfWork.PortfolioRepository.Insert(portfolio);

            var result = unitOfWork.PortfolioRepository.GetAll();

            Assert.Equal(portfolio, unitOfWork.PortfolioRepository.GetById(portfolio.Id));
        }

        [Fact]
        public void InsertSinglePortfolioTest()
        {
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);
            var portfolioData = GetPortfolioList();
            var portfolioSet = new Mock<DbSet<Portfolio>>().SetupData(portfolioData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);
            context.Setup(ctx => ctx.Set<Portfolio>()).Returns(portfolioSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var portfolio = new Portfolio()
            {
                AvailableMoney = 0,
                ActionsValue = 0,
                Transactions = new List<Transaction>(),
                IsDeleted = false,

            };

            unitOfWork.PortfolioRepository.Insert(portfolio);

            var result = unitOfWork.PortfolioRepository.GetAll();

            Assert.True(result.IsNotEmpty());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeletePortfolioByIdTest(int index)
        {
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);
            var portfolioData = GetPortfolioList();
            var portfolioSet = new Mock<DbSet<Portfolio>>().SetupData(portfolioData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);
            context.Setup(ctx => ctx.Set<Portfolio>()).Returns(portfolioSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var elementId = portfolioData.ElementAt(index).Id;

            unitOfWork.PortfolioRepository.Delete(elementId);
            unitOfWork.Save();

            var allPortfolios = unitOfWork.PortfolioRepository.GetAll();

            Assert.True(allPortfolios.First(u => u.Id == elementId).IsDeleted);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeletePortfolioTest(int index)
        {
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);
            var portfolioData = GetPortfolioList();
            var portfolioSet = new Mock<DbSet<Portfolio>>().SetupData(portfolioData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);
            context.Setup(ctx => ctx.Set<Portfolio>()).Returns(portfolioSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var element = portfolioData.ElementAt(index);

            unitOfWork.PortfolioRepository.Delete(element);
            unitOfWork.Save();

            var allPortfolios = unitOfWork.PortfolioRepository.GetAll();

            Assert.True(allPortfolios.First(p => p.Id == element.Id).IsDeleted);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void UpdateAdminTest(int index)
        {
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);
            var portfolioData = GetPortfolioList();
            var portfolioSet = new Mock<DbSet<Portfolio>>().SetupData(portfolioData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);
            context.Setup(ctx => ctx.Set<Portfolio>()).Returns(portfolioSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var element = portfolioData.ElementAt(index);

            var originalAvailableMoney = element.AvailableMoney;
            var originalActionValue = element.ActionsValue;

            element.AvailableMoney = 100;
            element.ActionsValue = 100;

            unitOfWork.PortfolioRepository.Update(element);
            unitOfWork.Save();

            var allPortfolios = unitOfWork.PortfolioRepository.GetAll();

            Assert.NotEqual(allPortfolios.First(u => u.Id == element.Id).AvailableMoney, originalAvailableMoney);
            Assert.NotEqual(allPortfolios.First(u => u.Id == element.Id).ActionsValue, originalActionValue);
        }

        private List<User> GetUserList()
        {
            return new List<User>
            {
                new User()
                {
                    Id = 1,
                    Name = "jbheber",
                    Password = "Jb12345",
                    Email = "juanbheber@outlook.com",
                    IsAdmin = false,
                    IsDeleted = false,

                },
                new User()
                {
                    Id = 2,
                    Name = "fartolaa",
                    Password = "Art12345",
                    Email = "artolaa@outlook.com",
                    IsAdmin = false,
                    IsDeleted = false,

                },
                new User()
                {
                    Id = 3,
                    Name = "jheber",
                    Password = "Jh1234554",
                    Email = "juanbautistaheber@gmail.com",
                    IsAdmin = true,
                    IsDeleted = false,

                },
                new User()
                {
                    Id = 4,
                    Name = "arto",
                    Password = "Artoo1234554",
                    Email = "arto@gmail.com",
                    IsAdmin = true,
                    IsDeleted = true,

                },
                new User()
                {
                    Id = 5,
                    Name = "maca",
                    Password = "Maluso1234554",
                    Email = "macaluso@gmail.com",
                    IsAdmin = false,
                    IsDeleted = false,

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
                    Id = 1,
                    CI = 46640529,
                    Email = users.ElementAt(0).Email,
                    IsDeleted = users.ElementAt(0).IsDeleted,
                    Name = "Juan Bautista",
                    Surname = "Heber",
                    User = users.ElementAt(0),
                    UserId = users.ElementAt(0).Id,

                },
                new Player()
                {
                    Id = 2,
                    CI = 46640520,
                    Email = users.ElementAt(1).Email,
                    IsDeleted = users.ElementAt(1).IsDeleted,
                    Name = "Fernando",
                    Surname = "Artola",
                    User = users.ElementAt(1),
                    UserId = users.ElementAt(1).Id,

                },
                new Player()
                {
                    Id = 3,
                    CI = 46640521,
                    Email = users.ElementAt(2).Email,
                    IsDeleted = users.ElementAt(2).IsDeleted,
                    Name = "Juan",
                    Surname = "Heber",
                    User = users.ElementAt(2),
                    UserId = users.ElementAt(2).Id,

                },
                new Player()
                {
                    Id = 4,
                    CI = 46640522,
                    Email = users.ElementAt(3).Email,
                    IsDeleted = users.ElementAt(3).IsDeleted,
                    Name = "Fernando",
                    Surname = "Artola",
                    User = users.ElementAt(3),
                    UserId = users.ElementAt(3).Id,

                },
                 new Player()
                {
                     Id = 5,
                    CI = 46640523,
                    Email = users.ElementAt(4).Email,
                    IsDeleted = users.ElementAt(4).IsDeleted,
                    Name = "Damian",
                    Surname = "Macaluso",
                    User = users.ElementAt(4),
                    UserId = users.ElementAt(4).Id,

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
                    Id = 1,
                    AvailableMoney = 0,
                    ActionsValue = 0,
                    Transactions = new List<Transaction>(),
                    IsDeleted = players.ElementAt(0).IsDeleted,

                },
                new Portfolio()
                {
                    Id = 2,
                    AvailableMoney = 5,
                    ActionsValue = 5,
                    Transactions = new List<Transaction>(),
                    IsDeleted = players.ElementAt(1).IsDeleted,

                },
                new Portfolio()
                {
                    Id = 3,
                    AvailableMoney = 10,
                    ActionsValue = 10,
                    Transactions = new List<Transaction>(),
                    IsDeleted = players.ElementAt(2).IsDeleted,

                },
                new Portfolio()
                {
                    Id = 4,
                    AvailableMoney = 15,
                    ActionsValue = 15,
                    Transactions = new List<Transaction>(),
                    IsDeleted = players.ElementAt(3).IsDeleted,

                },
                 new Portfolio()
                {
                     Id = 5,
                    AvailableMoney = 20,
                    ActionsValue = 20,
                    Transactions = new List<Transaction>(),
                    IsDeleted = players.ElementAt(4).IsDeleted,

                },
            };
        }
    }
}
