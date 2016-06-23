using Stockapp.Data;
using Stockapp.Data.Exceptions;
using Stockapp.Logic.API;
using Stockapp.Portal.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Stockapp.Portal.Controllers
{
    public class InvitationCodeController : ApiController
    {
        private readonly IInvitationCodeLogic invitationCodeLogic;

        public InvitationCodeController(IInvitationCodeLogic invitationCodeLogic)
        {
            this.invitationCodeLogic = invitationCodeLogic;
        }

        // POST: api/InvitationCode
        /// <summary>
        /// Register a new InvitationCode
        /// </summary>
        /// <param name="user">InvitationCode created client-side</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/invitationcode/")]
        [ResponseType(typeof(InvitationCode))]
        public IHttpActionResult PostInvitationCode(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newCode = invitationCodeLogic.GenerateCode(user);
                if (newCode != null) {
                    return Ok(newCode);
                }
                return BadRequest();
            }
            catch (InvitationCodeException ie)
            {
                return BadRequest(ie.Message);
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                invitationCodeLogic.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}