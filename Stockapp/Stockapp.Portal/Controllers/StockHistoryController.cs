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
    public class StockHistoryController : ApiController
    {
        private readonly IStockHistoryLogic stockHistoryLogic;

        public StockHistoryController(IStockHistoryLogic stockHistoryLogic)
        {
            this.stockHistoryLogic = stockHistoryLogic;
        }

        public IHttpActionResult Get(Stock stock, int from = 0, int to = 20)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<StockHistory> stockHistories = stockHistoryLogic.FetchStockHistories(stock, from, to);
            if (stockHistories == null)
            {
                return NotFound();
            }
            return Ok(stockHistories);
        }

        // PUT: api/StockHistory/5
        /// <summary>
        /// Update StockHistory
        /// </summary>
        /// <param name="id">StockHistory.Id</param>
        /// <param name="player">Updated StockHistory</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStockHistory(Guid id, StockHistory stockHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stockHistory.Id)
            {
                return BadRequest();
            }

            if (!stockHistoryLogic.UpdateStockHistory(stockHistory))
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                stockHistoryLogic.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}