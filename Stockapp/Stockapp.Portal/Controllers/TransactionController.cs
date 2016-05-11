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
    public class TransactionController : ApiController
    {
        private readonly ITransactionLogic transactionLogic;

        public TransactionController(ITransactionLogic transactionLogic)
        {
            this.transactionLogic = transactionLogic;
        }

        public IHttpActionResult Get(DateTimeOffset from, DateTimeOffset to, Stock stock = null, string transactionType = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<Transaction> transactions = transactionLogic.GetTransacions(from, to, stock, transactionType);
            if (transactions == null)
            {
                return NotFound();
            }
            return Ok(transactions);
        }

        // PUT: api/Transaction/5
        /// <summary>
        /// Update Transaction
        /// </summary>
        /// <param name="id">Transaction.Id</param>
        /// <param name="user">Updated Transaction</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTransaction(Guid id, Transaction transaction)
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

        // POST: api/Transaction
        /// <summary>
        /// Register a new Transaction
        /// </summary>
        /// <param name="user">Transaction created client-side</param>
        /// <returns></returns>
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult PostTransaction(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (transactionLogic.RegisterTransaction(transaction))
                    return CreatedAtRoute("DefaultApi", new { id = transaction.Id }, transaction);
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
                transactionLogic.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}