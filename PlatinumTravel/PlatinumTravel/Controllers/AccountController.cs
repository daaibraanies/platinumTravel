using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlatinumTravel.Models;
using System.Web.Mvc;
using System.Web.Security;
using NLog;

namespace PlatinumTravel.Controllers
{
    /// <summary>
    /// Ауентификация и атворизация пользователей
    /// через БД 
    /// </summary>
    public class AccountController : Controller
    {
        Logger testLog = LogManager.GetCurrentClassLogger();            //Тестовый логгер УБРАТЬ ПОСЛЕ ВЫКЛАДКИ
                
        [HttpGet]
        public ActionResult Login()
        {
            //TODO:
            //Роль в бд отображать интом, 
            //добавить проверку на роль 
            //пользователя на случай нескольких ролей.

            if (HttpContext.User.Identity.IsAuthenticated) return RedirectToAction("Index", "Manage");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            //TODO
            //Связь с бд, обработка возможных исключений
            //логирование событий
            Profile connectingUser = null;

            if (ModelState.IsValid)
            {
                testLog.Info("Попытка входа. Данные при входе: " + model.UserName+"," + model.Password);

                try
                {
                    using(PlatinumDBContext db = PlatinumDBContext.GetConnection())
                    {
                        connectingUser = db.Profiles.FirstOrDefault(x=>x.UserName == model.UserName); 
                    }
                }
                catch(Exception e)
                {
                    testLog.Warn("Исключение при попытке доступа к бд. " + e.Message);
                }

                if (connectingUser != null && connectingUser.Password.Trim() == model.Password.Trim())
                {
                    try
                    {
                        FormsAuthentication.SetAuthCookie(connectingUser.Role.Trim(), false);
                        testLog.Info("Доступ на правах администратора предоставлен " + connectingUser.UserName.Trim());

                        return RedirectToAction("Index","Manage");                          //Направить пользователя на страницу управления конентом
                    }
                    catch(Exception e)
                    {
                        testLog.Warn("Исключение при создании куки. " + e.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неверные данные.");
                    testLog.Info("В доступе отказано. Неверные данные. " +model.UserName+" "+model.Password);
                }

            }
            else
            {
                ModelState.AddModelError("","Необходимо ввести данные.");
            }
            return View(model);
        }

        
        public ActionResult Logout()
        {
            testLog.Info("Пользователь " + HttpContext.User.Identity.Name + " покинул панель одминистирования.");

            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }
    }
}