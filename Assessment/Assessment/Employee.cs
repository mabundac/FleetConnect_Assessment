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
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.EmployeeJobs = new HashSet<EmployeeJob>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
        public string Keywords { get; set; }
        public byte[] Resume { get; set; }
        public int YearsOfExperience { get; set; }
        public int UserID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeJob> EmployeeJobs { get; set; }
    }
}
