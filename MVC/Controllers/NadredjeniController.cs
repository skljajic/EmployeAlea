using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class NadredjeniController : Controller
    {
        // GET: Nadredjeni
        public ActionResult Index()
        {
            IEnumerable<mvcNadredjeni> nadredjen;
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Nadredjeni").Result;
                nadredjen = response.Content.ReadAsAsync<IEnumerable<mvcNadredjeni>>().Result;
                return View(nadredjen);
            }
        }

        // GET: Nadredjeni/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Nadredjeni/Create
        public ActionResult Create()
        {
            return View();
        }

    }
}
