using System;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

 namespace Assessment.Security.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ValidateAntiForgeryTokenAllPosts : AuthorizeAttribute
    {
        public ValidateAntiForgeryTokenAllPosts()
            : base()
        {
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            //  Only validate POSTs that sre not attempting to log on or log off
            if (request.HttpMethod == System.Net.WebRequestMethods.Http.Post 
                    && !filterContext.ActionDescriptor.ActionName.Equals("Login") 
                    && !filterContext.ActionDescriptor.ActionName.Equals("LogOff"))
            {
                //  Ajax POSTs and normal form posts have to be treated differently when it comes to validating the AntiForgeryToken
                if (request.IsAjaxRequest())
                {
                    var antiForgeryCookie = request.Cookies[AntiForgeryConfig.CookieName];

                    var cookieValue = antiForgeryCookie != null
                        ? antiForgeryCookie.Value
                        : null;

                    //AntiForgery.Validate(cookieValue, request.Headers["__RequestVerificationToken"]);
                    try
                    {
                        AntiForgery.Validate(cookieValue, request.Headers["__RequestVerificationToken"]);
                    }
                    catch (HttpAntiForgeryException)
                    {
                        FormsAuthentication.SignOut();

                        string _port = "";

                        if (HttpContext.Current.Request.ServerVariables["SERVER_PORT"] != null
                                && HttpContext.Current.Request.ServerVariables["SERVER_PORT"].ToString() != "80"
                                && HttpContext.Current.Request.ServerVariables["SERVER_PORT"].ToString() != "443")
                        {
                            _port = String.Concat(":", HttpContext.Current.Request.ServerVariables["SERVER_PORT"].ToString());
                        }

                        Uri url = HttpContext.Current.Request.Url;
                        string redirecrUrl = url.Scheme + "://" + url.Authority + _port + HttpContext.Current.Request.ApplicationPath + "/LogOff";
                        filterContext.Result = new RedirectResult(redirecrUrl);
                    }

                }
                else
                {
                    new ValidateAntiForgeryTokenAttribute()
                        .OnAuthorization(filterContext);
                }
            }
        }
    }
}