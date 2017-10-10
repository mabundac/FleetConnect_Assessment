using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

 namespace Assessment.Security.Helpers
{
    public class SessionHelpers
    {
        public static SecurityPrincipal CURRENT_PRINCIPAL
        {
            get
            {
                if (HttpContext.Current.Session["SECURITY_PRINCIPAL"] != null)
                {
                    return (SecurityPrincipal)HttpContext.Current.Session["SECURITY_PRINCIPAL"];
                }
                else
                {
                    return null;
                }
            }
        }
        public static long? LOGGED_IN_USER
        {
            get
            {
                if (HttpContext.Current.Session["LoggedInUser"] != null)
                {
                    return (long)HttpContext.Current.Session["LoggedInUser"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session.Add("LoggedInUser", value);
            }
        }
    }
}
       
