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
    public class UserController : ApiController
    {
        private readonly IUserLogic userLogic;

        public UserController(IUserLogic userLogic)
        {
            this.userLogic = userLogic;
        }

        // PUT: api/User/5
        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="id">User.Id</param>
        /// <param name="user">Updated user</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(Guid id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            if (!userLogic.UpdateUser(user))
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/User
        /// <summary>
        /// Register a new User
        /// </summary>
        /// <param name="user">User created client-side</param>
        /// <param name="invitationCode">Invitation Code recieved</param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user, InvitationCode invitationCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if(userLogic.RegisterUser(user, invitationCode))
                    return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
                return BadRequest();
            }
            catch (UserExceptions ue)
            {
                return BadRequest(ue.Message);
            }

        }

        // DELETE: api/User/5
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">User.Id</param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(Guid id)
        {
            if (userLogic.DeleteUser(id))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return NotFound();
        }

        // POST: api/User
        /// <summary>
        /// Sign In with user name and password
        /// </summary>
        /// <param name="user">User name and password</param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        public IHttpActionResult SignIn(UserSignInDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var localUser = new User()
            {
                Name = user.UserName,
                Password = user.Password
            };
            try
            {
                var appUser = userLogic.LogIn(localUser);
                if (appUser == null)
                    return NotFound();
                return Ok(appUser);
            }
            catch (UserExceptions ue)
            {
                return BadRequest(ue.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                userLogic.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}