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
            var adminId = 1;

            
            IHttpActionResult actionResult = controller.PutAdmin(new Admin() { Id = adminId });

            StatusCodeResult contentResult = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.NotNull(contentResult);
            Assert.Equal(contentResult.StatusCode, HttpStatusCode.NoContent);

        }

        [Fact]
        public void UpdateAdminsReturnsNotFoundTest()
        {
            var mockAdminLogic = new Mock<IAdminLogic>();
            var adminId = 1;

            mockAdminLogic.Setup(x => x.UpdateAdmin(It.IsAny<Admin>())).Returns(false);
            var controller = new AdminController(mockAdminLogic.Object);

            
            IHttpActionResult actionResult = controller.PutAdmin(new Admin() { Id = adminId });

            var contentResult = Assert.IsType<NotFoundResult>(actionResult);
            Assert.NotNull(contentResult);
        }

        [Fact]
        public void DeleteAdminReturnsNoContentTest()
        {
            var mockAdminLogic = new Mock<IAdminLogic>();
            var adminId = 1;
            mockAdminLogic.Setup(x => x.DeleteAdmin(It.IsAny<long>())).Returns(true);
            var controller = new AdminController(mockAdminLogic.Object);

            
            IHttpActionResult actionResult = controller.DeleteAdmin(adminId);

            StatusCodeResult contentResult = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.NotNull(contentResult);
            Assert.Equal(contentResult.StatusCode, HttpStatusCode.NoContent);
        }

        [Fact]
        public void DeleteAdminReturnsNotFoundTest()
        {
            var mockAdminLogic = new Mock<IAdminLogic>();

            mockAdminLogic.Setup(x => x.DeleteAdmin(It.IsAny<long>())).Returns(false);
            var controller = new AdminController(mockAdminLogic.Object);
            var adminId = 1;

            IHttpActionResult actionResult = controller.DeleteAdmin(adminId);

            var contentResult = Assert.IsType<NotFoundResult>(actionResult);
            Assert.NotNull(contentResult);
        }
    }
}

