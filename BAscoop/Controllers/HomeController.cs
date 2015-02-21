using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAscoop.Models;

namespace BAscoop.Controllers
{
    public class HomeController : Controller
    {
        private BioscoopDb db = new BioscoopDb();

        public ActionResult Index()
        {
            ViewBag.Message = "Kijk bij ons de nieuwste films!";

            return View(db.Movies.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
