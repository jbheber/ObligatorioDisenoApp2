using Moq;
using Stockapp.Data;
using Stockapp.Data.Exceptions;
using Stockapp.Data.Repository;
using Stockapp.Logic;
using Stockapp.Logic.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            string word = "a@f4567l";
            bool expected = true;
            bool obtain = userLogic.IsAlphaNumeric(word);

            Assert.Equal(expected, obtain);
        }

        [Theory]
        [InlineData("1234")]
        [InlineData("123456")]
        [InlineData("12345678")]
        public void InvitationCodeLenghtTest(string invitationCode)
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.UserRepository.Get(null, null, null));

            IUserLogic userLogic = new UserLogic(mockUnitOfWork.Object);

            bool expected = invitationCode.Length != 8 ? false : true;

            bool obtain = userLogic.ValidInvitationCodeLenght(invitationCode);

            Assert.Equal(expected, obtain);
        }

        [Theory]
        [InlineData("Carlos", "carlos@hotmail.com", "carlos123", "abc12345")]
        [InlineData("Carlos", "", "carlos123", "abc12345")]
        [InlineData("Carlos", "carlos@hotmail.com", "carlos", "abc12345")]
        [InlineData("Carlos", "carlos@hotmail.com", "carlos123", "12345")]
        [InlineData("", "", "", "")]
        public void RegisterValidationsTest(string name, string email, string password, string ic)
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.UserRepository.Get(null, null, null));

            IUserLogic userLogic = new UserLogic(mockUnitOfWork.Object);

            InvitationCode invitationCode = new InvitationCode();
            invitationCode.Code = ic;
            User user = new User();
            user.Name = name;
            user.Email = email;
            user.Password = password;
            userLogic.ValidateUser(user, invitationCode);

            Assert.Throws<UserExceptions>(
            delegate {
                userLogic.ValidateUser(user, invitationCode);
            });
        }
    }
}
