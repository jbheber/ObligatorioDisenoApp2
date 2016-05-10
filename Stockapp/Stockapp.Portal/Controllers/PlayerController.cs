using Stockapp.Data;
using Stockapp.Data.Exceptions;
using Stockapp.Logic.API;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Stockapp.Portal.Controllers
{
    public class PlayerController : ApiController
    {
        private readonly IPlayerLogic playerLogic;

        public PlayerController(IPlayerLogic playerLogic)
        {
            this.playerLogic = playerLogic;
        }

        // PUT: api/Player/5
        /// <summary>
        /// Update Player
        /// </summary>
        /// <param name="id">Player.Id</param>
        /// <param name="player">Updated player</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlayer(Guid id, Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != player.Id)
            {
                return BadRequest();
            }

            if (!playerLogic.UpdatePlayer(player))
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Player
        /// <summary>
        /// Register a new Player
        /// </summary>
        /// <param name="userId">Current user Id</param>
        /// <param name="newPlayer">Player to register</param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        public IHttpActionResult PostPlayer(Guid userId, Player newPlayer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (playerLogic.RegisterPlayer(newPlayer))
                    return CreatedAtRoute("DefaultApi", new { id = newPlayer.Id }, newPlayer);
                return BadRequest();
            }
            catch (PlayerExceptions pe)
            {
                return BadRequest(pe.Message);
            }

        }

        // DELETE: api/Player/5
        /// <summary>
        /// Delete player
        /// </summary>
        /// <param name="id">Player.Id</param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        public IHttpActionResult DeletePlayer(Guid id)
        {
            if (playerLogic.DeletePlayer(id))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return NotFound();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                playerLogic.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
