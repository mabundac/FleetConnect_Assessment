using Elmah;
using System;
using System.Linq;
using System.Web;

 namespace Assessment.Filters
{
    public class ElmahErrorAttribute : System.Web.Mvc.HandleErrorAttribute
    {
        public override void OnException(System.Web.Mvc.ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            if (!filterContext.ExceptionHandled)
            {
                return;
            }

            var httpContext = filterContext.HttpContext.ApplicationInstance.Context;
            var signal = ErrorSignal.FromContext(httpContext);
            signal.Raise(filterContext.Exception, httpContext);
        }

        private static bool TryRaiseErrorSignal(System.Web.Mvc.ExceptionContext context)
        {
            var httpContext = GetHttpContext(context.HttpContext);
            if (httpContext == null)
            {
                return false;
            }

            var signal = ErrorSignal.FromContext(httpContext);
            if (signal == null)
            {
                return false;
            }

            signal.Raise(context.Exception, httpContext);
            return true;
        }

        private static bool IsFiltered(System.Web.Mvc.ExceptionContext context)
        {
            var config = context.HttpContext.GetSection("elmah/errorFilter") as ErrorFilterConfiguration;

            if (config == null)
            {
                return false;
            }

            var testContext = new ErrorFilterModule.AssertionHelperContext(
                                  context.Exception,
                                  GetHttpContext(context.HttpContext));
            return config.Assertion.Test(testContext);
        }

        private static HttpContext GetHttpContext(HttpContextBase context)
        {
            return context.ApplicationInstance.Context;
        }

        private static void LogException(System.Web.Mvc.ExceptionContext context)
        {
            var httpContext = GetHttpContextImpl(context.HttpContext);
            var error = new Error(context.Exception, httpContext);
            ErrorLog.GetDefault(httpContext).Log(error);
        }

        private static HttpContext GetHttpContextImpl(HttpContextBase context)
        {
            return context.ApplicationInstance.Context;
        }

    }
}