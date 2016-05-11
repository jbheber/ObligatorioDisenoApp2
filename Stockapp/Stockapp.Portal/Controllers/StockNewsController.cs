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
    public class StockNewsController : ApiController
    {
        private readonly IStockNewsLogic stockNewsLogic;

        public StockNewsController(IStockNewsLogic stockNewsLogic)
        {
            this.stockNewsLogic = stockNewsLogic;
        }

        public IHttpActionResult Get(Guid stockId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            StockNews stockNews = stockNewsLogic.GetStockNews(stockId);
            if (stockNews == null)
            {
                return NotFound();
            }
            return Ok(stockNews);
        }

        // POST: api/StockNews
        /// <summary>
        /// Register a new StockNews
        /// </summary>
        /// <param name="user">StockNews created client-side</param>
        /// <returns></returns>
        [ResponseType(typeof(StockNews))]
        public IHttpActionResult PostStockNews(StockNews stockNews)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (stockNewsLogic.RegisterStockNews(stockNews))
                    return CreatedAtRoute("DefaultApi", new { id = stockNews.Id }, stockNews);
                return BadRequest();
            }
            catch (UserExceptions ue)
            {
                return BadRequest(ue.Message);
            }
        }

        // DELETE: api/StockNews/5
        /// <summary>
        /// Delete StockNews
        /// </summary>
        /// <param name="id">StockNews.Id</param>
        /// <returns></returns>
        [ResponseType(typeof(StockNews))]
        public IHttpActionResult DeleteStockNews(StockNews stockNews)
        {
            if (stockNewsLogic.DeleteStockNews(stockNews))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return NotFound();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                stockNewsLogic.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}