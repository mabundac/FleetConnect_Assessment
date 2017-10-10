using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assessment.Models
{
    public class EmployeeModel
    {
        //userName: viewModel.userName(),
        //    password: viewModel.password(),
        //    firstName: viewModel.firstName(),
        //    surname: viewModel.surname(),
        //    address: viewModel.address(),
        //    resume: viewModel.resume(),
        //    keywords: viewModel.keywords(),
        //    category: viewModel.selectedCategory(),
        //    yearsOfExperience: 

        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
        public string Keywords { get; set; }
        public byte[] Resume { get; set; }
        public int YearsOfExperience { get; set; }
        public int UserID { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

    }
}