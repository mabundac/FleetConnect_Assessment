using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assessment.Models.Filters
{
    public class JobFilterArgument
    {
        public int? CompanyID { get; set; }
        public int? EmployeeID { get; set; }
        public string Keyword { get; set; }
        public int? NoOfYersOfExperience { get; set; }
    }
}