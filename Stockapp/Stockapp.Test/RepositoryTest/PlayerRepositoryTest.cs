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
    public class PlayerRepositoryTest
    {
        [Fact]
        public void GetAllPlayerTest()
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);


            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<Player> result = unitOfWork.PlayerRepository.GetAll();

            Assert.Equal(result.SafeCount(), playerData.Count);
        }

        [Fact]
        public void GetFilterPlayerTest()
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<Player> result = unitOfWork.PlayerRepository.Get(p => p.User.IsAdmin == true, null, "User");

            Assert.Equal(
                result.SafeCount(),
                userData.Where(d => d.IsAdmin == true && !d.IsDeleted).SafeCount());
        }

        [Fact]
        public void GetNonDeletedPlayersTest()
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<Player> result = unitOfWork.PlayerRepository.Get();

            Assert.Equal(result.SafeCount(), playerData.Where(d => d.IsDeleted == false).SafeCount());
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

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            var allPlayers = unitOfWork.PlayerRepository.Get();
            var entity = allPlayers.ElementAt(index);

            Assert.Equal(entity, unitOfWork.PlayerRepository.GetById(entity.Id));
        }

        [Fact]
        public void InsertPlayerTest()
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var player = new Player()
            {
                CI = 46640529,
                Email = "test@test.com",
                IsDeleted = false,
                Name = "Player",
                Surname = "Test",
                User = new User()
                {
                    Name = "test1",
                    Password = "te12345678",
                    Email = "test@test.com",
                    IsAdmin = false,
                    IsDeleted = false,
                    Id = Guid.NewGuid()
                },
                Id = Guid.NewGuid()
            };

            unitOfWork.PlayerRepository.Insert(player);

            var result = unitOfWork.PlayerRepository.GetAll();

            Assert.Equal(player, unitOfWork.PlayerRepository.GetById(player.Id));
        }

        [Fact]
        public void InsertSinglePlayerTest()
        {
            var userSet = new Mock<DbSet<User>>().SetupData();
            var playerSet = new Mock<DbSet<Player>>().SetupData();

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var player = new Player()
            {
                CI = 46640529,
                Email = "test@test.com",
                IsDeleted = false,
                Name = "Player",
                Surname = "Test",
                User = new User()
                {
                    Name = "test1",
                    Password = "te12345678",
                    Email = "test@test.com",
                    IsAdmin = false,
                    IsDeleted = false,
                    Id = Guid.NewGuid()
                },
                Id = Guid.NewGuid()
            };

            unitOfWork.PlayerRepository.Insert(player);

            var result = unitOfWork.PlayerRepository.GetAll();

            Assert.True(result.isNotEmpty());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeletePlayerByIdTest(int index)
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var elementId = playerData.ElementAt(index).Id;

            unitOfWork.PlayerRepository.Delete(elementId);
            unitOfWork.Save();

            var allPlayers = unitOfWork.PlayerRepository.GetAll();

            Assert.True(allPlayers.First(u => u.Id == elementId).IsDeleted);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeletePlayerTest(int index)
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var element = playerData.ElementAt(index);

            unitOfWork.PlayerRepository.Delete(element);
            unitOfWork.Save();

            var allPlayers = unitOfWork.PlayerRepository.GetAll();

            Assert.True(allPlayers.First(p => p.Id == element.Id).IsDeleted);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void UpdatePlayerTest(int index)
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var playerData = GetPlayerList();
            var playerSet = new Mock<DbSet<Player>>().SetupData(playerData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Player>()).Returns(playerSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var element = playerData.ElementAt(index);

            var originalName = element.Name;
            var originalSurname = element.Surname;

            element.Name = "Player";
            element.Surname = "Modified";

            unitOfWork.PlayerRepository.Update(element);
            unitOfWork.Save();

            var allPlayers = unitOfWork.PlayerRepository.GetAll();

            Assert.NotEqual(allPlayers.First(u => u.Id == element.Id).Name, originalName);
            Assert.NotEqual(allPlayers.First(u => u.Id == element.Id).Surname, originalSurname);
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
                    Id = Guid.NewGuid()
                },
                new User()
                {
                    Name = "fartolaa",
                    Password = "Art12345",
                    Email = "artolaa@outlook.com",
                    IsAdmin = false,
                    IsDeleted = false,
                    Id = Guid.NewGuid()
                },
                new User()
                {
                    Name = "jheber",
                    Password = "Jh1234554",
                    Email = "juanbautistaheber@gmail.com",
                    IsAdmin = true,
                    IsDeleted = false,
                    Id = Guid.NewGuid()
                },
                new User()
                {
                    Name = "arto",
                    Password = "Artoo1234554",
                    Email = "arto@gmail.com",
                    IsAdmin = true,
                    IsDeleted = true,
                    Id = Guid.NewGuid()
                },
                new User()
                {
                    Name = "maca",
                    Password = "Maluso1234554",
                    Email = "macaluso@gmail.com",
                    IsAdmin = false,
                    IsDeleted = false,
                    Id = Guid.NewGuid()
                }
            };
        }

        private List<Player> GetPlayerList()
        {
            var users = this.GetUserList();
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
                    UserId = users.ElementAt(0).Id,
                    Portfolio = new Portfolio(),
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
                    UserId = users.ElementAt(1).Id,
                    Portfolio = new Portfolio(),
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
                    UserId = users.ElementAt(2).Id,
                    Portfolio = new Portfolio(),
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
                    UserId = users.ElementAt(3).Id,
                    Portfolio = new Portfolio(),
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
                    UserId = users.ElementAt(4).Id,
                    Portfolio = new Portfolio(),
                    Id = Guid.NewGuid()
                }
            };
            players.ForEach(p => p.PortfolioId = p.Portfolio.Id);
            return players;
        }
    }
}
