using Moq;
using Stockapp.Data;
using Stockapp.Logic.API;
using Stockapp.Portal.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace Stockapp.Test.PortalTest
{
    public class PlayerControllerTest
    {
        [Fact]
        public void UpdatePlayerReturnsNoContentTest()
        {
            var mockPlayerLogic = new Mock<IPlayerLogic>();
            var mockPortfolioLogic = new Mock<IPortfolioLogic>();


            mockPlayerLogic.Setup(x => x.UpdatePlayer(It.IsAny<Player>())).Returns(true);
            var controller = new PlayerController(mockPlayerLogic.Object, mockPortfolioLogic.Object);

            var playerId = 1;
            IHttpActionResult actionResult = controller.PutPlayer(new Player() { Id = playerId });

            StatusCodeResult contentResult = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.NotNull(contentResult);
            Assert.Equal(contentResult.StatusCode, HttpStatusCode.NoContent);

        }

        [Fact]
        public void UpdatePlayerReturnsNotFoundTest()
        {
            var mockPlayerLogic = new Mock<IPlayerLogic>();
            var mockPortfolioLogic = new Mock<IPortfolioLogic>();

            mockPlayerLogic.Setup(x => x.UpdatePlayer(It.IsAny<Player>())).Returns(false);
            var controller = new PlayerController(mockPlayerLogic.Object, mockPortfolioLogic.Object);

            var playerId = 1;
            IHttpActionResult actionResult = controller.PutPlayer(new Player() { Id = playerId });

            var contentResult = Assert.IsType<NotFoundResult>(actionResult);
            Assert.NotNull(contentResult);
        }

        [Fact]
        public void RegisterPlayerReturnsCreatedAtRouteTest()
        {
            var mockPlayerLogic = new Mock<IPlayerLogic>();
            var mockPortfolioLogic = new Mock<IPortfolioLogic>();

            mockPlayerLogic.Setup(x => x.RegisterPlayer(It.IsAny<Player>()))
                .Returns(true);

            var controller = new PlayerController(mockPlayerLogic.Object, mockPortfolioLogic.Object);

            var player = new Player()
            {
                CI = 46640523,
                Email = "dm13@gmail.com",
                Name = "Damian",
                Surname = "Macaluso",
                User = new User()
            };
            IHttpActionResult actionResult = controller.PostPlayer(player);

            var contentResult = Assert.IsType<CreatedAtRouteNegotiatedContentResult<Player>>(actionResult);
            Assert.NotNull(contentResult);

            Assert.Equal(contentResult.Content, player);

        }

        [Fact]
        public void DeletePlayerReturnsNoContentTest()
        {
            var mockPlayerLogic = new Mock<IPlayerLogic>();
            var mockPortfolioLogic = new Mock<IPortfolioLogic>();

            mockPlayerLogic.Setup(x => x.DeletePlayer(It.IsAny<long>())).Returns(true);
            var controller = new PlayerController(mockPlayerLogic.Object, mockPortfolioLogic.Object);

            var userId = 1;
            IHttpActionResult actionResult = controller.DeletePlayer(userId);

            StatusCodeResult contentResult = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.NotNull(contentResult);
            Assert.Equal(contentResult.StatusCode, HttpStatusCode.NoContent);
        }

        [Fact]
        public void DeletePlayerReturnsNotFoundTest()
        {
            var mockPlayerLogic = new Mock<IPlayerLogic>();
            var mockPortfolioLogic = new Mock<IPortfolioLogic>();

            mockPlayerLogic.Setup(x => x.DeletePlayer(It.IsAny<long>())).Returns(false);
            var controller = new PlayerController(mockPlayerLogic.Object, mockPortfolioLogic.Object);

            var playerId = 1;
            IHttpActionResult actionResult = controller.DeletePlayer(playerId);

            var contentResult = Assert.IsType<NotFoundResult>(actionResult);
            Assert.NotNull(contentResult);
        }

    }
}
