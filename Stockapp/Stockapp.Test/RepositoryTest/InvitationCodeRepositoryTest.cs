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
    public class InvitationCodeRepositoryTest
    {
        [Fact]
        public void GetAllInvitationCodeTest()
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var invitationCodeData = GetInvitationCodeList();
            var invitationCodeSet = new Mock<DbSet<InvitationCode>>().SetupData(invitationCodeData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<InvitationCode>()).Returns(invitationCodeSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<InvitationCode> result = unitOfWork.InvitationCodeRepository.GetAll();

            Assert.Equal(result.SafeCount(), invitationCodeData.Count);
        }

        [Fact]
        public void GetFilterInvitationCodeTest()
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var invitationCodeData = GetInvitationCodeList();
            var invitationCodeSet = new Mock<DbSet<InvitationCode>>().SetupData(invitationCodeData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<InvitationCode>()).Returns(invitationCodeSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<InvitationCode> result = unitOfWork.InvitationCodeRepository.Get(p => p.ParentUser.IsAdmin == true, null, "User");

            Assert.Equal(
                result.SafeCount(),
                userData.Where(d => d.IsAdmin == true && !d.IsDeleted).SafeCount());
        }

        [Fact]
        public void GetNonDeletedInvitationCodeTest()
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var invitationCodeData = GetInvitationCodeList();
            var invitationCodeSet = new Mock<DbSet<InvitationCode>>().SetupData(invitationCodeData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<InvitationCode>()).Returns(invitationCodeSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<InvitationCode> result = unitOfWork.InvitationCodeRepository.Get();

            Assert.Equal(result.SafeCount(), invitationCodeData.Where(d => d.IsDeleted == false).SafeCount());
        }

        [Theory]
        [InlineData(3)]
        [InlineData(1)]
        public void GetByIdTest(int index)
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var invitationCodeData = GetInvitationCodeList();
            var invitationCodeSet = new Mock<DbSet<InvitationCode>>().SetupData(invitationCodeData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<InvitationCode>()).Returns(invitationCodeSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            var allInvitationCode = unitOfWork.InvitationCodeRepository.Get();
            var entity = allInvitationCode.ElementAt(index);

            Assert.Equal(entity, unitOfWork.InvitationCodeRepository.GetById(entity.Id));
        }

        [Fact]
        public void InsertInvitationCodeTest()
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var invitationCodeData = GetInvitationCodeList();
            var invitationCodeSet = new Mock<DbSet<InvitationCode>>().SetupData(invitationCodeData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<InvitationCode>()).Returns(invitationCodeSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var invitationCode = new InvitationCode()
            {
                Code = "AA245GJ9",
                IsDeleted = false,
                ParentUser = new User()
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

            unitOfWork.InvitationCodeRepository.Insert(invitationCode);

            var result = unitOfWork.InvitationCodeRepository.GetAll();

            Assert.Equal(invitationCode, unitOfWork.InvitationCodeRepository.GetById(invitationCode.Id));
        }

        [Fact]
        public void InsertSingleInvitationCodeTest()
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var invitationCodeData = GetInvitationCodeList();
            var invitationCodeSet = new Mock<DbSet<InvitationCode>>().SetupData(invitationCodeData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<InvitationCode>()).Returns(invitationCodeSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var invitationCode = new InvitationCode()
            {
                Code = "AA245GJ9",
                IsDeleted = false,
                ParentUser = new User()
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

            unitOfWork.InvitationCodeRepository.Insert(invitationCode);

            var result = unitOfWork.InvitationCodeRepository.GetAll();

            Assert.True(result.isNotEmpty());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeleteInvitationCodeByIdTest(int index)
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var invitationCodeData = GetInvitationCodeList();
            var invitationCodeSet = new Mock<DbSet<InvitationCode>>().SetupData(invitationCodeData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<InvitationCode>()).Returns(invitationCodeSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var elementId = invitationCodeData.ElementAt(index).Id;

            unitOfWork.InvitationCodeRepository.Delete(elementId);
            unitOfWork.Save();

            var allinvitationCodes = unitOfWork.InvitationCodeRepository.GetAll();

            Assert.True(allinvitationCodes.First(u => u.Id == elementId).IsDeleted);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeleteInvitationCodeTest(int index)
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var invitationCodeData = GetInvitationCodeList();
            var invitationCodeSet = new Mock<DbSet<InvitationCode>>().SetupData(invitationCodeData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<InvitationCode>()).Returns(invitationCodeSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var element = invitationCodeData.ElementAt(index);

            unitOfWork.InvitationCodeRepository.Delete(element);
            unitOfWork.Save();

            var allInvitationCodes = unitOfWork.InvitationCodeRepository.GetAll();

            Assert.True(allInvitationCodes.First(p => p.Id == element.Id).IsDeleted);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void UpdateInvitationCodeTest(int index)
        {
            var userData = GetUserList();
            var userSet = new Mock<DbSet<User>>().SetupData(userData);
            var invitationCodeData = GetInvitationCodeList();
            var invitationCodeSet = new Mock<DbSet<InvitationCode>>().SetupData(invitationCodeData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<User>()).Returns(userSet.Object);
            context.Setup(ctx => ctx.Set<InvitationCode>()).Returns(invitationCodeSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var element = invitationCodeData.ElementAt(index);

            var originalName = element.Code;

            element.Code = "123456YF";

            unitOfWork.InvitationCodeRepository.Update(element);
            unitOfWork.Save();

            var allInvitationCodes = unitOfWork.InvitationCodeRepository.GetAll();

            Assert.NotEqual(allInvitationCodes.First(u => u.Id == element.Id).Code, originalName);
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

        private List<InvitationCode> GetInvitationCodeList()
        {
            var users = this.GetUserList();
            return new List<InvitationCode>()
            {
                new InvitationCode()
                {
                    Code = "AA245GJ1",
                    IsDeleted = users.ElementAt(0).IsDeleted,
                    ParentUser = users.ElementAt(0),
                    ParentUserId = users.ElementAt(0).Id,
                    Id = Guid.NewGuid()
                },
                new InvitationCode()
                {
                    Code = "AA245GJ2",
                    IsDeleted = users.ElementAt(1).IsDeleted,
                    ParentUser = users.ElementAt(1),
                    ParentUserId = users.ElementAt(1).Id,
                    Id = Guid.NewGuid()
                },
                new InvitationCode()
                {
                    Code = "AA245GJ3",
                    IsDeleted = users.ElementAt(2).IsDeleted,
                    ParentUser = users.ElementAt(2),
                    ParentUserId = users.ElementAt(2).Id,
                    Id = Guid.NewGuid()
                },
                new InvitationCode()
                {
                    Code = "AA245GJ4",
                    IsDeleted = users.ElementAt(3).IsDeleted,
                    ParentUser = users.ElementAt(3),
                    ParentUserId = users.ElementAt(3).Id,
                    Id = Guid.NewGuid()
                },
                 new InvitationCode()
                {
                    Code = "AA245GJ5",
                    IsDeleted = users.ElementAt(4).IsDeleted,
                    ParentUser = users.ElementAt(4),
                    ParentUserId = users.ElementAt(4).Id,
                    Id = Guid.NewGuid()
                },
            };
        }
    }
}
