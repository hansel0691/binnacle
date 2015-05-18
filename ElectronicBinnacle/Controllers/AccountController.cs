using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using DotNetOpenAuth.AspNet;
using ElectronicBinnacle.Models.Context;
using ElectronicBinnacle.Models.Models.UserControl;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using ElectronicBinnacle.Filters;
using ElectronicBinnacle.Models;

namespace ElectronicBinnacle.Controllers
{
    [System.Web.Mvc.Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        [System.Web.Mvc.AllowAnonymous]
        public void Error401()
        {
            Response.StatusCode = 401;
            Response.End();
        }

        [System.Web.Mvc.AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        public JsonResult Login([FromBody]LoginModel model)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                using (var context = new MyContext())
                {
                    var user = context.GetActiveUser(model.UserName);
                    if (user == null) return Json(new {success = false}, JsonRequestBehavior.AllowGet);
                    return Json(new {success = true, user = user.JsonForAuth()}, JsonRequestBehavior.AllowGet);
                }
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Authorize]
        public void LogOff()
        {
            WebSecurity.Logout();
        }

        [System.Web.Mvc.AllowAnonymous]
        public JsonResult AuthUser(bool withOrderCount = false)
        {
            User authUser = null;
            using (var context = new MyContext())
                authUser = context.GetEmployeeUser(WebSecurity.CurrentUserId);
            if (authUser != null)
                return Json(new { authenticated = true, user = authUser.JsonForAuth(withOrderCount) }, JsonRequestBehavior.AllowGet);
            return Json(new { authenticated = false }, JsonRequestBehavior.AllowGet);
        }
    }
}
