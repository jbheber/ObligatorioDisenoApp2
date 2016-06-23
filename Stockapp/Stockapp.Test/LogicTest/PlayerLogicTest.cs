using Moq;
using Stockapp.Data;
using Stockapp.Data.Repository;
using Stockapp.Logic.API;
using Stockapp.Logic.Implementation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace Stockapp.Test.LogicTest
{
    public class PlayerLogicTest
    {
        [Fact]
        public void GetPlayerTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var userId = 1;
            mockUnitOfWork.Setup(un => un.PlayerRepository.Get(It.IsAny<Expression<Func<Player, bool>>>(), null, It.IsAny<string>())).Returns(new List<Player>(){ new Player() { UserId = userId }});

            IPlayerLogic playerLogic = new PlayerLogic(mockUnitOfWork.Object);

            var searchedPlayer = playerLogic.GetPlayer(userId);
            Assert.NotNull(searchedPlayer);
            Assert.Equal(searchedPlayer.UserId, userId);
        }

        [Fact]
        public void GetPlayerNotFoundTest()
        {
            var userId = 1;
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.PlayerRepository.Get(It.IsAny<Expression<Func<Player, bool>>>(),null,It.IsAny<string>()));

            IPlayerLogic playerLogic = new PlayerLogic(mockUnitOfWork.Object);

            var searchedPlayer = playerLogic.GetPlayer(userId);
            Assert.Null(searchedPlayer);
            mockUnitOfWork.Verify(un => un.PlayerRepository.Get(It.IsAny<Expression<Func<Player, bool>>>(), null, It.IsAny<string>()));
        }

        [Fact]
        public void DeletePlayerByIdTest()
        {
            var playerId = 1;
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.PlayerRepository.Delete(It.IsAny<long>()));
            mockUnitOfWork.Setup(un => un.Save());

            IPlayerLogic playerLogic = new PlayerLogic(mockUnitOfWork.Object);

            var deletedPlayer = playerLogic.DeletePlayer(playerId);
            mockUnitOfWork.Verify(un => un.PlayerRepository.Delete(It.IsAny<long>()));
            Assert.True(deletedPlayer);
        }

        [Fact]
        public void DeletePlayerTest()
        {
            var playerId = 1;
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.PlayerRepository.Delete(It.IsAny<Player>()));
            mockUnitOfWork.Setup(un => un.Save());

            IPlayerLogic playerLogic = new PlayerLogic(mockUnitOfWork.Object);

            var deletedPlayer = playerLogic.DeletePlayer(playerId);
            mockUnitOfWork.Verify(un => un.PlayerRepository.Delete(It.IsAny<long>()));
            mockUnitOfWork.Verify(un => un.Save());
            Assert.True(deletedPlayer);
        }

        [Fact]
        public void RegisterPlayerTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.PlayerRepository.Insert(It.IsAny<Player>()));
            mockUnitOfWork.Setup(un => un.Save());

            IPlayerLogic playerLogic = new PlayerLogic(mockUnitOfWork.Object);

            var createdPlayer = playerLogic.RegisterPlayer(new Player());
            mockUnitOfWork.Verify(un => un.PlayerRepository.Insert(It.IsAny<Player>()));
            Assert.True(createdPlayer);
        }

        [Fact]
        public void UpdatePlayerTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.UserRepository.Update(It.IsAny<User>()));
            mockUnitOfWork.Setup(un => un.PlayerRepository.Update(It.IsAny<Player>()));
            mockUnitOfWork.Setup(un => un.Save());

            IPlayerLogic playerLogic = new PlayerLogic(mockUnitOfWork.Object);

            var createdPlayer = playerLogic.UpdatePlayer(new Player());
            mockUnitOfWork.Verify(un => un.PlayerRepository.Update(It.IsAny<Player>()));
            mockUnitOfWork.Verify(un => un.Save());

            Assert.True(createdPlayer);
        }
    }
}
