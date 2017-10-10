using System;
using System.Linq;
using System.Web;

using Assessment.Security.Interfaces;

 namespace Assessment.Security
{
    public class SessionContextStore : IContextStoreProvider
    {
        #region Constructors
        public SessionContextStore()
        {
            if (HttpContext.Current == null)
            {
                throw new NotSupportedException("Store only supports web applications.");
            }
        }

        #endregion

        #region IContextStoreProvider Members
        public bool HasPrincipal
        {
            get
            {
                return ((HttpContext.Current.Session != null) && (HttpContext.Current.Session["SECURITY_PRINCIPAL"] != null));
            }
        }
        public SecurityPrincipal GetPrincipal()
        {
            if (HttpContext.Current.Session != null)
            {
                return HttpContext.Current.Session["SECURITY_PRINCIPAL"] as SecurityPrincipal;
            }

            return null;
        }
        public void SetPrincipal(SecurityPrincipal principal)
        {
            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session["SECURITY_PRINCIPAL"] = principal;
            }
        }
        public void ClearPrincipal()
        {
            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Remove("SECURITY_PRINCIPAL");
            }
        }
        public void SetData(string key, object data)
        {
            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session[key] = data;
            }
        }
        public object GetData(string key)
        {
            if (HttpContext.Current.Session != null)
            {
                return HttpContext.Current.Session[key];
            }

            return null;
        }
        public void ClearData(string key)
        {
            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Remove(key);
            }
        }
        public bool ContainsData(string key)
        {
            if (HttpContext.Current.Session != null)
            {
                return (HttpContext.Current.Session[key] != null);
            }

            return false;
        }
        #endregion
    }
}