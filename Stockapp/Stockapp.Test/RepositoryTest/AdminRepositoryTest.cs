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
    public class AdminRepositoryTest
    {
        [Fact]
        public void GetAllAdminTest()
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var adminData = GetAdminList();
            var adminSet = new Mock<DbSet<Admin>>().SetupData(adminData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Admin>()).Returns(adminSet.Object);


            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<Admin> result = unitOfWork.AdminRepository.GetAll();

            Assert.Equal(result.SafeCount(), adminData.Count);
        }

        [Fact]
        public void GetFilterAdminTest()
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var adminData = GetAdminList();
            var adminSet = new Mock<DbSet<Admin>>().SetupData(adminData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Admin>()).Returns(adminSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<Admin> result = unitOfWork.AdminRepository.Get(p => p.User.IsAdmin == true, null, "User");

            Assert.Equal(
                result.SafeCount(),
                userData.Where(d => d.IsAdmin == true && !d.IsDeleted).SafeCount());
        }

        [Fact]
        public void GetNonDeletedAdminTest()
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var adminData = GetAdminList();
            var adminSet = new Mock<DbSet<Admin>>().SetupData(adminData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Admin>()).Returns(adminSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<Admin> result = unitOfWork.AdminRepository.Get();

            Assert.Equal(result.SafeCount(), adminData.Where(d => d.IsDeleted == false).SafeCount());
        }

        [Theory]
        [InlineData(3)]
        [InlineData(1)]
        public void GetByIdTest(int index)
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var adminData = GetAdminList();
            var adminSet = new Mock<DbSet<Admin>>().SetupData(adminData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Admin>()).Returns(adminSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            var allAdmins = unitOfWork.AdminRepository.Get();
            var entity = allAdmins.ElementAt(index);

            Assert.Equal(entity, unitOfWork.AdminRepository.GetById(entity.Id));
        }

        [Fact]
        public void InsertAdminTest()
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var adminData = GetAdminList();
            var adminSet = new Mock<DbSet<Admin>>().SetupData(adminData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Admin>()).Returns(adminSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var admin = new Admin()
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

            unitOfWork.AdminRepository.Insert(admin);

            var result = unitOfWork.AdminRepository.GetAll();

            Assert.Equal(admin, unitOfWork.AdminRepository.GetById(admin.Id));
        }

        [Fact]
        public void InsertSingleAdminTest()
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var adminData = GetAdminList();
            var adminSet = new Mock<DbSet<Admin>>().SetupData(adminData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Admin>()).Returns(adminSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var admin = new Admin()
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

            unitOfWork.AdminRepository.Insert(admin);

            var result = unitOfWork.AdminRepository.GetAll();

            Assert.True(result.isNotEmpty());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeleteAdminByIdTest(int index)
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var adminData = GetAdminList();
            var adminSet = new Mock<DbSet<Admin>>().SetupData(adminData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Admin>()).Returns(adminSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var elementId = adminData.ElementAt(index).Id;

            unitOfWork.AdminRepository.Delete(elementId);
            unitOfWork.Save();

            var allAdmins = unitOfWork.AdminRepository.GetAll();

            Assert.True(allAdmins.First(u => u.Id == elementId).IsDeleted);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeleteAdminTest(int index)
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var adminData = GetAdminList();
            var adminSet = new Mock<DbSet<Admin>>().SetupData(adminData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Admin>()).Returns(adminSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var element = adminData.ElementAt(index);

            unitOfWork.AdminRepository.Delete(element);
            unitOfWork.Save();

            var allAdmins = unitOfWork.AdminRepository.GetAll();

            Assert.True(allAdmins.First(p => p.Id == element.Id).IsDeleted);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void UpdateAdminTest(int index)
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var adminData = GetAdminList();
            var adminSet = new Mock<DbSet<Admin>>().SetupData(adminData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<Admin>()).Returns(adminSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var element = adminData.ElementAt(index);

            var originalName = element.Name;
            var originalSurname = element.Surname;

            element.Name = "Player";
            element.Surname = "Modified";

            unitOfWork.AdminRepository.Update(element);
            unitOfWork.Save();

            var allAdmins = unitOfWork.AdminRepository.GetAll();

            Assert.NotEqual(allAdmins.First(u => u.Id == element.Id).Name, originalName);
            Assert.NotEqual(allAdmins.First(u => u.Id == element.Id).Surname, originalSurname);
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

        private List<Admin> GetAdminList()
        {
            var users = this.GetUserList();
            return new List<Admin>()
            {
                new Admin()
                {
                    CI = 46640529,
                    Email = users.ElementAt(0).Email,
                    IsDeleted = users.ElementAt(0).IsDeleted,
                    Name = "Juan Bautista",
                    Surname = "Heber",
                    User = users.ElementAt(0),
                    UserId = users.ElementAt(0).Id,
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
                    UserId = users.ElementAt(1).Id,
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
                    UserId = users.ElementAt(2).Id,
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
                    UserId = users.ElementAt(3).Id,
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
                    UserId = users.ElementAt(4).Id,
                    Id = Guid.NewGuid()
                },
            };
        }
    }
}