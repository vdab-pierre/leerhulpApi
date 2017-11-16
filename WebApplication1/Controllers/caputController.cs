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
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class caputController : ApiController
    {
        private leerhulpEntities db = new leerhulpEntities();

        // GET: api/caput
        public IQueryable<caputs> Getcaputs()
        {
            return db.caputs;
        }

        // GET: api/caput/5
        [ResponseType(typeof(caputs))]
        public IHttpActionResult Getcaputs(int id)
        {
            caputs caputs = db.caputs.Find(id);
            if (caputs == null)
            {
                return NotFound();
            }

            return Ok(caputs);
        }

        // PUT: api/caput/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcaputs(int id, caputs caputs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != caputs.id)
            {
                return BadRequest();
            }

            db.Entry(caputs).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!caputsExists(id))
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

        // POST: api/caput
        [ResponseType(typeof(caputs))]
        public IHttpActionResult Postcaputs(caputs caputs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.caputs.Add(caputs);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = caputs.id }, caputs);
        }

        // DELETE: api/caput/5
        [ResponseType(typeof(caputs))]
        public IHttpActionResult Deletecaputs(int id)
        {
            caputs caputs = db.caputs.Find(id);
            if (caputs == null)
            {
                return NotFound();
            }

            db.caputs.Remove(caputs);
            db.SaveChanges();

            return Ok(caputs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool caputsExists(int id)
        {
            return db.caputs.Count(e => e.id == id) > 0;
        }
    }
}