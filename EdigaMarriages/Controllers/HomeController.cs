
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EdigaMarriages.Controllers
{
    public class HomeController : Controller
    {
        //private MarriagesDB marriagesDB;

        public HomeController()
        {
            //marriagesDB = new MarriagesDB();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Gowda Ediga Marriages.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Gowda Ediga Marriages.";

            return View();
        }

        public JsonResult GetSurnames()
        {
            List<string> surnames = new List<string>();

            //surnames = marriagesDB.GetSurnames();

            return Json(surnames, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login(string password = "")
        {
            if (password.Equals("samsung@123"))
            {
                HttpCookie aCookie = new HttpCookie("admin", "true");
                //Request.Cookies.Remove("admin");
                Response.Cookies.Add(aCookie);
            }

            return View("Index");
        }

        public ActionResult Terms()
        {
            return View();
        }
    }
}