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
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ProjectController : ApiController
    {
        private dbAleaEntities db = new dbAleaEntities();

        // GET: api/Project
        public IQueryable<Project> GettblProjektis()
        {
          
                db.Configuration.ProxyCreationEnabled = false;

                var projekt = from b in db.tblProjektis
                 
                    select new Project()
                    {
                        id = b.id,
                        nazivProjekta = b.nazivProjekta,
                        Opis=b.Opis,
                        idNadredjenog = b.idNadredjenog,
                        NadIme = b.tblNadredjeni.Ime,
                        status = b.status

                    };

                return projekt;
            }


        // GET: api/Project/5
        //[ResponseType(typeof(tblProjekti))]
        //public async Task<IHttpActionResult> GettblProjekti(int id)
        //{
        //    tblProjekti tblProjekti = await db.tblProjektis.FindAsync(id);
        //    if (tblProjekti == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tblProjekti);
        //}
        // GET: api/Zaposleni/5

        [ResponseType(typeof(tblProjekti ))]
        public async Task<IHttpActionResult> GettblProjekti(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var projekat = await db.tblProjektis.Include(b => b.id).Select(b =>
                new Project()
                {
                    id = b.id,
                    nazivProjekta=b.nazivProjekta,
                    Opis=b.Opis,
                    idNadredjenog=b.idNadredjenog,
                    status = b.status,
                    NadIme = b.tblNadredjeni.Ime,
                    
                    
                }).SingleOrDefaultAsync(b => b.id == id);
            if (projekat  == null)
            {
                return NotFound();
            }

            return Ok(projekat);
        }

        //// PUT: api/Project/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PuttblProjekti(int id, tblProjekti tblProjekti)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != tblProjekti.id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(tblProjekti).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!tblProjektiExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // PUT: api/projekti/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblProjekti(int id, tblProjekti tblProjekti)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var radnik = new tblProjekti 
            {
                id = tblProjekti.id,
                nazivProjekta = tblProjekti.nazivProjekta,
                Opis = tblProjekti.Opis,
                idNadredjenog = tblProjekti.idNadredjenog,
                 status = tblProjekti .status

            };



            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != tblZaposleni.id)
            //{
            //    return BadRequest();
            //}

            db.Entry(tblProjekti).State = EntityState.Modified;

            //try
            //{
            db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!tblZaposleniExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return StatusCode(HttpStatusCode.OK);
        }


        // POST: api/Project
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

        // DELETE: api/Project/5
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