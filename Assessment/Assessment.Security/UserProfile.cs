using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Assessment.Security.Interfaces;
using System.Runtime.Serialization;
using System.Security;
using log4net;
using System.Reflection;

 namespace Assessment.Security
{
    [Serializable]  
    public class UserProfile : IUserProfile
    {
        #region Fields
        private object id;
        private string fullName;
        private List<string> permissions;
        private List<int> retailers;
        ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #endregion
        #region Constructors
        public UserProfile(object id, string fullName)
            : this(id, fullName, null, null)
        {
        }
        public UserProfile(object id, string fullName, IEnumerable<string> permissions, IEnumerable<int> retailers)
        {
            this.id = id;
            this.fullName = fullName;

            if (permissions != null)
            {
                this.permissions = new List<string>(permissions);
            }

            if (retailers != null)
            {
                this.retailers = new List<int>(retailers);
            }

        }
        protected UserProfile(SerializationInfo info, StreamingContext context)
        {
            this.id = info.GetValue("Id", typeof(object));
            this.fullName = info.GetString("FullName");
            this.permissions = info.GetValue("Permissions", typeof(List<string>)) as List<string>;
            this.retailers = info.GetValue("Retailers", typeof(List<int>)) as List<int>;
        }
        #endregion
        #region IProfile Members
        #region Properties
        public object Id
        {
            get
            {
                return this.id;
            }
        }
        public string FullName
        {
            get
            {
                return this.fullName;
            }
        }
        public virtual bool HasAuthorization
        {
            get
            {
                return ((this.permissions != null) && (this.permissions.Count > 0));
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// DEBUGGING : "testPass" causes the check to pass as though the permission has been allocated,  "testFail" causes the check to fails as though the permission has not been allocated.
        /// </summary>
        public virtual bool CheckPermission(string permission)
        {
            bool result = false;
            List<string> lowerCasePermissions = this.permissions.ConvertAll(i => i.ToLower());

            //log.Debug("Permission being checked : " + permission.ToLower());
            //log.Debug("Permissions list : " + string.Join(",", lowerCasePermissions));

#if DEBUG
    if (permission == "testPass") result = true;
    else if (permission == "testFail") result = false;
    else result = lowerCasePermissions.Contains(permission.ToLower());
    //TODO : Remove this 
    //result = true;
#else
    result = lowerCasePermissions.Contains(permission.ToLower());

#endif

            return result;
        }
        public virtual bool CheckRetailerAccess(int retailerId)
        {
            return this.retailers.Contains(retailerId);
        }
        #endregion

        #endregion
        #region ISerializable Members
        [SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", this.id);
            info.AddValue("FullName", this.fullName);
            info.AddValue("Permissions", this.permissions);
            info.AddValue("Retailers", this.retailers);
        }

        #endregion
    }
}