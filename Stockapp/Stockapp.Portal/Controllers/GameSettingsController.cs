using Stockapp.Data.Entities;
using Stockapp.Logic.API;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Stockapp.Portal.Controllers
{
    public class GameSettingsController : ApiController
    {
        private readonly IGameSettingsLogic gameSettingsLogic;

        public GameSettingsController(IGameSettingsLogic gameSettingsLogic)
        {
            this.gameSettingsLogic = gameSettingsLogic;
        }

        // PUT: api/GameSettings/5
        /// <summary>
        /// Update Game Settings
        /// </summary>
        /// <param name="id">User.Id</param>
        /// <param name="settings">GameSettings admin</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGameSettings(Guid id, GameSettings settings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!gameSettingsLogic.UpdateOrCreateGameSettings(settings))
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // Get: api/GameSettings/
        /// <summary>
        /// Update Game Settings
        /// </summary>
        /// <param name="id">User.Id</param>
        /// <param name="user">Updated admin</param>
        /// <returns></returns>
        [ResponseType(typeof(GameSettings))]
        public IHttpActionResult GetGameSettings()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var settings = gameSettingsLogic.GetOrCreateGameSettings();
            if (settings == null)
            {
                return NotFound();
            }

            return Ok(settings);
        }
    }
}
