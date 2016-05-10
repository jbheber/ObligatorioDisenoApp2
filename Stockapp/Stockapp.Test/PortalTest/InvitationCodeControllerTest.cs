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
    public class InvitationCodeControllerTest
    {
        [Fact]
        public void RegisterInvitationCodeReturnsCreatedAtRouteTest()
        {
            var invitationCode = new InvitationCode()
            {
                Code = "AA245GJ5",
                ParentUser = new User()
                {
                    Name = "fartolaa",
                    Password = "Art.12345",
                    Email = "artolaa@outlook.com",
                    IsAdmin = true,
                    Id = Guid.NewGuid()
                },
                Id = Guid.NewGuid()
            };
            var mockInvitationCodeLogic = new Mock<IInvitationCodeLogic>();

            mockInvitationCodeLogic.Setup(x => x.GenerateCode(It.IsAny<User>()))
                .Returns(invitationCode);
            var controller = new InvitationCodeController(mockInvitationCodeLogic.Object);

            IHttpActionResult actionResult = controller.PostInvitationCode(invitationCode.ParentUser);

            var contentResult = Assert.IsType<CreatedAtRouteNegotiatedContentResult<InvitationCode>>(actionResult);
            Assert.NotNull(contentResult);

            Assert.Equal(contentResult.Content, invitationCode);
        }

        [Fact]
        public void RegisterInvitationCodeReturnsBadRequestTest()
        {
            var mockInvitationCodeLogic = new Mock<IInvitationCodeLogic>();

            mockInvitationCodeLogic.Setup(x => x.GenerateCode(It.IsAny<User>()))
                .Throws(new InvitationCodeExceptions("Admin exception"));

            var controller = new InvitationCodeController(mockInvitationCodeLogic.Object);

            IHttpActionResult actionResult = controller.PostInvitationCode(new User());

            var contentResult = Assert.IsType<BadRequestErrorMessageResult>(actionResult);
            Assert.NotNull(contentResult);

            Assert.Equal(contentResult.Message, "Admin exception");
        }
    }
}




