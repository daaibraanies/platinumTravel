using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
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
        public DbSet<SlideModel> Slides { get; set; }



        //DB Fields

        public class MigrationsContextFactory : IDbContextFactory<PlatinumDBContext>
        {
            public PlatinumDBContext Create()
            {
                return new PlatinumDBContext(1,"s");
            }
        }

        //Соединение по умолчанию
        //Права только на СЕЛEКТ
        private PlatinumDBContext() : base("UserConnection") { }
       
        //Соединение авторизированного пользователя
        //Расширенные права
        private PlatinumDBContext(string Role) : base(Role + "Connection") { }

        private PlatinumDBContext(int i, string sd) : base("root") { }

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