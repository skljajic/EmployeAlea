using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class IzvjestajiController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       

        // GET: Izvjestaji
        public ActionResult Index()
        {
            //   IEnumerable<mvcAktivnosti> aktivnosti;

            //   HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("activities").Result;
            //   aktivnosti = response.Content.ReadAsAsync<IEnumerable<mvcAktivnosti>>().Result;


            if (Session["LogedUserID"] != null)
            {
                if (Session["LogedUserLevel"] != null)
                {
                    if (Session["LogedUserLevel"].ToString() != "User")
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("LowCredentials", "Home", new { area = "" });
                    }
                }
                else
                {
                    return RedirectToAction("WrongCredentials", "Home", new { area = "" });

                }
            }
            else
            {
                return RedirectToAction("WrongCredentials", "Home", new { area = "" });

            }
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
