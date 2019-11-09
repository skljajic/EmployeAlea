using MVC.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;


namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(tblZaposleni u)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (dbAleaEntities dc = new dbAleaEntities())
                {
                    var v = dc.tblZaposlenis.Where(a =>
                            a.username.Equals(u.username) && a.password.Equals(u.password) && a.status == true)
                        .FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserID"] = v.id.ToString();
                        Session["LogedUserFullname"] = v.Ime.ToString();
                        Session["LogedUserLevel"] = v.level.ToString();
                        Session["LogedUserNadredjenId"] = v.nadredjen.ToString();
                        Session["LogedUserStatus"] = v.status;
                        Session["LogedUserPassword"] = v.password;
                        Session["LogedUserUsername"] = v.username;


                        return RedirectToAction("AfterLogin");
                    }
                    else
                    {
                        Session["LogedUserID"] = "";
                        Session["LogedUserFullname"] = "";
                        Session["LogedUserLevel"] = "";
                        Session["LogedUserNadredjenId"] = "";
                        Session["LogedUserStatus"] = "";
                        Session["LogedUserPassword"] = "";
                        Session["LogedUserUsername"] = "";
                        return RedirectToAction("WrongCredentials");
                    }

                }
            }

            return View(u);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult AfterLogin()
        {
            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        public ActionResult WrongCredentials()
        {
            return View();
        }

        public ActionResult LowCredentials()
        {
            if (Session["LogedUserID"] != null)
            {

                return View();

            }
            else
            {
                return RedirectToAction("WrongCredentials");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff(tblZaposleni u)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                 
                Session["LogedUserID"] = "";
                    Session["LogedUserFullname"] = "";
                    Session["LogedUserLevel"] = "";
                    Session["LogedUserNadredjenId"] = "";
                    Session["LogedUserStatus"] = "";
                    Session["LogedUserPassword"] = "";

                    //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);


            }
            return RedirectToAction("LogOff");
        }
    }





}
