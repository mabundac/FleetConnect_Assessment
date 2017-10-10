using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

 namespace Assessment.Helpers
{
    public class SettingsHelper
    {
        public static string APPLICATION_NAME = ConfigurationManager.AppSettings["applicationName"].ToString();
        public static string APPLICATION_PATH
        {
            get
            {
                string APP_PATH = System.Web.HttpContext.Current.Request.ApplicationPath.ToLower();
                if (APP_PATH == "/")
                {      //a site
                    APP_PATH = "/";
                }
                else if (!APP_PATH.EndsWith(@"/"))
                { //a virtual
                    APP_PATH += @"/";
                }

                return APP_PATH;
            }
        }
        public static string FULLY_QUALIFIED_APPLICATION_PATH
        {
            get
            {
                //Return variable declaration
                var appPath = string.Empty;

                //Getting the current context of HTTP request
                var context = System.Web.HttpContext.Current;

                //Checking the current context content
                if (context != null)
                {
                    //Formatting the fully qualified website url/name
                    appPath = string.Format("{0}://{1}{2}{3}",
                                            context.Request.Url.Scheme,
                                            context.Request.Url.Host,
                                            context.Request.Url.Port == 80
                                                ? string.Empty
                                                : ":" + context.Request.Url.Port,
                                            context.Request.ApplicationPath);
                }

                if (!appPath.EndsWith("/"))
                    appPath += "/";

                return appPath;
            }
        }
        public static int GRID_PAGE_SIZE = int.Parse(ConfigurationManager.AppSettings["gridPageSize"]);
        public static bool LOG_ACTION_REQUESTS = bool.Parse(ConfigurationManager.AppSettings["logActionRequests"].ToString());
        public static string REPORTS_FOLDER = ConfigurationManager.AppSettings["reportsFolder"].ToString();
    }

    public enum ErrorCodes
    {
        PermissionDenied = 1
    }
}