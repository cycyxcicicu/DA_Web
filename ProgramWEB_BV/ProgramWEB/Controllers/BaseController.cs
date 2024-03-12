using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ProgramWEB.Models;
using ProgramWEB.Define;
using ProgramWEB.Models.Data;
using ProgramWEB.Libary;
using System.Collections.Generic;
namespace ProgramWEB.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Lay du lieu tu session
            var userSession = Session[DefineSession.userSession];
            if (Session[DefineSession.beforeUrlSession] == null)
                Session.Add(DefineSession.beforeUrlSession, 
                    new string[] { filterContext.RouteData.Values["action"].ToString(),
                        filterContext.RouteData.Values["controller"].ToString()
                    });
            if (userSession == null)
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "User", action = "Login" }));
            base.OnActionExecuting(filterContext);
        }
        public bool sendEmail(string pathForm, string toEmail, string title, List<string> keys, List<string> values)
        {
            try
            {
                string content = System.IO.File.ReadAllText(Server.MapPath(pathForm));
                for (int i = 0; i < keys.Count; i++)
                {
                    content = content.Replace(keys[i], values[i]);
                }
                EmailHelper.SendEmail(toEmail, title, content);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}