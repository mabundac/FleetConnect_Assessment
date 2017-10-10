using System;
using System.Linq;

 namespace Assessment.Security.Helpers
{
    public static class SecurityHelper
    {
        public static bool HasPermission(string permission)
        {
            if (SessionHelpers.CURRENT_PRINCIPAL != null)
            {
                return SessionHelpers.CURRENT_PRINCIPAL.Profile.CheckPermission(permission);
            }
            else
            {
                return false;
            }
        }

        public static bool CanAccessRetailer(int retailerId)
        {
            if (SessionHelpers.CURRENT_PRINCIPAL != null)
            {
                return SessionHelpers.CURRENT_PRINCIPAL.Profile.CheckRetailerAccess(retailerId);
            }
            else
            {
                return false;
            }
        }
    }
}
