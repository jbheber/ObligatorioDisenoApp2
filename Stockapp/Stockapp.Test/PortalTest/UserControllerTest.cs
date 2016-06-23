using Moq;
using Stockapp.Data;
using Stockapp.Data.Exceptions;
using Stockapp.Logic.API;
using Stockapp.Portal.Controllers;
using Stockapp.Portal.Models;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace Stockapp.Test.PortalTest
{
    public class UserControllerTest
    {
        [Fact]
        public void UpdateUsersReturnsNoContentTest()
        {
            var mockUserLogic = new Mock<IUserLogic>();

            mockUserLogic.Setup(x => x.UpdateUser(It.IsAny<User>())).Returns(true);
            var controller = new UserController(mockUserLogic.Object);

            var userId = 1;
            IHttpActionResult actionResult = controller.PutUser(userId, new User() { Id = userId });

            StatusCodeResult contentResult = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.NotNull(contentResult);
            Assert.Equal(contentResult.StatusCode, HttpStatusCode.NoContent);

        }

        [Fact]
        public void UpdateUsersReturnsNotFoundTest()
        {
            var mockUserLogic = new Mock<IUserLogic>();

            mockUserLogic.Setup(x => x.UpdateUser(It.IsAny<User>())).Returns(false);
            var controller = new UserController(mockUserLogic.Object);

            var userId = 1;
            IHttpActionResult actionResult = controller.PutUser(userId, new User() { Id = userId });

            var contentResult = Assert.IsType<NotFoundResult>(actionResult);
            Assert.NotNull(contentResult);
        }

        [Fact]
        public void RegisterUserReturnsCreatedAtRouteTest()
        {
            var mockUserLogic = new Mock<IUserLogic>();

            mockUserLogic.Setup(x => x.RegisterUser(It.IsAny<User>(), It.IsAny<InvitationCode>()))
                .Returns(true);
            var controller = new UserController(mockUserLogic.Object);

            var userId = 1;
            var newUser = new RegisterUserDTO()
            {
                User = new User()
                {
                    Name = "fartolaa",
                    Password = "Art.12345",
                    Email = "artolaa@outlook.com",
                    Id = userId
                },
                InvitationCode = new InvitationCode()
                {
                    Code = "AA245GJ1",
                }
            };
            IHttpActionResult actionResult = controller.PostUser(newUser);

            var contentResult = Assert.IsType<CreatedAtRouteNegotiatedContentResult<User>>(actionResult);
            Assert.NotNull(contentResult);

            Assert.Equal(contentResult.Content, newUser.User);

        }

        [Fact]
        public void RegisterUserReturnsBadRequestTest()
        {
            var mockUserLogic = new Mock<IUserLogic>();

            mockUserLogic.Setup(x => x.RegisterUser(It.IsAny<User>(), It.IsAny<InvitationCode>()))
                .Throws(new UserException("User exception"));

            var controller = new UserController(mockUserLogic.Object);

            IHttpActionResult actionResult = controller.PostUser(new RegisterUserDTO()
            {
                User = new User(),
                InvitationCode = new InvitationCode()
            });

            var contentResult = Assert.IsType<BadRequestErrorMessageResult>(actionResult);
            Assert.NotNull(contentResult);

            Assert.Equal(contentResult.Message, "User exception");
        }

        [Fact]
        public void DeleteUserReturnsNoContentTest()
        {
            var mockUserLogic = new Mock<IUserLogic>();

            mockUserLogic.Setup(x => x.DeleteUser(It.IsAny<long>())).Returns(true);
            var controller = new UserController(mockUserLogic.Object);

            var userId = 1;
            IHttpActionResult actionResult = controller.DeleteUser(userId);

            StatusCodeResult contentResult = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.NotNull(contentResult);
            Assert.Equal(contentResult.StatusCode, HttpStatusCode.NoContent);
        }

        [Fact]
        public void DeleteUserReturnsNotFoundTest()
        {
            var mockUserLogic = new Mock<IUserLogic>();

            mockUserLogic.Setup(x => x.DeleteUser(It.IsAny<long>())).Returns(false);
            var controller = new UserController(mockUserLogic.Object);

            var userId = 1;
            IHttpActionResult actionResult = controller.DeleteUser(userId);

            var contentResult = Assert.IsType<NotFoundResult>(actionResult);
            Assert.NotNull(contentResult);
        }

        [Fact]
        public void SignInReturnsUser()
        {
            var mockUserLogic = new Mock<IUserLogic>();

            var user = new User()
            {
                Name = "fartolaa",
                Password = "Art.12345",
                Email = "artolaa@outlook.com",
                Id = 1
            };

            mockUserLogic.Setup(x => x.LogIn(It.IsAny<User>())).Returns(user);
            var controller = new UserController(mockUserLogic.Object);

            IHttpActionResult actionResult = controller.SignIn(user.Name, user.Password);

            var contentResult = Assert.IsType<OkNegotiatedContentResult<User>>(actionResult);
            Assert.NotNull(contentResult);
            Assert.Equal(contentResult.Content, user);
        }

        [Fact]
        public void SignInReturnsUserException()
        {
            var mockUserLogic = new Mock<IUserLogic>();

            var user = new User()
            {
                Name = "fartolaa",
                Password = "Art.12345",
                Email = "artolaa@outlook.com",
                Id = 1
            };

            mockUserLogic.Setup(x => x.LogIn(It.IsAny<User>())).Throws(new UserException("User exception"));
            var controller = new UserController(mockUserLogic.Object);

            IHttpActionResult actionResult = controller.SignIn(user.Name, user.Password);

            var contentResult = Assert.IsType<BadRequestErrorMessageResult>(actionResult);
            Assert.NotNull(contentResult);

            Assert.Equal(contentResult.Message, "User exception");
        }

        public void SignInReturnsUserNotFound()
        {
            var mockUserLogic = new Mock<IUserLogic>();

            var user = new User()
            {
                Name = "fartolaa",
                Password = "Art.12345",
                Email = "artolaa@outlook.com",
                Id = 1
            };

            mockUserLogic.Setup(x => x.LogIn(It.IsAny<User>()));
            var controller = new UserController(mockUserLogic.Object);

            IHttpActionResult actionResult = controller.SignIn(user.Name, user.Password);

            var contentResult = Assert.IsType<NotFoundResult>(actionResult);
            Assert.NotNull(contentResult);
        }
    }
}
