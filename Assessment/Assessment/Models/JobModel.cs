using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assessment.Models
{
    public class JobModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int YearsOfExperience { get; set; }
        public string JobDescription { get; set; }
        public string Keywords { get; set; }
        public string Category { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public bool AlreadyApplied { get; set; }
        public int NoOfApplications { get; set; }
    }
}