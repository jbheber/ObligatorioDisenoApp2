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
    public class StockController : ApiController
    {
        private readonly IStockLogic stockLogic;

        public StockController(IStockLogic stockLogic)
        {
            this.stockLogic = stockLogic;
        }

        public IHttpActionResult Get(Guid stockId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Stock stock = stockLogic.GetStock(stockId);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }

        // PUT: api/Stock/5
        /// <summary>
        /// Update Stock
        /// </summary>
        /// <param name="id">Stock.Id</param>
        /// <param name="user">Updated Stock</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStock(Guid id, Stock stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stock.Id)
            {
                return BadRequest();
            }

            if (!stockLogic.UpdateStock(stock))
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Stock
        /// <summary>
        /// Register a new Stock
        /// </summary>
        /// <param name="user">Stock created client-side</param>
        /// <returns></returns>
        [ResponseType(typeof(Stock))]
        public IHttpActionResult PostStock(Stock stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (stockLogic.CreateStock(stock))
                    return CreatedAtRoute("DefaultApi", new { id = stock.Id }, stock);
                return BadRequest();
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
                stockLogic.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}