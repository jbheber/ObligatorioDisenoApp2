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

        // PUT: api/Admin/5
        /// <summary>
        /// Update Admin
        /// </summary>
        /// <param name="id">Admin.Id</param>
        /// <param name="user">Updated admin</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdmin(Guid id, Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != admin.Id)
            {
                return BadRequest();
            }

            if (!adminLogic.UpdateAdmin(admin))
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Admin
        /// <summary>
        /// Register a new Admin
        /// </summary>
        /// <param name="user">Admin created client-side</param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        public IHttpActionResult PostAdmin(Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (adminLogic.CreateAdmin(admin))
                    return CreatedAtRoute("DefaultApi", new { id = admin.Id }, admin);
                return BadRequest();
            }
            catch (UserExceptions ue)
            {
                return BadRequest(ue.Message);
            }

        }

        // DELETE: api/Admin/5
        /// <summary>
        /// Delete admin
        /// </summary>
        /// <param name="id">Admin.Id</param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteAdmin(Guid id)
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