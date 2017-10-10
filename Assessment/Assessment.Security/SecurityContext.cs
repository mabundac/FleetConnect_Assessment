using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net;
using Assessment.Security.Interfaces;
using System.Security;

 namespace Assessment.Security
{
    public static class SecurityContext
    {
        #region Fields
        private static IAuthorizationProvider authorizationProvider;
        private static IAuthenticationProvider authenticationProvider;
        private static IContextStoreProvider contextStoreProvider;
        private static bool setThreadPrincipal;
        #endregion

        #region Constructors
        static SecurityContext()
        {
            try
            {
                SecurityProvider securityProvider = new SecurityProvider();
                authorizationProvider = securityProvider;
                authenticationProvider = securityProvider;

                contextStoreProvider = new Assessment.Security.SessionContextStore();
                //setThreadPrincipal = configSection.Security.Context.SetThreadPrincipal;
                setThreadPrincipal = true;
            }
            catch (Exception ex)
            {
                throw new SecurityException("Error creating providers from configuration file.", ex);
            }
        }
        #endregion

        #region Properties
        public static bool IsValid
        {
            get
            {
                return (contextStoreProvider.HasPrincipal);
            }
        }
        public static SecurityPrincipal CurrentUser
        {
            get
            {
                return contextStoreProvider.GetPrincipal();
            }
        }
        #endregion

        #region Methods
        public static void LogIn(NetworkCredential credential)
        {
            SecurityPrincipal principal = authenticationProvider.Authenticate(credential);

            if (principal == null || (!(principal.SecurityIdentity.IsAuthenticated)))
            {
                throw new SecurityException("User, " + credential.UserName + ", could not be authenticated.");
            }

            if (setThreadPrincipal)
            {
                System.Threading.Thread.CurrentPrincipal = principal;
            }

            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }

            principal.Profile = authorizationProvider.Authorize(principal);
            contextStoreProvider.SetPrincipal(principal);
        }
        public static void LogOut()
        {
            if (contextStoreProvider.HasPrincipal)
            {
                contextStoreProvider.ClearPrincipal();
            }
        }
        #endregion

    }
}