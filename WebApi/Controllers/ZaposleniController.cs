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
    public class ZaposleniController : ApiController
    {
        private dbAleaEntities db = new dbAleaEntities();

        // GET: api/Zaposleni
        public IQueryable<tblZaposleni> GettblZaposlenis()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tblZaposlenis;
        }

        // GET: api/Zaposleni/5
        [ResponseType(typeof(tblZaposleni))]
        public IHttpActionResult GettblZaposleni(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tblZaposleni tblZaposleni = db.tblZaposlenis.Find(id);
            if (tblZaposleni == null)
            {
                return NotFound();
            }

            return Ok(tblZaposleni);
        }

        // PUT: api/Zaposleni/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblZaposleni(int id, tblZaposleni tblZaposleni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblZaposleni.id)
            {
                return BadRequest();
            }

            db.Entry(tblZaposleni).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblZaposleniExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }

        // POST: api/Zaposleni
        [ResponseType(typeof(tblZaposleni))]
        public IHttpActionResult PosttblZaposleni(tblZaposleni tblZaposleni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblZaposlenis.Add(tblZaposleni);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblZaposleni.id }, tblZaposleni);
        }

        // DELETE: api/Zaposleni/5
        [ResponseType(typeof(tblZaposleni))]
        public IHttpActionResult DeletetblZaposleni(int id)
        {
            tblZaposleni tblZaposleni = db.tblZaposlenis.Find(id);
            if (tblZaposleni == null)
            {
                return NotFound();
            }

            db.tblZaposlenis.Remove(tblZaposleni);
            db.SaveChanges();

            return Ok(tblZaposleni);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblZaposleniExists(int id)
        {
            return db.tblZaposlenis.Count(e => e.id == id) > 0;
        }
    }
}