using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

 namespace Assessment.Security.Filters
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// DEBUGGING : "testPass" causes the check to pass as though the permission has been allocated,  "testFail" causes the check to fails as though the permission has not been allocated.
        /// </summary>
        public string Permission;
        private bool permissionCheck = false;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool result = Assessment.Security.Helpers.SecurityHelper.HasPermission(Permission);

            if (!result)
            {
                this.permissionCheck = true;
            }
            return result;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            if (this.permissionCheck)
            {
                filterContext.Result = new RedirectResult("~/Home/PermissionDenied");
            }
            else
            {
                filterContext.Result = new RedirectResult("/");
                base.HandleUnauthorizedRequest(filterContext);
            }

        }
    }
}
