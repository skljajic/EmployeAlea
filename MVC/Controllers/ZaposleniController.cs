using System;
using System.Collections.Generic;

using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class ZaposleniController : Controller
    {
        
        private dbAleaEntities db = new dbAleaEntities();

        public ActionResult Index()
        {
            IEnumerable<mvcZaposleni> zaposlen;
            {
                if (Session["LogedUserID"] != null)
                {
                    if (Session["LogedUserLevel"] != null)
                    {
                        if (Session["LogedUserLevel"].ToString() != "User")
                        {
                            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("employee").Result;
                            zaposlen = response.Content.ReadAsAsync<IEnumerable<mvcZaposleni>>().Result;

                            return View(zaposlen);
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
        [Route("/api/login")]
        public ActionResult Login(mvcZaposleni objuserlogin)

        {

            var display = Userloginvalues().Where(m => m.username == objuserlogin.username  && m.password == objuserlogin.password).FirstOrDefault();

            if (display != null)

            {

                ViewBag.Status = "CORRECT UserNAme and Password";

            }

            else

            {

                ViewBag.Status = "INCORRECT UserName or Password";

            }

            return View(objuserlogin);



        }

        public List<mvcZaposleni> Userloginvalues()

        {

            List<mvcZaposleni> objModel = new List<mvcZaposleni>();

       
            return objModel;

        }




        public ActionResult AddOrEdit(int id=0)
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

                        return View(new mvcZaposleni()); //kreiranje novog projekta
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
                                GlobalVariables.WebApiClient.GetAsync("employee/" + id).Result;
                            return View(response.Content.ReadAsAsync<mvcZaposleni>().Result);

                        }
                    }
                    else
                    {
                        return RedirectToAction("WrongCredentials", "Home", new { area = "" });

                    }

                }

        }

        public ActionResult Edit(int id = 0)
        {

          
                return View(new mvcZaposleni());
         

        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcZaposleni zap)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("employee",zap).Result;
         
            return RedirectToAction("Index");
        }

        [HttpPut]
        public ActionResult Edit(tblZaposleni zap,int id)
        {

            HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("zaposleni", zap).Result;

            return RedirectToAction("Index");
        }


        
        public ActionResult Details(int id)
        {

            if (id == 0)
                return View(new mvcZaposleni());
            else
            {
                
                HttpResponseMessage response =
                    GlobalVariables.WebApiClient.GetAsync("employee/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcZaposleni>().Result);
            }
        }


        [HttpPost]
        
        public ActionResult Create(mvcZaposleni zap)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("employee", zap).Result;

            return RedirectToAction("Index");
        }

        public ActionResult Create(int id=0)
        {
         
         return View(new mvcZaposleni());
        }

    }
}