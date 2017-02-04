using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlatinumTravel.Models;
using PlatinumTravel.Filters;

namespace PlatinumTravel.Controllers
{
    /// <summary>
    /// Управление контентом на сайте.
    /// Вход только для авторизированных пользователей.
    /// </summary>
    [Authentificate]
    public class ManageController : Controller
    {
        // GET: Manage
        public ActionResult Index()
        {
            using(PlatinumDBContext db = PlatinumDBContext.GetConnection())
            {
                var test = db.Profiles.Find(1);
            }
            return View();
        }
    }
}