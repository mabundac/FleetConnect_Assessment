using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assessment.Models
{
    public class JobListModel
    {
        public int JobID  {get;set;}
        public string JobTitle  {get;set;}
        public int YearsOfExperience {get;set;}
        public string JobDescription { get;set;}
        public string Keywords { get;set;}
        public string Category { get;set;}
        public int? CompanyID { get;set;}
        public int? EmployeeJobID { get; set; }
        public int? EmployeeID { get;set;}
        public byte[] Resume { get;set;}
        public DateTime? DateLogged { get;set;}
        public int NoOfApplications  {get;set; }
    }
}