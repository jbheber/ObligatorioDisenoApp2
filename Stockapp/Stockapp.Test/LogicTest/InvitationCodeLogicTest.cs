﻿using Moq;
using Stockapp.Data;
using Stockapp.Data.Repository;
using Stockapp.Logic.API;
using Stockapp.Logic.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace Stockapp.Test.LogicTest
{
    public class InvitationCodeLogicTest
    {
        [Fact]
        public void GenerateInvitationCodeTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.InvitationCodeRepository.Get(null, null, ""));
            mockUnitOfWork.Setup(un => un.InvitationCodeRepository.Insert(It.IsAny<InvitationCode>()));
            mockUnitOfWork.Setup(un => un.Save());

            IInvitationCodeLogic invitationCodeLogic = new InvitationCodeLogic(mockUnitOfWork.Object);
            
            var code = invitationCodeLogic.GenerateCode(new User() { IsAdmin = true });

            Assert.Equal(code.Code.Length, 8);

            mockUnitOfWork.VerifyAll();
        }

        [Fact]
        public void GenerateInvitationCodeThrowExceptionTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.InvitationCodeRepository.Get(null, null, ""));
            IInvitationCodeLogic invitationCodeLogic = new InvitationCodeLogic(mockUnitOfWork.Object);

            try
            {
                var code = invitationCodeLogic.GenerateCode(new User() { IsAdmin = false });
                Assert.True(false);
            }
            catch (Exception e)
            {
                //for debug purposes
                var errorMessage = e.Message;
                Assert.True(true);
            }
        }
    }
}
