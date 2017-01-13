using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using EQServiceSystem.Models;

namespace EQServiceSystem.Controllers
{
    public class HomeController : Controller
    {       

        public ActionResult Index()
        {
            if (!WebSecurity.IsAuthenticated)
            {
                return View("~/Views/Account/Login.cshtml");
            }
            else
            {
                var cur_user = WebSecurity.CurrentUserName;
                var role = System.Web.Security.Roles.GetRolesForUser(cur_user).Single();
                ViewBag.Message = role;
                return View("~/Views/Home/MainUserPage.cshtml", new MenuLogic(cur_user));
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
