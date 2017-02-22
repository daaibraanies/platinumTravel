using System.Text;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;


namespace PlatinumTravel.Controllers
{
    public class TravelController : Controller
    {
      
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string where,string startdate="",string enddate="")
        {
            //TESTING TO DO !!!
            PlatinumTravel.Models.Countries repo = new PlatinumTravel.Models.Countries();  
            if(repo.countries.ContainsKey(where))
            {
                ViewBag.ctrcd = repo.countries[where];
            }
            if(!string.IsNullOrEmpty(startdate) || !string.IsNullOrEmpty(enddate))
            {
                ViewBag.strdt = startdate;
                ViewBag.enddt = enddate;
            }
            return View();
        }

    }
}