using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Web.Mvc;

 namespace Assessment.Helpers
{
    public class SessionHelpers
    {
        public static long LOGGED_IN_USER_ID
        {
            get
            {
                if (HttpContext.Current.Session["LoggedInUser"] != null)
                {
                    return (long)HttpContext.Current.Session["LoggedInUser"];
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                HttpContext.Current.Session.Add("LoggedInUser", value);
            }
        }
        public static int CURRENT_SUBSCRIBER
        {
            get
            {
                if (HttpContext.Current.Session["CurrentSubscriber"] != null)
                {
                    return (int)HttpContext.Current.Session["CurrentSubscriber"];
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                HttpContext.Current.Session.Add("CurrentSubscriber", value);
            }
        }

        public static Guid CURRENT_MEDICAL_CENTER
        {
            get
            {
                if (HttpContext.Current.Session["CurrentMedicalCenter"] != null)
                {
                    return (Guid)HttpContext.Current.Session["CurrentMedicalCenter"];
                }
                else
                {
                    return Guid.Empty;
                }
            }
            set
            {
                HttpContext.Current.Session.Add("CurrentMedicalCenter", value);
            }
        }

        public static bool LOGGED_OUT
        {
            get
            {
                if (HttpContext.Current.Session["LoggedOut"] != null)
                {
                    return (bool)HttpContext.Current.Session["LoggedOut"];
                }
                else
                {
                    return false;
                }
            }
            set
            {
                HttpContext.Current.Session.Add("LoggedOut", value);
            }
        }
        public static string ELMAH_ERROR_ID
        {
            get
            {
                // return HttpContext.Current.Session["ElmahId"].ToString();
                return (HttpContext.Current.Session["ElmahId"] != null) ? HttpContext.Current.Session["ElmahId"].ToString() : "";
            }
            set
            {
                HttpContext.Current.Session["ElmahId"] = value;
            }

        }
     
    }
}