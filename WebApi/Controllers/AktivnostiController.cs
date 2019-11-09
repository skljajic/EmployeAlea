using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    [EnableCors(origins: "http://localhost:49958", headers: "*", methods: "*")]

    public class AktivnostiController : ApiController
    {
        private dbAleaEntities db = new dbAleaEntities();

        // GET: api/Aktivnosti
        public IQueryable<tblAktivnosti> GettblAktivnostis()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tblAktivnostis;
        }

        // GET: api/Aktivnosti/5
        [ResponseType(typeof(tblAktivnosti))]
        public async Task<IHttpActionResult> GettblAktivnosti(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tblAktivnosti tblAktivnosti = await db.tblAktivnostis.FindAsync(id);
            if (tblAktivnosti == null)
            {
                return NotFound();
            }

            return Ok(tblAktivnosti);
        }

        // PUT: api/Aktivnosti/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblAktivnosti(int id, tblAktivnosti tblAktivnosti)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblAktivnosti.id)
            {
                return BadRequest();
            }

            db.Entry(tblAktivnosti).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblAktivnostiExists(id))
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

        // POST: api/Aktivnosti
        // POST: api/Projekti
        [ResponseType(typeof(tblAktivnosti))]
        public async Task<IHttpActionResult> PosttblAktivnosti(tblAktivnosti tblAktivnosti)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblAktivnostis.Add(tblAktivnosti);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblAktivnosti.id }, tblAktivnosti);
        }

        // DELETE: api/Aktivnosti/5
        [ResponseType(typeof(tblAktivnosti))]
        public async Task<IHttpActionResult> DeletetblAktivnosti(int id)
        {
            tblAktivnosti tblAktivnosti = await db.tblAktivnostis.FindAsync(id);
            if (tblAktivnosti == null)
            {
                return NotFound();
            }

            db.tblAktivnostis.Remove(tblAktivnosti);
            await db.SaveChangesAsync();

            return Ok(tblAktivnosti);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblAktivnostiExists(int id)
        {
            return db.tblAktivnostis.Count(e => e.id == id) > 0;
        }
    }
}