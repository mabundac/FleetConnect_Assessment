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
    
    public partial class SECURITY_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SECURITY_User()
        {
            this.SECURITY_UserRole = new HashSet<SECURITY_UserRole>();
        }
    
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public Nullable<bool> IsLocked { get; set; }
        public string Password { get; set; }
        public bool ResetPassword { get; set; }
        public System.DateTime DateAdded { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SECURITY_UserRole> SECURITY_UserRole { get; set; }
    }
}
