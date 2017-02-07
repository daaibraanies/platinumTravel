using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlatinumTravel.Controllers
{
    public class ToursController : Controller
    {
        // GET: Tours
        public ActionResult Index()
        {
            return View();
        }
    }
}