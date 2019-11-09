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
    public class EmployeeController : ApiController
    {
        private dbAleaEntities db = new dbAleaEntities();

        // GET: api/Zaposleni
        public IQueryable<Employee> GettblZaposlenis()
        {
            db.Configuration.ProxyCreationEnabled = false;

            var zaposleni = from b in db.tblZaposlenis
                         //   where (b.id==6)
             select new Employee()
                {
                    id = b.id,
                    Ime=b.Ime,
                    level=b.level.ToString(),
                    NadIme=b.tblNadredjeni.Ime,
                    username=b.username,
                    password=b.password,
                    nadredjen=b.nadredjen,
                    status=b.status

                };

            return zaposleni;
        }

        // GET: api/Zaposleni/5

        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> GettblZaposleni(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var employee = await db.tblZaposlenis.Include(b => b.id).Select(b =>
                new Employee()
                {
                    id = b.id,
                    username  = b.username,
                    password  = b.password,
                    Ime = b.Ime,
                    nadredjen  = b.nadredjen,
                     level=b.level,
                      status=b.status,
                    NadIme = b.tblNadredjeni.Ime
                }).SingleOrDefaultAsync(b => b.id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
        //[ResponseType(typeof(Employee))]
        //public IHttpActionResult GettblZaposleni(int id)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;

        //    tblZaposleni tblZaposleni = db.tblZaposlenis.Find(id);

        //    if (tblZaposleni == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //         var zaposleni = from b in db.tblZaposlenis
        //        where id == tblZaposleni.id                   
        //         select new Employee()

        //         {
        //             id = tblZaposleni.id,
        //             Ime = tblZaposleni.Ime,
        //             level = tblZaposleni.level,
        //             NadIme = tblZaposleni.tblNadredjeni.Ime,
        //             username = tblZaposleni.username,
        //             password = tblZaposleni.password,
        //             nadredjen = tblZaposleni.nadredjen,
        //             status = tblZaposleni.status
        //         };

        //        return Ok(zaposleni);

        //     }

        //}
        ////  [ResponseType(typeof(Employee))]
        //  public Employee GettblZaposleni(int id)
        //  {
        //      db.Configuration.ProxyCreationEnabled = false;
        //      tblZaposleni tblZaposleni = db.tblZaposlenis.Find(id);

        //      if (tblZaposleni != null)
        //      {
        //          var zaposleni = new Employee()
        //          {
        //              id = tblZaposleni.id,
        //              Ime = tblZaposleni.Ime,
        //              level = tblZaposleni.level,
        //              NadIme = tblZaposleni.tblNadredjeni.Ime,
        //              username = tblZaposleni.username,
        //              password = tblZaposleni.password,
        //              nadredjen = tblZaposleni.nadredjen,
        //              status = tblZaposleni.status

        //          };

        //          return zaposleni;
        //      }
        //      else
        //      {
        //          return null;
        //      }
        //  }

        // PUT: api/Zaposleni/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public  IHttpActionResult PuttblZaposleni(int id, Employee tblZaposleni)
        {
            var radnik = new tblZaposleni
            {
                id=tblZaposleni.id, 
                username=tblZaposleni.username,
                password=tblZaposleni.password,
                 Ime=tblZaposleni.Ime,
                  level=tblZaposleni.level,
                   nadredjen=tblZaposleni.nadredjen,
                    status=tblZaposleni.status 

            };

          
          
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != tblZaposleni.id)
            //{
            //    return BadRequest();
            //}

            db.Entry(tblZaposleni).State = EntityState.Modified;

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