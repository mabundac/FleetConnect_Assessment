//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Assessment
{
    using System;
    using System.Collections.Generic;
    
    public partial class SECURITY_RolePermission
    {
        public int RolePermissionId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
    
        public virtual SECURITY_Permission SECURITY_Permission { get; set; }
        public virtual SECURITY_Role SECURITY_Role { get; set; }
    }
}
