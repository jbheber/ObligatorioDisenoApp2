using Moq;
using Stockapp.Data;
using Stockapp.Data.Repository;
using Stockapp.Logic.API;
using Stockapp.Logic.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Stockapp.Test
{
    public class AdminLogicTest
    {
        [Fact]
        public void CreateAdminTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.AdminRepository.Get(null, null, ""));
            mockUnitOfWork.Setup(un => un.AdminRepository.Insert(It.IsAny<Admin>()));
            mockUnitOfWork.Setup(un => un.Save());

            IAdminLogic adminLogic = new AdminLogic(mockUnitOfWork.Object);
            var result = adminLogic.CreateAdmin(new Admin());

            mockUnitOfWork.Verify(un => un.AdminRepository.Get(null, null, ""), Times.Once());
            mockUnitOfWork.Verify(un => un.AdminRepository.Insert(It.IsAny<Admin>()), Times.Once());
            mockUnitOfWork.Verify(un => un.Save(), Times.Once());
            Assert.True(result);
        }

        [Fact]
        public void UpdateAdminTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var admin = new Admin()
            {
                Id = Guid.NewGuid()
            };

            mockUnitOfWork.Setup(un => un.AdminRepository.Update(It.IsAny<Admin>()));
            mockUnitOfWork.Setup(un => un.Save());


            IAdminLogic adminLogic = new AdminLogic(mockUnitOfWork.Object);
            admin.Email = "aa@hotmail.com";
            var result = adminLogic.UpdateAdmin(admin);

            mockUnitOfWork.Verify(un => un.AdminRepository.Update(It.IsAny<Admin>()), Times.Once());
            mockUnitOfWork.Verify(un => un.Save(), Times.Once());
            Assert.True(result);
        }

        [Fact]
        public void GetAdminTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var admin = new Admin()
            {
                Id = Guid.NewGuid()
            };

            mockUnitOfWork.Setup(un => un.AdminRepository.GetById(It.IsAny<Guid>())).Returns(admin);

            IAdminLogic adminLogic = new AdminLogic(mockUnitOfWork.Object);
            var result = adminLogic.GetAdmin(admin.Id);

            mockUnitOfWork.Verify(un => un.AdminRepository.GetById(It.IsAny<Guid>()), Times.Once());
            Assert.Equal(result, admin);
        }

        [Fact]
        public void DeleteAdminByIdTest()
        {
            var adminId = Guid.NewGuid();
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.AdminRepository.Delete(It.IsAny<Guid>()));
            mockUnitOfWork.Setup(un => un.Save());

            IAdminLogic adminLogic = new AdminLogic(mockUnitOfWork.Object);

            var deletedAdmin = adminLogic.DeleteAdmin(adminId);
            mockUnitOfWork.Verify(un => un.AdminRepository.Delete(It.IsAny<Guid>()));
            Assert.True(deletedAdmin);
        }

        [Fact]
        public void DeleteAdminTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.AdminRepository.Delete(It.IsAny<Admin>()));
            mockUnitOfWork.Setup(un => un.Save());

            IAdminLogic adminLogic = new AdminLogic(mockUnitOfWork.Object);

            var deletedAdmin = adminLogic.DeleteAdmin(new Admin());
            mockUnitOfWork.Verify(un => un.AdminRepository.Delete(It.IsAny<Admin>()));
            mockUnitOfWork.Verify(un => un.Save());
            Assert.True(deletedAdmin);
        }
    }
}

