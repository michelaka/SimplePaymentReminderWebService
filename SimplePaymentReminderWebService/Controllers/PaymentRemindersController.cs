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
using SimplePaymentReminderDataAccessNF;

namespace SimplePaymentReminderWebService.Controllers
{
    public class PaymentRemindersController : ApiController
    {
        private SimplePaymentReminderEntities db = new SimplePaymentReminderEntities();

        // GET: api/PaymentReminders
        public IQueryable<PaymentReminder> GetPaymentReminders()
        {
            return db.PaymentReminders;
        }

        // GET: api/PaymentReminders/5
        [ResponseType(typeof(PaymentReminder))]
        public IHttpActionResult GetPaymentReminder(int id)
        {
            PaymentReminder paymentReminder = db.PaymentReminders.Find(id);
            if (paymentReminder == null)
            {
                return NotFound();
            }

            return Ok(paymentReminder);
        }

        // PUT: api/PaymentReminders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPaymentReminder(int id, PaymentReminder paymentReminder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paymentReminder.ID)
            {
                return BadRequest();
            }

            db.Entry(paymentReminder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentReminderExists(id))
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

        // POST: api/PaymentReminders
        [ResponseType(typeof(PaymentReminder))]
        public IHttpActionResult PostPaymentReminder(PaymentReminder paymentReminder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PaymentReminders.Add(paymentReminder);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = paymentReminder.ID }, paymentReminder);
        }

        // DELETE: api/PaymentReminders/5
        [ResponseType(typeof(PaymentReminder))]
        public IHttpActionResult DeletePaymentReminder(int id)
        {
            PaymentReminder paymentReminder = db.PaymentReminders.Find(id);
            if (paymentReminder == null)
            {
                return NotFound();
            }

            db.PaymentReminders.Remove(paymentReminder);
            db.SaveChanges();

            return Ok(paymentReminder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaymentReminderExists(int id)
        {
            return db.PaymentReminders.Count(e => e.ID == id) > 0;
        }
    }
}