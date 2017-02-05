using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlatinumTravel.Models;
using PlatinumTravel.Filters;
using NLog;
using PlatinumTravel.Models;

namespace PlatinumTravel.Controllers
{
    /// <summary>
    /// Управление контентом на сайте.
    /// Вход только для авторизированных пользователей.
    /// </summary>
    [Authentificate]
    public class ManageController : Controller
    {
        Logger testLog = LogManager.GetCurrentClassLogger();
        ///TODO
        ///Убрать многопоточные вызовы с бд и посмотреть как будет лучше


        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }

        //TODO ПРОВЕРКИ
        [HttpGet]
        public ActionResult ShowUsers()
        {
            IEnumerable<Profile> allUsers;
            testLog.Info("Запрос данных о пользователях. " + HttpContext.Profile.UserName);

            try
            {
                allUsers = PlatinumDBContext.GetConnection().Profiles;
                return View(allUsers);
            }
            catch(Exception e)
            {
                testLog.Warn("Исключение при попытке доступа к бд. " + e.Message);
                return RedirectToAction("Index");
            }

        }
        
        /// <summary>
        /// Получть данные для изменения пользователя
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditUserProfile(int? Id)
        {
            if (Id.HasValue)
            {
                try
                {
                    testLog.Info("Запрос на изменение данных пользователя ид: " + Id + ". Кто:" + HttpContext.User.Identity.Name);
                    using (PlatinumDBContext db = PlatinumDBContext.GetConnection())
                    {
                        return View(db.Profiles.Find(Id).Trimmed());
                    }
                }
                catch (Exception e)
                {
                    testLog.Warn("Исключение при поптыке извлечь данные пользователя. " + e.Message);
                }
                return View("Index");
            }
            return View("Index");
        }

        /// <summary>
        /// Отправить изменения на сервер           //TODO НА ВЫКЛАДКЕ ВЕЗДЕ ПЕРЕПИЛИТЬ ПЕРЕНАПРАВЛЕНИЯ В СЛУЧАЕ ИСКЛЮЧЕНИЙ
        /// </summary>
        /// <param name="dataToEdit"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserProfile(Profile dataToEdit)
        {
            Profile userToEdit = null;
            if (ModelState.IsValid)
            {
                try
                {
                    using(PlatinumDBContext db = PlatinumDBContext.GetConnection())
                    {
                        userToEdit = db.Profiles.Find(dataToEdit.Id);

                        if (userToEdit != null)
                        {
                            userToEdit.UserName = dataToEdit.UserName;
                            userToEdit.Password = dataToEdit.Password;
                            userToEdit.Role = dataToEdit.Role;

                            try
                            {
                                db.Entry(userToEdit).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                return RedirectToAction("ShowUsers");
                            }
                            catch(Exception e)
                            {
                                testLog.Warn("Исключение при поптыке внесения изменения в БД. " + e.Message);
                                return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Проблема с запросом объекта.");
                            testLog.Warn("Проблема с извлечением запрашиваемого объекта.");
                            return new HttpNotFoundResult();
                        }
                    }
                }
                catch(Exception e)
                {
                    testLog.Warn("Исключение при поптыке доступа к БД. " + e.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", "Некорректно заполнена форма.");
            }

            return View(dataToEdit);
        }


        /// <summary>
        /// Вызов формы создания пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddUser()
        {   
            return View();
        }


        /// <summary>
        /// Добавить нового пользователя для управления контентом
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(AddUserViewModel newUser)
        {
            testLog.Info("Попытка добавления пользователя. Кто:" + HttpContext.User.Identity.Name);

            if (ModelState.IsValid)
            {
                Profile profileToAdd = new Profile
                {
                    UserName = newUser.UserName,
                    Password = newUser.Password,
                    Role = "Admin"                                                                          //TODO Роль по умолчанию Исправить если будет несколько ролей
                };

                try
                {
                    using(PlatinumDBContext db = PlatinumDBContext.GetConnection())
                    {
                        db.Profiles.Add(profileToAdd);
                        db.Entry(profileToAdd).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                    }
                    testLog.Info("Пользователь " + profileToAdd.UserName + "создан. Создал: " + HttpContext.User.Identity.Name);
                    return RedirectToAction("ShowUsers");
                }
                catch (Exception e)
                {
                    testLog.Warn("Исключение при попытке создания нового пользователя. " + e.Message);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Неправильно заполнена форма");
            }
            return View(newUser);
        }

        /// <summary>
        /// Удаляет пользователя по ид
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteUser(int? Id)
        {
            Profile userToDelete = new Profile();
            if (Id.HasValue)
            {
                testLog.Info("Попытка удаления пользователя. Кто:" + HttpContext.User.Identity.Name);

                try
                {
                    using(PlatinumDBContext db = PlatinumDBContext.GetConnection())
                    {
                        userToDelete = db.Profiles.Find(Id);
                        db.Profiles.Attach(userToDelete);
                        db.Profiles.Remove(userToDelete);
                        db.SaveChanges();
                    }
                    return RedirectToAction("ShowUsers");
                }
                catch(Exception e)
                {
                    testLog.Warn("Исключение при попытке удаления пользователя. " + e.Message);
                }
            }
            return RedirectToAction("ShowUsers");                           //Подумать куда можно перенаправить и как выводить ошибки. Мб создать страницу общую для ошибок.
            
        }
    }
}