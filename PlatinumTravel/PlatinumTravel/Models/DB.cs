using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NLog;


namespace PlatinumTravel.Models
{
    /// <summary>
    /// Организует подключение к бд в зависимости от роли пользователя
    /// </summary>
    public class PlatinumDBContext : DbContext
    {
        //DB Fields
        public DbSet<Profile> Profiles { get; set; }   //TODO реализовать сеттер




        //DB Fields

        //Соединение по умолчанию
        //Права только на СЕЛEКТ
        private PlatinumDBContext() : base("UserConnection") { }
       
        //Соединение авторизированного пользователя
        //Расширенные права
        private PlatinumDBContext(string Role) : base(Role + "Connection") { }

        public static PlatinumDBContext GetConnection()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                try
                { 
                    return new PlatinumDBContext(HttpContext.Current.User.Identity.Name);
                }
                catch(Exception e)
                {
                    Logger testLog = LogManager.GetCurrentClassLogger();
                    testLog.Warn("Исключение при попытке соединения с бд пользователя " + HttpContext.Current.User.Identity.Name +" "+e.Message);
                    //TODO
                    //Создать страницу ошибки
                    //Создать лист с инфой о предоставленном подключении

                    return new PlatinumDBContext();
                }
            }
            else return new PlatinumDBContext();
        }
    }
}