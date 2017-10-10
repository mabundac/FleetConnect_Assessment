using Assessment.Helpers;
using Assessment.Security;
using Assessment.Security.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Assessment.Controllers
{
    public abstract class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentUrl = Request.AppRelativeCurrentExecutionFilePath.ToLower();
            #region Is there a logged in User
          
           if ((currentUrl != "~/account/login" && currentUrl != "~/account/forgotpassword" && currentUrl != "~/account/resetpassword")
                && Assessment.Security.Helpers.SessionHelpers.LOGGED_IN_USER == null)
            {
                filterContext.Result = new RedirectResult(Url.Content("~/Account/Login"));
                return;
            }
            #endregion
            #region Javascript and Cookies check
            bool cont = false;
            //If the request method is a get, we dont need to check if javascript is enabled
            if (Request.HttpMethod == "GET")
            {
                //Min IE version check  
                cont = true;
                List<string> ieIdentifiers = new List<string>();
                ieIdentifiers.Add("msie");
                ieIdentifiers.Add("ie");
                ieIdentifiers.Add("internetexplorer");

                if (ieIdentifiers.Contains(Request.Browser.Browser.ToLower()) && Request.Browser.MajorVersion < 9)
                {
                    //Go to Minimum requirements
                    filterContext.Result = new RedirectResult(Url.Content("~/Content/MinimumRequirements.html"));
                }
            }
            else
            {
                //Request.HttpMethod == "POST", check that the cookie which is set in head via javascript has been sent through in the request
                if (Request.Cookies["jsEnabled"] != null || Request.IsAjaxRequest())
                {
                    cont = true;
                }
            }

            //if (cont)
            //{
            //    //Carry on to requested Action / Controller
            //    base.OnActionExecuting(filterContext);
            //}
            //else
            //{
            //    //Go to Minimum requirements
            //    filterContext.Result = new RedirectResult(Url.Content("~/Content/MinimumRequirements.html"));
            //    return;
            //}
            #endregion
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception as HttpAntiForgeryException;
            if (exception == null) return;

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.ExceptionHandled = true;
            }
            else
            {
                var routeValues = new RouteValueDictionary();
                routeValues["controller"] = "Account";
                routeValues["action"] = "Login";
                filterContext.Result = new RedirectToRouteResult(routeValues);
                filterContext.ExceptionHandled = true;
            }
        }
    }
}
