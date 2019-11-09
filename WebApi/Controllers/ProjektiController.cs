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
    public class ProjektiController : ApiController
    {
        private dbAleaEntities db = new dbAleaEntities();

        // GET: api/Projekti
        public IQueryable<tblProjekti> GettblProjektis()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tblProjektis;
        }

        // GET: api/Projekti/5
        [ResponseType(typeof(tblProjekti))]
        public async Task<IHttpActionResult> GettblProjekti(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
           
            tblProjekti tblProjekti = await db.tblProjektis.FindAsync(id);
            if (tblProjekti == null)
            {
                return NotFound();
            }

            return Ok(tblProjekti);
        }

        // PUT: api/Projekti/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblProjekti(int id, tblProjekti tblProjekti)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblProjekti.id)
            {
                return BadRequest();
            }

            db.Entry(tblProjekti).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjektiExists(id))
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

        // POST: api/Projekti
        [ResponseType(typeof(tblProjekti))]
        public async Task<IHttpActionResult> PosttblProjekti(tblProjekti tblProjekti)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblProjektis.Add(tblProjekti);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblProjekti.id }, tblProjekti);
        }

        // DELETE: api/Projekti/5
        [ResponseType(typeof(tblProjekti))]
        public async Task<IHttpActionResult> DeletetblProjekti(int id)
        {
            tblProjekti tblProjekti = await db.tblProjektis.FindAsync(id);
            if (tblProjekti == null)
            {
                return NotFound();
            }

            db.tblProjektis.Remove(tblProjekti);
            await db.SaveChangesAsync();

            return Ok(tblProjekti);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjektiExists(int id)
        {
            return db.tblProjektis.Count(e => e.id == id) > 0;
        }
    }
}