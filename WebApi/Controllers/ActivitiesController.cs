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

    public class ActivitiesController : ApiController
    {
        private dbAleaEntities db = new dbAleaEntities();

     public IQueryable<Activity> GettblAktivnostis()
        {
            db.Configuration.ProxyCreationEnabled = false;

            var zaposleni = from b in db.tblAktivnostis
                //   where (b.id==6)
                select new Activity()
                {
                    id = b.id,
                    Naslov=b.Naslov,
                    NadrIme = b.tblNadredjeni.Ime,
                    opis=b.opis,
                    start =b.start,
                    end=b.end,
                    startRadnik=b.startRadnik,
                    idNadredjenog=b.idNadredjenog,
                    idProjekta=b.idProjekta,
                    ProjIme = b.tblProjekti.nazivProjekta,
                    idRadnika =b.idRadnika,
                    RadnikIme =b.tblZaposleni.Ime
                };

            return zaposleni;
        }


        // GET: api/Activities/5
        [ResponseType(typeof(Activity))]
        public async Task<IHttpActionResult> GettblAktivnosti(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var activity = await db.tblAktivnostis.Include(b => b.id).Select(b =>
                new Activity()
                {
                    id = b.id,
                    end = b.end,
                    idNadredjenog = b.idNadredjenog,
                    idProjekta = b.idProjekta,
                    idRadnika = b.idRadnika,
                    NadrIme = b.tblNadredjeni.Ime,
                    Naslov = b.Naslov,
                    opis = b.opis,
                    ProjIme = b.tblProjekti.nazivProjekta,
                    RadnikIme = b.tblZaposleni.Ime,
                    start = b.start,
                    startRadnik = b.startRadnik

                }).SingleOrDefaultAsync(b => b.id == id);
            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        }

        //// PUT: api/Activities/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutActivity(int id, Activity activity)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != activity.id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(activity).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ActivityExists(id))
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
        // PUT: api/Zaposleni/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblAktivnosti(int id, tblAktivnosti tblAktivnosti)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var aktivnost = new tblAktivnosti
            {
                id = tblAktivnosti.id,
                end = tblAktivnosti.end,
                idNadredjenog = (int) tblAktivnosti.idNadredjenog,
                idProjekta = (int) tblAktivnosti.idProjekta,
                idRadnika = (int) tblAktivnosti.idRadnika,
                Naslov = tblAktivnosti.Naslov,
                opis = tblAktivnosti.opis,
                start = tblAktivnosti.start,
                startRadnik=tblAktivnosti.startRadnik,
                          




            };



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
                db.SaveChanges();
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

            return StatusCode(HttpStatusCode.OK);
        }


        // POST: api/Activities
        [ResponseType(typeof(tblAktivnosti))]
        public async Task<IHttpActionResult> PosttblAktivnosti(tblAktivnosti tblAktivnosti)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblAktivnostis.Add(tblAktivnosti);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblAktivnosti.id }, tblAktivnosti);
        }

        //// DELETE: api/Activities/5
        //[ResponseType(typeof(Activity))]
        //public async Task<IHttpActionResult> DeleteActivity(int id)
        //{
        //    Activity activity = await db.tblAktivnosti.FindAsync(id);
        //    if (activity == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Activities.Remove(activity);
        //    await db.SaveChangesAsync();

        //    return Ok(activity);
        //}

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