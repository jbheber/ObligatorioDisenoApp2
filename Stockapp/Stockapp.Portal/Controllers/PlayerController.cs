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
        private readonly IPortfolioLogic portfolioLogic;


        public PlayerController(IPlayerLogic playerLogic, IPortfolioLogic portfolioLogic)
        {
            this.playerLogic = playerLogic;
            this.portfolioLogic = portfolioLogic;
        }

        [HttpGet]
        [Route("api/player/{userId:long}")]
        [ResponseType(typeof(Player))]
        public IHttpActionResult Get(long userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var player = playerLogic.GetPlayer(userId);
            player.Portfolio = portfolioLogic.FetchPlayerPortfolio(player);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        // PUT: api/Player/5
        /// <summary>
        /// Update Player
        /// </summary>
        /// <param name="id">Player.Id</param>
        /// <param name="player">Updated player</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/player/")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlayer(Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
        [HttpPost]
        [Route("api/player/")]
        [ResponseType(typeof(Player))]
        public IHttpActionResult PostPlayer(Player newPlayer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (playerLogic.RegisterPlayer(newPlayer))
                    return Ok(newPlayer);
                return BadRequest();
            }
            catch (PlayerException pe)
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
        [HttpDelete]
        [Route("api/player/{id:long}")]
        [ResponseType(typeof(Player))]
        public IHttpActionResult DeletePlayer(long id)
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
