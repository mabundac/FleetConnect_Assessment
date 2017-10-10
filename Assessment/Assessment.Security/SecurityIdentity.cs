using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Principal;
using System.Runtime.Serialization;

namespace Assessment.Security
{
    public class SecurityIdentity : IIdentity, ISerializable
    {
        #region Fields
        private long id;
        private string name;
        private string authenticationType;
        #endregion
        #region Properties
        public long Id
        {
            get
            {
                return this.id;
            }
        }
        #endregion
        #region Constructors
        public SecurityIdentity(long id, string name)
            : this(id, name, string.Empty)
        {
        }

        public SecurityIdentity(long id, string name, string authenticationType)
        {
            this.id = id;
            this.name = name;
            this.authenticationType = authenticationType;
        }

        public SecurityIdentity(long id, WindowsIdentity identity)
            : this(id, identity.Name, string.Empty)
        {
        }

        public SecurityIdentity(long id, WindowsIdentity identity, string authenticationType)
            : this(id, identity.Name, authenticationType)
        {
        }

        protected SecurityIdentity(SerializationInfo info, StreamingContext context)
        {
            this.id = info.GetInt64("Id");
            this.name = info.GetString("Name");
            this.authenticationType = info.GetString("AuthenticationType");
        }

        #endregion
        #region IIdentity Members
        public string AuthenticationType
        {
            get
            {
                return this.authenticationType;
            }
        }
        public bool IsAuthenticated
        {
            get
            {
                return ((this.name != null) && (this.name != string.Empty));
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }
        }
        #endregion
        #region ISerializable Members
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", this.id);
            info.AddValue("Name", this.name);
            info.AddValue("AuthenticationType", this.authenticationType);
        }
        #endregion
    }
}