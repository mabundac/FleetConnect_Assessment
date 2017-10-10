using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assessment.Models
{
    public class CompanyModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
        public byte[] Logo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}