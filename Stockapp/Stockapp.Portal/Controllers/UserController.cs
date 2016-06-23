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

        [HttpGet]
        [Route("api/user/")]
        public IHttpActionResult Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<User> users = userLogic.GetAllUsers();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        // PUT: api/User/5
        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="id">User.Id</param>
        /// <param name="user">Updated user</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/user/{id:long}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(long id, User user)
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
        [HttpPost]
        [Route("api/user/")]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(RegisterUserDTO newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if(userLogic.RegisterUser(newUser.User, newUser.InvitationCode))
                    return Ok(newUser.User);
                return BadRequest();
            }
            catch (UserException ue)
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
        [HttpDelete]
        [Route("api/user/{id:long}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(long id)
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
        [HttpGet]
        [Route("api/user/signin/{email}/{password}/")]
        [ResponseType(typeof(User))]
        public IHttpActionResult SignIn(string email, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var localUser = new User()
            {
                Email = email,
                Password = password
            };
            try
            {
                var appUser = userLogic.LogIn(localUser);
                if (appUser == null)
                    return NotFound();
                return Ok(appUser);
            }
            catch (UserException ue)
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