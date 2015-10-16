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
    public class accountsController : ApiController
    {
        private BigBankEntities db = new BigBankEntities();

        // GET: api/accounts
        public IQueryable<accountModel> Getaccounts()
        {
            return db.accounts.Select(c => new accountModel
            {
                CustomerId = c.CustomerId,
                AccountId = c.AccountId,
                CreatedDate = c.CreatedDate,
                AccountNumber = c.AccountNumber,
                Balance = c.Balance,

            });
        }

        // GET: api/accounts/5
        [ResponseType(typeof(account))]
        public IHttpActionResult Getaccount(int id)
        {
            account dbAccount = db.accounts.Find(id);
            if (dbAccount == null)
            {
                return NotFound();
            }
            accountModel modelAccount = new accountModel
            {
                CustomerId = dbAccount.CustomerId,
                AccountId = dbAccount.AccountId,
                AccountNumber = dbAccount.AccountNumber,
                Balance = dbAccount.Balance,
                CreatedDate = dbAccount.CreatedDate,
            };

            
            return Ok(modelAccount);
        }

        // PUT: api/accounts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putaccount(int id, account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != account.AccountId)
            {
                return BadRequest();
            }

            db.Entry(account).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!accountExists(id))
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

        // POST: api/accounts
        [ResponseType(typeof(account))]
        public IHttpActionResult Postaccount(account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.accounts.Add(account);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = account.AccountId }, account);
        }

        // DELETE: api/accounts/5
        [ResponseType(typeof(account))]
        public IHttpActionResult Deleteaccount(int id)
        {
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return NotFound();
            }

            db.accounts.Remove(account);
            db.SaveChanges();

            return Ok(account);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool accountExists(int id)
        {
            return db.accounts.Count(e => e.AccountId == id) > 0;
        }
    }
}