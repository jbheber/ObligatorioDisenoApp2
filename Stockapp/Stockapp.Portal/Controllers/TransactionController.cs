using Stockapp.Data;
using Stockapp.Data.Exceptions;
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
    public class TransactionController : ApiController
    {
        private readonly ITransactionLogic transactionLogic;

        public TransactionController(ITransactionLogic transactionLogic)
        {
            this.transactionLogic = transactionLogic;
        }

        [HttpGet]
        [Route("api/transaction/{from}/{to}/{stockId:long?}/{transactionType:alpha?}")]
        public IHttpActionResult Get(DateTimeOffset from, DateTimeOffset to, long stockId = 0, string transactionType = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<Transaction> transactions = transactionLogic.GetTransacions(from, to, stockId, transactionType);
            if (transactions == null)
            {
                return NotFound();
            }
            return Ok(transactions);
        }

        [HttpGet]
        [Route("api/transaction/getusertransactions/{portfolioId:long}/{from}/{to}/{stockId:long?}")]
        public IHttpActionResult GetUserTransactions(long portfolioId, DateTimeOffset from, DateTimeOffset to, long stockId = 0)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var transactions = transactionLogic.GetTransacions(from, to, stockId);
            transactions = transactions.Where(t => t.PortfolioId == portfolioId);
            if (transactions == null)
            {
                return NotFound();
            }
            return Ok(transactions);
        }

        // PUT: api/Stock/5
        /// <summary>
        /// Update Stock
        /// </summary>
        /// <param name="id">Stock.Id</param>
        /// <param name="user">Updated Stock</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/transaction/{id:long}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTransaction(long id, Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transaction.Id)
            {
                return BadRequest();
            }

            if (!transactionLogic.UpdateTransaction(transaction))
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
        [Route("api/transaction/")]
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult PostTransaction(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                transaction.TransactionDate = DateTimeOffset.Now;
                if (transactionLogic.RegisterTransaction(transaction))
                    return Ok();
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
                transactionLogic.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}