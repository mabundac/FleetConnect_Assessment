using System;
using System.Collections.Generic;
using System.Linq;
using Assessment.Security.Interfaces;
using log4net;
using System.Reflection;

 namespace Assessment.Security
{
    public class SecurityProvider : IAuthenticationProvider, IAuthorizationProvider
    {
        ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region IAuthenticationProvider members
        public Security.SecurityPrincipal Authenticate(System.Net.NetworkCredential credential)
        {
            #region Variables
            SecurityPrincipal principal = null;
            #endregion

            using (var db = new FleetConnectEntities())
            {
                try
                {
                    #region Get user object with credentials supplied
                    string encryptedPassword = EncryptionHelper.EncryptData(credential.Password);

                    var roles = from item in db.SECURITY_Role select item;

                    //Get the user by email address
                    SECURITY_User usr = db.SECURITY_User.FirstOrDefault(u => u.EmailAddress == credential.UserName && u.Password == encryptedPassword);
                    #endregion
                    #region Verify user

                    if (usr == null)
                    {
                        log.Debug("Invalid user credentials supplied");
                    }
                    else
                    {
                        log.Info("Log in credentials validated, creating security principal for " + usr.EmailAddress);
                        principal = new SecurityPrincipal(new SecurityIdentity(usr.UserId, usr.FirstName + " " + usr.LastName), new string[0]);
                        Helpers.SessionHelpers.LOGGED_IN_USER = usr.UserId;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    log.Error("Error authenticating user", ex);
                }
            }
            return principal;
        }
        #endregion

        #region IAuthorizationProvider
        public IUserProfile Authorize(Security.SecurityPrincipal principal)
        {
            IUserProfile profile = null;
            using (var db = new FleetConnectEntities())
            {
                #region Variables
                List<string> permissions = new List<string>();
                List<int> retailers = new List<int>();

                SECURITY_User usr = db.SECURITY_User.Find(Helpers.SessionHelpers.LOGGED_IN_USER); //This is set in Authenticate(System.Net.NetworkCredential credential) above
                #endregion
                #region Get permissions
                log.Info("Detected user : " + usr.EmailAddress);
                var permissionsResult = db.SECURITY_GetUserPermissions(usr.UserId);

                foreach (var item in permissionsResult.ToList())
                {
                    log.Info("Granting " + usr.EmailAddress + " permission : " + item.Name);
                    if (!permissions.Contains(item.GUID.ToString().ToUpper()))
                    {
                        permissions.Add(item.GUID.ToString().ToUpper());
                    }
                    else
                    {
                        log.Info("Permission list already contains: " + item.Name);
                    }
                }

                #region Create the UserProfile
                profile = new Assessment.Security.UserProfile(usr.UserId, usr.FirstName + " " + usr.LastName, permissions, retailers);
                #endregion
                #endregion
            }
            return profile;
        }
        #endregion
    }
}