using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BigBank.API;
using BigBank.API.Models;

namespace BigBank.API.Controllers
{
    public class transactionsController : ApiController
    {
        private BigBankEntities db = new BigBankEntities();

        // GET: api/transactions
        public IQueryable<transactionModel> Gettransactions()
        {
            return db.transactions.Select(c => new transactionModel
            {
                TransactionId = c.TransactionId,
                AccountId = c.AccountId,
                TransactionDate = c.TransactionDate,
                Amount = c.Amount,
            });

        }

        // GET: api/transactions/5
        [ResponseType(typeof(transaction))]
        public IHttpActionResult Gettransaction(int id)
        {
            transaction dbTransaction = db.transactions.Find(id);
            if (dbTransaction == null)
            {
                return NotFound();
            }
            transactionModel modelTransaction = new transactionModel
            {
                TransactionId = dbTransaction.TransactionId,
                AccountId = dbTransaction.AccountId,
                TransactionDate = dbTransaction.TransactionDate,
                Amount = dbTransaction.Amount,
            };

            return Ok(dbTransaction);
        }

        // PUT: api/transactions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttransaction(int id, transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transaction.TransactionId)
            {
                return BadRequest();
            }

            db.Entry(transaction).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!transactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/transactions
        [ResponseType(typeof(transaction))]
        public IHttpActionResult Posttransaction(transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.transactions.Add(transaction);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = transaction.TransactionId }, transaction);
        }

        // DELETE: api/transactions/5
        [ResponseType(typeof(transaction))]
        public IHttpActionResult Deletetransaction(int id)
        {
            transaction transaction = db.transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }

            db.transactions.Remove(transaction);
            db.SaveChanges();

            return Ok(transaction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool transactionExists(int id)
        {
            return db.transactions.Count(e => e.TransactionId == id) > 0;
        }
    }
}