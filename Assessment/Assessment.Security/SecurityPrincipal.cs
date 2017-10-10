using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Principal;
using System.Runtime.Serialization;
using Assessment.Security.Interfaces;
using System.Security;

 namespace Assessment.Security
{
    public class SecurityPrincipal : IPrincipal, ISerializable
    {   
        #region Fields
        private SecurityIdentity identity;
        private IList<string> roleList;
        private IUserProfile profile;
        #endregion
        #region Constructors
        public SecurityPrincipal(SecurityIdentity identity, string[] roles)
        {
            this.identity = identity;
            this.roleList = new List<string>(roles);
        }
        protected SecurityPrincipal(SerializationInfo info, StreamingContext context)
        {
            this.profile = info.GetValue("Profile", typeof(IUserProfile)) as IUserProfile;
            this.identity = info.GetValue("Identity", typeof(SecurityIdentity)) as SecurityIdentity;
            this.roleList = info.GetValue("Roles", typeof(IList<string>)) as IList<string>;
        }
        #endregion
        #region Properties
        public SecurityIdentity SecurityIdentity
        {
            get
            {
                return this.identity;
            }
        }
        public IUserProfile Profile
        {
            get
            {
                return this.profile;
            }
            internal set
            {
                this.profile = value;
            }
        }
        public bool HasProfile
        {
            get
            {
                return (this.profile != null);
            }
        }
        #endregion
        #region IPrincipal Members
        public IIdentity Identity
        {
            get
            {
                return this.identity;
            }
        }
        public bool IsInRole(string role)
        {
            return this.roleList.Contains(role);
        }
        #endregion
        #region ISerializable Members
        [SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Identity", this.identity, typeof(SecurityIdentity));
            info.AddValue("Profile", this.profile, typeof(IUserProfile));
            info.AddValue("Roles", this.roleList, typeof(IList<string>));
        }
        #endregion
    }
}