using System;
using System.Collections.Generic;

using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class ProjektiController : Controller
    {

        private dbAleaEntities db = new dbAleaEntities();

        public ActionResult Index()
        {
            IEnumerable<mvcProjekti> zaposlen;

            if (Session["LogedUserID"] != null)
            {
                if (Session["LogedUserLevel"] != null)
                {
                    if (Session["LogedUserLevel"].ToString() != "User")
                    {
                        HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("project").Result;
                        zaposlen = response.Content.ReadAsAsync<IEnumerable<mvcProjekti>>().Result;

                        return View(zaposlen);
                    }
                    else
                    {
                        return RedirectToAction("LowCredentials", "Home", new {area = ""});
                    }
                }
                else
                {
                    return RedirectToAction("WrongCredentials", "Home", new {area = ""});

                }
            }
            else
            {
                return RedirectToAction("WrongCredentials", "Home", new {area = ""});

            }
        }


        public ActionResult AddOrEdit(int id = 0)
        {

            if (id == 0)
            {

                if (Session["LogedUserLevel"] != null)
                {
                    if (Session["LogedUserLevel"].ToString() == "User")
                    {
                        return RedirectToAction("LowCredentials", "Home", new {area = ""});


                    }
                    else
                    {
                         
                                return View(new mvcProjekti()); //kreiranje novog projekta
                    }
                }
                else
                {
                    return RedirectToAction("WrongCredentials", "Home", new {area = ""});

                }
            }


            else
            {
                if (Session["LogedUserLevel"] != null)
                {
                    if (Session["LogedUserLevel"].ToString() == "User")
                    {
                        return RedirectToAction("LowCredentials", "Home", new {area = ""});

                    }
                    else
                    {
                        HttpResponseMessage response =
                            GlobalVariables.WebApiClient.GetAsync("project/" + id).Result;
                        return View(response.Content.ReadAsAsync<mvcProjekti>().Result);

                    }
                }
                else
                {
                    return RedirectToAction("WrongCredentials", "Home", new {area = ""});

                }

            }

        }


        [HttpPost]
        public ActionResult AddOrEdit(mvcProjekti zap)
        {

            if (Session["LogedUserLevel"] != null)
                if (Session["LogedUserLevel"].ToString() == "User")
                {
                    return RedirectToAction("LowCredentials", "Home", new {area = ""});

                }
                else
                {

                    HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("project", zap).Result;

                    return RedirectToAction("Index");
                }
            else
            {
                return RedirectToAction("WrongCredentials", "Home", new {area = ""});

            }
        }
    


        public ActionResult Details(int id)
        {

            if (id == 0)
            {

                if (Session["LogedUserLevel"] != null)
                {
                    if (Session["LogedUserLevel"].ToString() == "User")
                    {
                        return RedirectToAction("LowCredentials", "Home", new {area = ""});

                    }
                    else
                    {
                        return View(new mvcProjekti()); //kreiranje novog projekta
                    }
                }
                else
                {
                    return RedirectToAction("WrongCredentials", "Home", new {area = ""});

                }
            }
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
                                GlobalVariables.WebApiClient.GetAsync("project/" + id).Result;
                            return View(response.Content.ReadAsAsync<mvcProjekti>().Result);
                        
                    }
                }
                else
                {
                    return RedirectToAction("WrongCredentials", "Home", new { area = "" });

                }

            }

        }




      
    

   

    }
}