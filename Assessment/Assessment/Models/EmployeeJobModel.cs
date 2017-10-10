using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assessment.Models
{
    public class EmployeeJobModel
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int JobID { get; set; }
        public byte[] Resume { get; set; }
        public System.DateTime DateLogged { get; set; }
        public string Status { get; set; }
    }
}