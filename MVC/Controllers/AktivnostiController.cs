using System;
using System.Collections.Generic;

using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class AktivnostiController : Controller
    {

        private dbAleaEntities db = new dbAleaEntities();

        public ActionResult Index()
        {
            IEnumerable<mvcAktivnosti> aktivnost;
            {
              
                if (Session["LogedUserID"] != null)
                {
                    if (Session["LogedUserLevel"] != null)
                    {
                        if (Session["LogedUserLevel"].ToString() != "User")
                        {
                            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("activities").Result;
                            aktivnost = response.Content.ReadAsAsync<IEnumerable<mvcAktivnosti>>().Result;

                            return View(aktivnost);
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
        }


      
       

      
        [HttpPost]
        public ActionResult Edit(mvcAktivnosti zap)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("activities", zap).Result;

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id = 0)
        {

            if (id == 0)
                return View(new mvcAktivnosti());
            else
            {

                HttpResponseMessage response =
                    GlobalVariables.WebApiClient.GetAsync("activities/" + id).Result;
                return View(response.Content.ReadAsAsync<mvcAktivnosti>().Result);
            }

        }



        [HttpPut]
        public ActionResult Edit(tblAktivnosti zap, int id)
        {

            HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("aktivnosti", zap).Result;

            return RedirectToAction("Index");
        }

      


        public ActionResult Details(int id)
        {

            if (id == 0)
                return View(new mvcAktivnosti());
            else
            {

                HttpResponseMessage response =
                    GlobalVariables.WebApiClient.GetAsync("activities/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcAktivnosti>().Result);
            }
        }

  public ActionResult Create(int id = 0)
        {

            return View(new mvcAktivnosti());
        }

        [HttpPost]

        public ActionResult Create(mvcAktivnosti zap)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("activities", zap).Result;

            return RedirectToAction("Index");
        }

        public ActionResult AddOrEdit(int id = 0)
        {

            if (id == 0)
            {

                if (Session["LogedUserLevel"] != null)
                {
                    if (Session["LogedUserLevel"].ToString() == "User")
                    {
                        return RedirectToAction("LowCredentials", "Home", new { area = "" });


                    }
                    else
                    {

                        return View(new mvcAktivnosti()); //kreiranje novog projekta
                    }
                }
                else
                {
                    return RedirectToAction("WrongCredentials", "Home", new { area = "" });

                }
            }


            else
            {
                if (Session["LogedUserLevel"] != null)
                {
                    if (Session["LogedUserLevel"].ToString() == "User")
                    {
                        return RedirectToAction("LowCredentials", "Home", new { area = "" });

                    }
                    else
                    {
                        HttpResponseMessage response =
                            GlobalVariables.WebApiClient.GetAsync("activities/" + id).Result;
                        return View(response.Content.ReadAsAsync<mvcAktivnosti>().Result);

                    }
                }
                else
                {
                    return RedirectToAction("WrongCredentials", "Home", new { area = "" });

                }

            }

        }


        [HttpPost]
        public ActionResult AddOrEdit(mvcProjekti zap)
        {

            if (Session["LogedUserLevel"] != null)
                if (Session["LogedUserLevel"].ToString() != "User")
                {
                    return RedirectToAction("LowCredentials", "Home", new { area = "" });

                }
                else
                {

                    HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("activities", zap).Result;

                    return RedirectToAction("Index");
                }
            else
            {
                return RedirectToAction("WrongCredentials", "Home", new { area = "" });

            }
        }

        //********************************************************
        public ActionResult Start(int id = 0)
        {

            if (id == 0)
            {

                if (Session["LogedUserLevel"] != null)
                {
                    if (Session["LogedUserLevel"].ToString() == "User")
                    {
                        return RedirectToAction("LowCredentials", "Home", new { area = "" });


                    }
                    else
                    {

                        return View(new mvcAktivnosti()); //kreiranje novog projekta
                    }
                }
                else
                {
                    return RedirectToAction("WrongCredentials", "Home", new { area = "" });

                }
            }


            else
            {
                if (Session["LogedUserLevel"] != null)
                {
                    if (Session["LogedUserLevel"].ToString() != "User")
                    {
                        return RedirectToAction("LowCredentials", "Home", new { area = "" });

                    }
                    else
                    {
                        HttpResponseMessage response =
                            GlobalVariables.WebApiClient.GetAsync("activities/" + id).Result;
                        return View(response.Content.ReadAsAsync<mvcAktivnosti>().Result);

                    }
                }
                else
                {
                    return RedirectToAction("WrongCredentials", "Home", new { area = "" });

                }

            }

        }


        [HttpPost]
        public ActionResult Start(mvcProjekti zap)
        {

            if (Session["LogedUserLevel"] != null)
                if (Session["LogedUserLevel"].ToString() != "User")
                {
                    return RedirectToAction("LowCredentials", "Home", new { area = "" });

                }
                else
                {

                    HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("activities", zap).Result;

                    return RedirectToAction("Index");
                }
            else
            {
                return RedirectToAction("WrongCredentials", "Home", new { area = "" });

            }
        }



    }
}