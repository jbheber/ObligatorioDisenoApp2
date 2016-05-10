using Moq;
using Stockapp.Data;
using Stockapp.Data.Repository;
using Stockapp.Logic.API;
using Stockapp.Logic.Implementation;
using System;
using Xunit;

namespace Stockapp.Test.LogicTest
{
    public class PlayerLogicTest
    {
        [Fact]
        public void GetPlayerTest()
        {
            var playerId = Guid.NewGuid();
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.PlayerRepository.GetById(It.IsAny<Guid>())).Returns(new Player() { Id = playerId });

            IPlayerLogic playerLogic = new PlayerLogic(mockUnitOfWork.Object);

            var searchedPlayer = playerLogic.GetPlayer(playerId);
            Assert.NotNull(searchedPlayer);
            Assert.Equal(searchedPlayer.Id, playerId);
        }

        [Fact]
        public void GetPlayerNotFoundTest()
        {
            var playerId = Guid.NewGuid();
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.PlayerRepository.GetById(It.IsAny<Guid>()));

            IPlayerLogic playerLogic = new PlayerLogic(mockUnitOfWork.Object);

            var searchedPlayer = playerLogic.GetPlayer(playerId);
            Assert.Null(searchedPlayer);
            mockUnitOfWork.Verify(un => un.PlayerRepository.GetById(It.IsAny<Guid>()));
        }

        [Fact]
        public void DeletePlayerByIdTest()
        {
            var playerId = Guid.NewGuid();
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.PlayerRepository.Delete(It.IsAny<Guid>()));
            mockUnitOfWork.Setup(un => un.Save());

            IPlayerLogic playerLogic = new PlayerLogic(mockUnitOfWork.Object);

            var deletedPlayer = playerLogic.DeletePlayer(playerId);
            mockUnitOfWork.Verify(un => un.PlayerRepository.Delete(It.IsAny<Guid>()));
            Assert.True(deletedPlayer);
        }

        [Fact]
        public void DeletePlayerTest()
        {
            var playerId = Guid.NewGuid();
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.PlayerRepository.Delete(It.IsAny<Player>()));
            mockUnitOfWork.Setup(un => un.Save());

            IPlayerLogic playerLogic = new PlayerLogic(mockUnitOfWork.Object);

            var deletedPlayer = playerLogic.DeletePlayer(playerId);
            mockUnitOfWork.Verify(un => un.PlayerRepository.Delete(It.IsAny<Guid>()));
            mockUnitOfWork.Verify(un => un.Save());
            Assert.True(deletedPlayer);
        }

        [Fact]
        public void RegisterPlayerTest()
        {
            var playerId = Guid.NewGuid();
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
            var playerId = Guid.NewGuid();
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
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
