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
    public class AdminControllerTest
    {
        [Fact]
        public void UpdateAdminsReturnsNoContentTest()
        {
            var mockAdminLogic = new Mock<IAdminLogic>();

            mockAdminLogic.Setup(x => x.UpdateAdmin(It.IsAny<Admin>())).Returns(true);
            var controller = new AdminController(mockAdminLogic.Object);

            var adminId = Guid.NewGuid();
            IHttpActionResult actionResult = controller.PutAdmin(adminId, new Admin() { Id = adminId });

            StatusCodeResult contentResult = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.NotNull(contentResult);
            Assert.Equal(contentResult.StatusCode, HttpStatusCode.NoContent);

        }

        [Fact]
        public void UpdateAdminsReturnsNotFoundTest()
        {
            var mockAdminLogic = new Mock<IAdminLogic>();

            mockAdminLogic.Setup(x => x.UpdateAdmin(It.IsAny<Admin>())).Returns(false);
            var controller = new AdminController(mockAdminLogic.Object);

            var adminId = Guid.NewGuid();
            IHttpActionResult actionResult = controller.PutAdmin(adminId, new Admin() { Id = adminId });

            var contentResult = Assert.IsType<NotFoundResult>(actionResult);
            Assert.NotNull(contentResult);
        }

        [Fact]
        public void RegisterAdminReturnsCreatedAtRouteTest()
        {
            var mockAdminLogic = new Mock<IAdminLogic>();

            mockAdminLogic.Setup(x => x.CreateAdmin(It.IsAny<Admin>()))
                .Returns(true);
            var controller = new AdminController(mockAdminLogic.Object);

            var adminId = Guid.NewGuid();
            var admin = new Admin()
            {
                Name = "fartolaa",
                CI = 12345678,
                Surname = "artola",
                Email = "artolaa@outlook.com",
                User = new User()
                {
                    Name = "fartolaa",
                    Password = "Art.12345",
                    Email = "artolaa@outlook.com",
                    Id = Guid.NewGuid()
                },
                Id = adminId
            };
            IHttpActionResult actionResult = controller.PostAdmin(admin);

            var contentResult = Assert.IsType<CreatedAtRouteNegotiatedContentResult<Admin>>(actionResult);
            Assert.NotNull(contentResult);

            Assert.Equal(contentResult.Content, admin);
        }

        [Fact]
        public void RegisterAdminReturnsBadRequestTest()
        {
            var mockAdminLogic = new Mock<IAdminLogic>();

            mockAdminLogic.Setup(x => x.CreateAdmin(It.IsAny<Admin>()))
                .Throws(new UserExceptions("Admin exception"));

            var controller = new AdminController(mockAdminLogic.Object);

            IHttpActionResult actionResult = controller.PostAdmin(new Admin());

            var contentResult = Assert.IsType<BadRequestErrorMessageResult>(actionResult);
            Assert.NotNull(contentResult);

            Assert.Equal(contentResult.Message, "Admin exception");
        }

        [Fact]
        public void DeleteAdminReturnsNoContentTest()
        {
            var mockAdminLogic = new Mock<IAdminLogic>();

            mockAdminLogic.Setup(x => x.DeleteAdmin(It.IsAny<Guid>())).Returns(true);
            var controller = new AdminController(mockAdminLogic.Object);

            var adminId = Guid.NewGuid();
            IHttpActionResult actionResult = controller.DeleteAdmin(adminId);

            StatusCodeResult contentResult = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.NotNull(contentResult);
            Assert.Equal(contentResult.StatusCode, HttpStatusCode.NoContent);
        }

        [Fact]
        public void DeleteAdminReturnsNotFoundTest()
        {
            var mockAdminLogic = new Mock<IAdminLogic>();

            mockAdminLogic.Setup(x => x.DeleteAdmin(It.IsAny<Guid>())).Returns(false);
            var controller = new AdminController(mockAdminLogic.Object);

            var adminId = Guid.NewGuid();
            IHttpActionResult actionResult = controller.DeleteAdmin(adminId);

            var contentResult = Assert.IsType<NotFoundResult>(actionResult);
            Assert.NotNull(contentResult);
        }
    }
}

