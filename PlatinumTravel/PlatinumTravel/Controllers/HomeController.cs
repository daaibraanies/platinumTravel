using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using PlatinumTravel.Models;
using PlatinumTravel.Filters;

namespace PlatinumTravel.Controllers
{
    public class HomeController : Controller
    {
        Logger generalLogger = LogManager.GetCurrentClassLogger();
        public ActionResult Index()
        {
            return View();
        }

        [Authentificate]
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
    }
}