using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace PlatinumTravel.Filters
{
    /// <summary>
    /// Аттрибут используемый для ауентификации пользователя
    /// Испоьлзует куки. Служит для допуска в область управления.
    /// </summary>
    public class Authentificate : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if(user == null || !user.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();                //Выдает результат "не авторизированный юзер" - запрещает вход и срабатывает OnAuthenticationChallenge
            }

        }


        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)         //При неудачной ауентификации направляет по маршруту к странице ауентификации
        {
            var user = filterContext.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                    {
                       {"controller","Account"}, {"action","Login" }                        //Маршрут перехода при неудаче
                    });
            }
        }
    }
}