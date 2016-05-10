using Moq;
using Stockapp.Data;
using Stockapp.Data.Exceptions;
using Stockapp.Data.Repository;
using Stockapp.Logic.API;
using Stockapp.Logic.Implementation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace Stockapp.Test
{
    public class UserLogicTest
    {

        [Fact]
        public void UniqueUserEmailTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.UserRepository.Get(null, null, null));

            IUserLogic userLogic = new UserLogic(mockUnitOfWork.Object);
            var email = "a@hotmail.com";
            bool expected = true;

            //Act
            var result = userLogic.EmailIsUnique(email);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("a@hotmail.com")]
        public void EmptyMailTest(string email)
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.UserRepository.Get(null, null, null));

            IUserLogic userLogic = new UserLogic(mockUnitOfWork.Object);

            var expected = email == string.Empty ? true : false;

            var obtain = userLogic.MailIsEmpty(email);

            Assert.Equal(expected, obtain);
        }

        [Theory]
        [InlineData("1234")]
        [InlineData("123456")]
        [InlineData("12345678")]
        public void PasswordLenghtTest(string password)
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.UserRepository.Get(null, null, null));

            IUserLogic userLogic = new UserLogic(mockUnitOfWork.Object);

            bool expected = password.Length < 6 ? false : true;

            bool obtain = userLogic.ValidPasswordLenght(password);

            Assert.Equal(expected, obtain);
        }

        [Fact]
        public void AlphaNumericTest1()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.UserRepository.Get(null, null, null));

            IUserLogic userLogic = new UserLogic(mockUnitOfWork.Object);

            string word = "aaaaaa";
            bool expected = false;
            bool obtain = userLogic.IsAlphaNumeric(word);

            Assert.Equal(expected, obtain);
        }

        [Fact]
        public void AlphaNumericTest2()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.UserRepository.Get(null, null, null));

            IUserLogic userLogic = new UserLogic(mockUnitOfWork.Object);

            string word = "a123456";
            bool expected = true;
            bool obtain = userLogic.IsAlphaNumeric(word);

            Assert.Equal(expected, obtain);
        }

        [Fact]
        public void AlphaNumericTest3()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.UserRepository.Get(null, null, null));

            IUserLogic userLogic = new UserLogic(mockUnitOfWork.Object);

            string word = "af4567l";
            bool expected = true;
            bool obtain = userLogic.IsAlphaNumeric(word);

            Assert.Equal(expected, obtain);
        }

        [Theory]
        [InlineData("Carlos", "", "carlos123", "abc12345")]
        [InlineData("Carlos", "carlos@hotmail.com", "carlos", "abc12345")]
        [InlineData("Carlos", "carlos@hotmail.com", "carlos123", "12345")]
        [InlineData("", "", "", "")]
        public void RegisterValidationsTest1(string name, string email, string password, string ic)
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.InvitationCodeRepository.Insert(It.IsAny<InvitationCode>()));
            mockUnitOfWork.Setup(un => un.UserRepository.Get(null, null, null));


            IUserLogic userLogic = new UserLogic(mockUnitOfWork.Object);

            var invitationCode = new InvitationCode()
            {
                Code = ic
            };
            mockUnitOfWork.Object.InvitationCodeRepository.Insert(invitationCode);

            var user = new User()
            {
                Name = name,
                Email = email,
                Password = password
            };
            bool throwUserException = false;
            try
            {
                userLogic.ValidateUser(user, invitationCode);
                // If test gets to this assert then it failed
                throwUserException = false;
            }
            catch (UserExceptions ue)
            {
                //For debug purposes
                var exceptionMessage = ue.Message;
                // If test gets to this assert then its correct
                throwUserException = true;
            }
            catch (Exception ex)
            {
                //For debug purposes
                var exceptionMessage = ex.Message;
                throwUserException = false;
            }
            Assert.True(throwUserException);
        }

        [Theory]
        [InlineData("Carlos", "car@gmail.com", "carlos123", "abc12345")]
        [InlineData("Arto", "artoo@gmail.com", "artola123", "abc12345")]
        public void RegisterValidationsTest2(string name, string email, string password, string ic)
        {
            InvitationCode invitationCode = new InvitationCode();
            invitationCode.Code = ic;
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.InvitationCodeRepository.Get(It.IsAny<Expression<Func<InvitationCode, bool>>>(), null, ""))
                .Returns(new List<InvitationCode>() { invitationCode });
            mockUnitOfWork.Setup(un => un.UserRepository.Get(null, null, null));

            IUserLogic userLogic = new UserLogic(mockUnitOfWork.Object);

            User user = new User();
            user.Name = name;
            user.Email = email;
            user.Password = password;
            try
            {
                userLogic.ValidateUser(user, invitationCode);
                // If test gets to this assert then its correct
                Assert.True(true);
            }
            catch (UserExceptions ue)
            {
                //For debug purposes
                var exceptionMessage = ue.Message;
                // If test gets to this assert then it failed
                Assert.True(false);
            }
            catch (Exception ex)
            {
                //For debug purposes
                var exceptionMessage = ex.Message;
                Assert.True(false);
            }
        }

        [Theory]
        [InlineData("Carlos", "car@gmail.com", "carlos123", "abc12345")]
        [InlineData("Arto", "artoo@gmail.com", "artola123", "abc12345")]
        public void RegisterUserTest(string name, string email, string password, string ic)
        {
            InvitationCode invitationCode = new InvitationCode();
            invitationCode.Code = ic;
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.InvitationCodeRepository.Get(It.IsAny<Expression<Func<InvitationCode, bool>>>(), null, ""))
                .Returns(new List<InvitationCode>() { invitationCode });
            mockUnitOfWork.Setup(un => un.UserRepository.Insert(It.IsAny<User>()));
            mockUnitOfWork.Setup(un => un.Save());

            IUserLogic userLogic = new UserLogic(mockUnitOfWork.Object);

            User user = new User()
            {
                Name = name,
                Email = email,
                Password = password
            };

            try
            {
                var response = userLogic.RegisterUser(user, invitationCode);
                //Assert
                mockUnitOfWork.Verify(un => un.UserRepository.Insert(It.IsAny<User>()), Times.Exactly(1));
                mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));
                Assert.True(response);
            }
            catch (UserExceptions ue)
            {
                //For debug purposes
                var exceptionMessage = ue.Message;
                // If test gets to this assert then it failed
                Assert.True(false);
            }
            catch (Exception ex)
            {
                //For debug purposes
                var exceptionMessage = ex.Message;
                Assert.True(false);
            }
        }

        [Fact]
        public void IsInDbTestFalse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.UserRepository.Get(null, null, null));

            IUserLogic userLogic = new UserLogic(mockUnitOfWork.Object);

            InvitationCode invitationCode = new InvitationCode()
            {
                Code = "abc12345"
            };

            User user = new User()
            {
                Name = "Carlos",
                Email = "car@gmail.com",
                Password = "carlos123"
            };
            Assert.False(userLogic.IsInDb(user));
        }

        [Fact]
        public void IsInDbTest()
        {
            var user = new User()
            {
                Name = "Carlos",
                Email = "car@gmail.com",
                Password = "carlos123",
                Id = Guid.NewGuid()
            };
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.UserRepository.Get(null, null, ""));
            IUserLogic userLogic = new UserLogic(mockUnitOfWork.Object);
            userLogic.IsInDb(user);
            mockUnitOfWork.VerifyAll();
        }

        [Fact]
        public void LogInTest()
        {
            var user = new User()
            {
                Name = "Carlos",
                Email = "car@gmail.com",
                Password = "carlos123",
                Id = Guid.NewGuid()
            };
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.UserRepository.Get(null, null, ""));

            IUserLogic userLogic = new UserLogic(mockUnitOfWork.Object);

            User searched = userLogic.LogIn(user);
            // If test gets to this assert then its correct
            mockUnitOfWork.VerifyAll();
        }

        [Fact]
        public void GetAllUsersFromRepositoryTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();


            mockUnitOfWork.Setup(un => un.UserRepository.Get(null, null, ""));

            IUserLogic userService = new UserLogic(mockUnitOfWork.Object);

            //Act
            IEnumerable<User> returnedUsers = userService.GetAllUsers();

            //Assert
            mockUnitOfWork.VerifyAll();
        }

        [Fact]
        public void UpdatesExistingUser()
        {
            //Arrange 
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(un => un.UserRepository.GetById(It.IsAny<Guid>()))
                .Returns(() => new User() { });

            mockUnitOfWork.Setup(un => un.UserRepository.Update(It.IsAny<User>()));
            mockUnitOfWork.Setup(un => un.Save());

            IUserLogic userLogic = new UserLogic(mockUnitOfWork.Object);

            //act
            bool updated = userLogic.UpdateUser(new User() { });

            //Assert
            mockUnitOfWork.Verify(un => un.UserRepository.Update(It.IsAny<User>()), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));
            Assert.True(updated);

        }
    }
}
