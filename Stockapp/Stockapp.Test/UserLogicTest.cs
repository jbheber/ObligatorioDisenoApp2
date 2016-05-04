using Moq;
using Stockapp.Data;
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
            var result = userLogic.UniqueUserEmail(email);

            Assert.Equal(expected, result);
        }
    }
}
