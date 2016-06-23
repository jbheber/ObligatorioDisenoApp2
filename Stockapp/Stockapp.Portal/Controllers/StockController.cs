using Stockapp.Data;
using Stockapp.Data.Exceptions;
using Stockapp.Data.Extensions;
using Stockapp.Logic.API;
using Stockapp.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        [Route("api/stock/{stockId:long}")]
        [ResponseType(typeof(Stock))]
        public IHttpActionResult Get(long stockId)
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

        [HttpGet]
        [Route("api/stock/getfilterstocks/{name?}/{description?}")]
        [ResponseType(typeof(Stock))]
        public IHttpActionResult GetFilterStocks(string name = "", string description = "")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stocks = stockLogic.GetStocks(name, description);
            if (stocks == null)
            {
                return NotFound();
            }
            return Ok(stocks);
        }

        [HttpGet]
        [Route("api/stock/")]
        public IHttpActionResult GetAll()
        {
            var stocks = stockLogic.GetAllStocks();
            if (stocks == null)
            {
                return NotFound();
            }
            return Ok(stocks);
        }

        // PUT: api/Stock/5
        [HttpPut]
        [Route("api/stock/")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStock(UpdateStockDTO updatedStock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (updatedStock.DateOfChange > DateTime.Now)
            {
                return BadRequest("la fecha de modificacion no es valida");
            }
            var stock = stockLogic.GetStock(updatedStock.Stock.Id);
            if (stock.StockHistory.SafeCount() == 0 || 
                stock.StockHistory.OrderByDescending(x => x.DateOfChange).FirstOrDefault().DateOfChange.Date <= updatedStock.DateOfChange.Date)
            {
                stock.UnityValue = updatedStock.Stock.UnityValue;
                stock.StockHistory.Add(new StockHistory()
                {
                    DateOfChange = updatedStock.DateOfChange,
                    RecordedValue = stock.UnityValue
                });
            }
            else
            {
                stock.StockHistory.Add(new StockHistory()
                {
                    DateOfChange = updatedStock.DateOfChange,
                    RecordedValue = updatedStock.Stock.UnityValue
                });
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
        [HttpPost]
        [Route("api/stock/")]
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
            catch (UserException ue)
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