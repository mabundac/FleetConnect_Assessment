using Assessment.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assessment.Models
{
    public static class MapperExtension
    {
       public static UserModel Map(this SECURITY_User original)
        {
            if (original == null)
            {
                return null;
            }

            var model = new UserModel()
            {
                ID = original.UserId,
                LastLogin = original.LastLogin,
                ResetPassword = original.ResetPassword,
                EmailAddress = original.EmailAddress,
                FirstName = original.FirstName,
                LastName = original.LastName,
                Password = EncryptionHelper.EncryptData(original.Password)
            };

            return model;
        }  
       public static List<UserModel> Map(this List<SECURITY_User> original)
       {
           if (original == null)
           {
               return null;
           }

           var list = original.Select(x => x.Map()).ToList();
           return list;
       }     
       public static CompanyModel Map(this Company original)
        {
            if (original == null)
            {
                return null;
            }

            var model = new CompanyModel()
            {
               ID = original.ID,
               Address = original.Address,
               Category = original.Category,
               Logo = original.Logo,
               Name = original.Name
            };

            return model;
        }    
       public static List<CompanyModel> Map(this List<Company> original)
        {
            if (original == null)
            {
                return null;
            }

            var list = original.Select(x => x.Map()).ToList();
            return list;
        }     
       public static EmployeeModel Map(this Employee original)
        {
            if (original == null)
            {
                return null;
            }

            var model = new EmployeeModel()
            {
                ID = original.ID,
                Address = original.Address,
                Category = original.Category,
                Name = original.Name,
                Keywords = original.Keywords,
                Resume = original.Resume,
                UserID = original.UserID,
                YearsOfExperience = original.YearsOfExperience 
            };

            return model;
        }    
       public static List<EmployeeModel> Map(this List<Employee> original)
        {
            if (original == null)
            {
                return null;
            }

            var list = original.Select(x => x.Map()).ToList();
            return list;
        }                      
       public static JobModel Map(this Job original)
        {
            if (original == null)
            {
                return null;
            }

            var model = new JobModel()
            {
                ID = original.ID,
                Category = original.Category,
                CompanyID = original.CompanyID,
                JobDescription = original.JobDescription,
                Keywords = original.Keywords,
                Title = original.Title,
                YearsOfExperience = original.YearsOfExperience,
                CompanyName = original.Company.Name,
                AlreadyApplied = false
            };

            return model;
        }    
       public static List<JobModel> Map(this List<Job> original)
        {
            if (original == null)
            {
                return null;
            }

            var list = original.Select(x => x.Map()).ToList();
            return list;
        }

        public static EmployeeJobModel Map(this EmployeeJob original)
        {
            if (original == null)
            {
                return null;
            }

            var model = new EmployeeJobModel()
            {
                ID = original.ID,
                DateLogged = original.DateLogged,
                EmployeeID = original.EmployeeID,
                JobID = original.JobID,
                Resume = original.Resume,
                Status = original.Status
            };

            return model;
        }
        public static List<EmployeeJobModel> Map(this List<EmployeeJob> original)
        {
            if (original == null)
            {
                return null;
            }

            var list = original.Select(x => x.Map()).ToList();
            return list;
        }

        public static JobListModel Map(this vw_Jobs original)
        {
            if (original == null)
            {
                return null;
            }

            var model = new JobListModel()
            {
                JobID = original.JobID,
                Category = original.Category,
                CompanyID = original.CompanyID,
                JobDescription = original.JobDescription,
                Keywords = original.Keywords,
                JobTitle = original.Title,
                YearsOfExperience = original.YearsOfExperience,
                DateLogged = original.DateLogged,
                EmployeeID = original.EmployeeID,
                EmployeeJobID = original.EmployeeJobID,
                Resume = original.Resume,
                NoOfApplications = original.NoOfApplications.HasValue ? original.NoOfApplications.Value : 0
            };

            return model;
        }

        public static List<JobListModel> Map(this List<vw_Jobs> original)
        {
            if (original == null)
            {
                return null;
            }

            var list = original.Select(x => x.Map()).ToList();
            return list;
        }

        public static EmployerAppliedJob Map(this vw_AppliedJobs original)
        {
            if (original == null)
            {
                return null;
            }

            var model = new EmployerAppliedJob()
            {
                JobID = original.JobID,
                JobTitle = original.Title,
                YearsOfExperience = original.YearsOfExperience,
                EmployeeID = original.EmployeeID,
                //Resume = original.Resume,
                Name = original.Name,
                DateApplied = original.DateLogged,
                EmployeeJobID = original.EmployeeJobID,
                Status = original.Status
            };

            return model;
        }

        public static List<EmployerAppliedJob> Map(this List<vw_AppliedJobs> original)
        {
            if (original == null)
            {
                return null;
            }

            var list = original.Select(x => x.Map()).ToList();
            return list;
        }
    }
}