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
    public class AdminController : ApiController
    {
        private readonly IAdminLogic adminLogic;

        public AdminController(IAdminLogic adminLogic)
        {
            this.adminLogic = adminLogic;
        }

        [HttpGet]
        [Route("api/admin/{userId:long}")]
        public IHttpActionResult Get(long userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Admin admin = adminLogic.GetUserAdmin(userId);
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }

        // PUT: api/Admin/5
        /// <summary>
        /// Update Admin
        /// </summary>
        /// <param name="id">Admin.Id</param>
        /// <param name="user">Updated admin</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/admin/")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdmin(Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!adminLogic.UpdateAdmin(admin))
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Admin/5
        /// <summary>
        /// Delete admin
        /// </summary>
        /// <param name="id">Admin.Id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/admin/{id:long}")]
        [ResponseType(typeof(Admin))]
        public IHttpActionResult DeleteAdmin(long id)
        {
            if (adminLogic.DeleteAdmin(id))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return NotFound();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                adminLogic.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}