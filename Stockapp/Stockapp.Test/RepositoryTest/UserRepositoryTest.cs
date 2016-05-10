using Moq;
using Stockapp.Data;
using Stockapp.Data.Access;
using Stockapp.Data.Extensions;
using Stockapp.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Xunit;

namespace Stockapp.Test
{
    public class UserRepositoryTest
    {
        [Fact]
        public void GetAllUsersTest()
        {
            var data = GetUserList();
            var set = new Mock<DbSet<User>>().SetupData(data);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(set.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<User> result = unitOfWork.UserRepository.GetAll();

            Assert.Equal(result.SafeCount(), data.Count);
        }

        [Fact]
        public void GetAllFilterUsersTest()
        {
            var data = GetUserList();
            var set = new Mock<DbSet<User>>().SetupData(data);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(set.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<User> result = unitOfWork.UserRepository.GetAll(u => u.IsAdmin == true);

            Assert.Equal(result.SafeCount(), data.Where(d => d.IsAdmin == true).SafeCount());
        }

        [Fact]
        public void GetNonDeletedUsersTest()
        {
            var data = GetUserList();
            var set = new Mock<DbSet<User>>().SetupData(data);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(set.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<User> result = unitOfWork.UserRepository.Get();

            Assert.Equal(result.SafeCount(), data.Where(d => d.IsDeleted == false).SafeCount());
        }

        [Theory]
        [InlineData(3)]
        [InlineData(1)]
        public void GetByIdTest(int index)
        {
            var data = GetUserList();
            var set = new Mock<DbSet<User>>().SetupData(data);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(set.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            var allUsers = unitOfWork.UserRepository.Get();
            var entity = allUsers.ElementAt(index);

            Assert.Equal(entity, unitOfWork.UserRepository.GetById(entity.Id));
        }

        [Fact]
        public void InsertUserTest()
        {
            var data = GetUserList();
            var set = new Mock<DbSet<User>>().SetupData(data);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(set.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var newUser = new User()
            {
                Name = "dario",
                Password = "DRodriguez.22",
                Email = "dario.rodriguez@outlook.com",
                IsAdmin = false,
                IsDeleted = false,
                Id = Guid.NewGuid()
            };

            unitOfWork.UserRepository.Insert(newUser);

            var result = unitOfWork.UserRepository.GetAll();

            Assert.Equal(newUser, unitOfWork.UserRepository.GetById(newUser.Id));
        }

        [Fact]
        public void InsertSingleUserTest()
        {
            var set = new Mock<DbSet<User>>().SetupData();

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(set.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var newUser = new User()
            {
                Name = "dario",
                Password = "DRodriguez.22",
                Email = "dario.rodriguez@outlook.com",
                IsAdmin = false,
                IsDeleted = false,
                Id = Guid.NewGuid()
            };

            unitOfWork.UserRepository.Insert(newUser);

            var result = unitOfWork.UserRepository.GetAll();

            Assert.True(result.isNotEmpty());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeleteUserByIdTest(int index)
        {
            var data = GetUserList();
            var set = new Mock<DbSet<User>>().SetupData(data);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(set.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var elementId = data.ElementAt(index).Id;

            unitOfWork.UserRepository.Delete(elementId);
            unitOfWork.Save();

            var allUsers = unitOfWork.UserRepository.GetAll();

            Assert.True(allUsers.First(u => u.Id == elementId).IsDeleted);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeleteUserTest(int index)
        {
            var data = GetUserList();
            var set = new Mock<DbSet<User>>().SetupData(data);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(set.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var element = data.ElementAt(index);

            unitOfWork.UserRepository.Delete(element);
            unitOfWork.Save();

            var allUsers = unitOfWork.UserRepository.GetAll();

            Assert.True(allUsers.First(u => u.Id == element.Id).IsDeleted);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void UpdateUserTest(int index)
        {
            var data = GetUserList();
            var set = new Mock<DbSet<User>>().SetupData(data);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(set.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var element = data.ElementAt(index);

            var originalName = element.Name;
            var originalPassword = element.Password;

            element.Name = "Modified User";
            element.Password = "Mu.1234554321";

            unitOfWork.UserRepository.Update(element);
            unitOfWork.Save();

            var allUsers = unitOfWork.UserRepository.GetAll();

            Assert.NotEqual(allUsers.First(u => u.Id == element.Id).Name, originalName);
            Assert.NotEqual(allUsers.First(u => u.Id == element.Id).Password, originalPassword);
        }

        private List<User> GetUserList()
        {
            return new List<User>
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
        }

    }
}
