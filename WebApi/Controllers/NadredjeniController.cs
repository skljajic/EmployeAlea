using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    [EnableCors(origins: "http://localhost:49958", headers: "*", methods: "*")]
    public class NadredjeniController : ApiController
    {
        private dbAleaEntities db = new dbAleaEntities();

        // GET: api/Nadredjeni
        public IQueryable<tblNadredjeni> GettblNadredjenis()
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.tblNadredjenis;
        }

        // GET: api/Nadredjeni/5
        [ResponseType(typeof(tblNadredjeni))]
        public IHttpActionResult GettblNadredjeni(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tblNadredjeni tblNadredjeni = db.tblNadredjenis.Find(id);
            if (tblNadredjeni == null)
            {
                return NotFound();
            }

            return Ok(tblNadredjeni);
        }

        // PUT: api/Nadredjeni/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblNadredjeni(int id, tblNadredjeni tblNadredjeni)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblNadredjeni.id)
            {
                return BadRequest();
            }

            db.Entry(tblNadredjeni).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblNadredjeniExists(id))
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

        // POST: api/Nadredjeni
        [ResponseType(typeof(tblNadredjeni))]
        public IHttpActionResult PosttblNadredjeni(tblNadredjeni tblNadredjeni)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblNadredjenis.Add(tblNadredjeni);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tblNadredjeniExists(tblNadredjeni.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tblNadredjeni.id }, tblNadredjeni);
        }

        // DELETE: api/Nadredjeni/5
        [ResponseType(typeof(tblNadredjeni))]
        public IHttpActionResult DeletetblNadredjeni(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tblNadredjeni tblNadredjeni = db.tblNadredjenis.Find(id);
            if (tblNadredjeni == null)
            {
                return NotFound();
            }

            db.tblNadredjenis.Remove(tblNadredjeni);
            db.SaveChanges();

            return Ok(tblNadredjeni);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblNadredjeniExists(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tblNadredjenis.Count(e => e.id == id) > 0;
        }
    }
}