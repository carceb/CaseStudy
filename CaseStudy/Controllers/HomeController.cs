using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaseStudy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "CODIT LEARNING TEST PROJECT.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Para contactar:";

            return View();
        }
    }
}