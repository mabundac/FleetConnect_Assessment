using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using Assessment.Models;
using System.Configuration;
using Assessment.Utilities;
using Assessment.Models.Repositories;

namespace Assessment.Controllers
{
   // [Authorize]
    public class AccountController : BaseController
    {

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            FormsAuthentication.SignOut();
            Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddYears(-1);
            System.Web.HttpContext.Current.User = null;
            WebSecurity.Logout();
            Session.Abandon();

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            //model.UserName = "mabundachris1@gmail.com";
            //model.Password = "adm!n123";


            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }


            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            System.Web.HttpContext.Current.User = null;
            Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddYears(-1);
            WebSecurity.Logout();
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion

        #region Password Recovery

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            var model = new ForgotPasswordModel();
            return View(model);
        }

        #endregion


    }
}
