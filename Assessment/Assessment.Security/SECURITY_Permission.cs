//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Assessment.Security
{
    using System;
    using System.Collections.Generic;
    
    public partial class SECURITY_Permission
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SECURITY_Permission()
        {
            this.SECURITY_RolePermission = new HashSet<SECURITY_RolePermission>();
        }
    
        public int PermissionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<System.Guid> GUID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SECURITY_RolePermission> SECURITY_RolePermission { get; set; }
    }
}
