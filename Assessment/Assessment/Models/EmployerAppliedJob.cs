using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assessment.Models
{
    public class EmployerAppliedJob
    {
        public int JobID { get; set; }
        public int YearsOfExperience { get; set; }
        public byte[] Resume { get; set; }
        public string Name { get; set; }
        public int EmployeeID { get; set; }
        public int EmployeeJobID { get; set; }
        public string JobTitle { get; set; }
        public DateTime DateApplied { get; set; }
        public string Status { get; set; }
    }
}