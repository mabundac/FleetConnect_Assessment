using Assessment.Models.Filters;
using Assessment.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Assessment.Models.Repositories
{
    public class AssessmentRepository : IAssessmentRepository
    {
        #region User

        public UserModel SaveUser(UserModel user)
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                if (user.ID <= 0)
                {
                    var newUser = new SECURITY_User()
                    {
                       DateAdded = DateTime.Now,
                       EmailAddress = user.EmailAddress,
                       FirstName = user.FirstName,
                       IsLocked = false,
                       LastName = user.LastName,
                       Password = EncryptionHelper.EncryptData(user.Password)
                    };

                    context.SECURITY_User.Add(newUser);
                    context.SaveChanges();

                    var role = context.SECURITY_Role.FirstOrDefault(x=>x.Name.ToLower() == user.Roles.ToLower());
                    var roleID = role != null ? role.RoleId : 0;

                    var userRole = new SECURITY_UserRole()
                    {
                       UserId = newUser.UserId,
                       RoleId = roleID
                    };

                    context.SECURITY_UserRole.Add(userRole);

                    context.SaveChanges();

                    return newUser.Map();
                }
                else
                {
                    var userToUpdate =
                        context.SECURITY_User.FirstOrDefault(x => x.UserId == user.ID);

                    if (userToUpdate == null)
                        return null;

                    userToUpdate.EmailAddress = user.EmailAddress;
                    userToUpdate.FirstName = user.FirstName;
                    userToUpdate.LastName = user.LastName;
                    userToUpdate.IsLocked = user.isLocked;
                    userToUpdate.Password = EncryptionHelper.EncryptData(user.Password);                    

                    context.SaveChanges();

                    var role = context.SECURITY_Role.FirstOrDefault(x => x.Name.ToLower() == user.Roles.ToLower());
                    var roleID = role != null ? role.RoleId : 0;
                    var userRole = new SECURITY_UserRole()
                    {
                        UserId = userToUpdate.UserId,
                        RoleId = roleID
                    };

                    context.SECURITY_UserRole.Add(userRole);

                    context.SaveChanges();


                    return userToUpdate.Map();
                }
            } 
        }
        public UserModel GetUserByID(int userID)
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                var user = context.SECURITY_User.FirstOrDefault(x => x.UserId == userID);
                var mappedUser = user.Map();

                return mappedUser;
            }
        }

        //public UserModel GetUserByIdentityID(Guid userID)
        //{
        //    using (var context = new FleetConnectAssessmentEntities())
        //    {
        //        var user = context.SECURITY_User.FirstOrDefault(x => x.IdentityUserID == userID);
        //        var mappedUser = user.Map();

        //        return mappedUser;
        //    }
        //}

        #endregion

        #region Company

        public CompanyModel SaveCompany(CompanyModel company, int userID)
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                if (company.ID <= 0)
                {
                    var newCompany = new Company()
                    {
                        Address = company.Address,
                        Category = company.Category,
                        Logo = company.Logo,
                        Name = company.Name
                    };

                    context.Companies.Add(newCompany);
                    context.SaveChanges();

                    var companyUser = new CompanyUser()
                    {
                        UserID = userID,
                        CompanyID = newCompany.ID
                    };

                    context.CompanyUsers.Add(companyUser);
                    context.SaveChanges();

                    return newCompany.Map();
                }
                else
                {
                    var companyToUpdate =
                        context.Companies.FirstOrDefault(x => x.ID == company.ID);

                    if (companyToUpdate == null)
                        return null;

                    companyToUpdate.Address = company.Address;
                    companyToUpdate.Category = company.Category;
                    companyToUpdate.Logo = company.Logo;
                    companyToUpdate.Name = company.Name;

                    context.SaveChanges();

                    return companyToUpdate.Map();
                }
            }
        }  
        public List<CompanyModel> GetCompanies()
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                var companies = context.Companies.OrderBy(x=>x.Name).ToList();
                var mappedCompanies = companies.Map();

                return mappedCompanies;
            }
        }  
        public CompanyModel GetCompanyByID(int companyID)
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                var company = context.Companies.FirstOrDefault(x => x.ID == companyID);
                var mappedCompany = company.Map();

                return mappedCompany;
            }
        }

        public CompanyModel GetCompanyByUserID(int userID)
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                var userCompany = context.CompanyUsers.FirstOrDefault(x => x.UserID == userID);
                var company = userCompany != null ? userCompany.Company : null;
                var mappedCompany = company.Map();

                return mappedCompany;
            }
        }

        #endregion

        #region Employee

        public EmployeeModel SaveEmployee(EmployeeModel employee, int userId)
        {
            var ValidModel = false;
            if (!string.IsNullOrEmpty(employee.Keywords) && !string.IsNullOrEmpty(employee.Name))
                ValidModel = true;

            if (ValidModel)
            {
                using (var context = new FleetConnectAssessmentEntities())
                {
                    if (employee.ID <= 0)
                    {
                        //var newUser = new User()
                        //{
                        //    DisplayName = employee.Name,                          
                        //    IdentityUserID = userId 
                        //};

                        //context.Users.Add(newUser);
                        //context.SaveChanges();

                        var newEmployee = new Employee()
                        {
                            Address = employee.Address,
                            Category = employee.Category,
                            Name = employee.Name,
                            Keywords = employee.Keywords,
                            Resume = employee.Resume,
                            UserID = userId,
                            YearsOfExperience = employee.YearsOfExperience
                        };

                        context.Employees.Add(newEmployee);
                        context.SaveChanges();

                        return newEmployee.Map();
                    }
                    else
                    {
                        var employeeToUpdate =
                            context.Employees.FirstOrDefault(x => x.ID == employee.ID);

                        if (employeeToUpdate == null)
                            return null;

                        employeeToUpdate.Address = employee.Address;
                        employeeToUpdate.Category = employee.Category;
                        employeeToUpdate.Name = employee.Name;
                        employeeToUpdate.Keywords = employee.Keywords;
                        employeeToUpdate.Resume = employee.Resume != null ? employee.Resume : employeeToUpdate.Resume;
                        employeeToUpdate.UserID = employee.UserID;
                        employeeToUpdate.YearsOfExperience = employee.YearsOfExperience;

                        context.SaveChanges();

                        return employeeToUpdate.Map();
                    }
                }
            }

            return null;
        }
        public EmployeeModel GetEmployeeByID(int employeeID)
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                var employee = context.Employees.FirstOrDefault(x => x.ID == employeeID);
                var mappedEmployee = employee.Map();

                return mappedEmployee;
            }
        }

        public EmployeeModel GetEmployeeByUserID(int userID)
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                var employee = context.Employees.FirstOrDefault(e => e.UserID == userID);

                var mappedEmployee = employee.Map();

                return mappedEmployee;
            }
        }

        public List<EmployeeModel> GetEmployees()
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                var employees = context.Employees.OrderBy(x=>x.Name).ToList();
                var mappedEmployee = employees.Map();

                return mappedEmployee;
            }
        }

        #endregion

        #region Job

        public List<JobModel> GetEmployeeJobs(int employeeID)
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                var jobs = (from job in context.Jobs
                           join employeeJob in context.EmployeeJobs on job.ID equals employeeJob.JobID
                           where employeeJob.EmployeeID == employeeID
                           select job).ToList();

                var mappedJobs = jobs.Map();

                return mappedJobs;
            }
        }

        /// <summary>
        /// Returns a job matching the jobID parameter
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>      
        public JobModel GetJobByID(int jobID)
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                var job = context.Jobs.FirstOrDefault(x => x.ID == jobID);

                var mappedJob = job.Map();

                return mappedJob;
            }
        }

       /// <summary>
       /// Gets a list of jobs by company Id
       /// </summary>
       /// <param name="companyID"></param>
       /// <returns></returns>
        public List<JobModel> GetJobsByCompanyID(int companyID)
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                var jobs = (from item in context.Jobs
                            where item.CompanyID == companyID || companyID == 0
                            orderby item.Title
                            select item).ToList();

                var mappedJobs = jobs.Map();

                return mappedJobs;
            }
        }

        /// <summary>
        /// Saves a new job entry and is accessible to employer
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public JobModel SaveJob(JobModel job)
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                if (job.ID <= 0)
                {
                    var newJob = new Job()
                    {
                        Category = job.Category,
                        CompanyID = job.CompanyID,
                        JobDescription = job.JobDescription,
                        Keywords = job.Keywords,
                        Title = job.Title,
                        YearsOfExperience = job.YearsOfExperience
                    };

                    context.Jobs.Add(newJob);
                    context.SaveChanges();

                    return newJob.Map();
                }
                else
                {
                    var jobToUpdate =
                        context.Jobs.FirstOrDefault(x => x.ID == job.ID);

                    if (jobToUpdate == null)
                        return null;

                    jobToUpdate.Category = job.Category;
                    jobToUpdate.CompanyID = job.CompanyID;
                    jobToUpdate.JobDescription = job.JobDescription;
                    jobToUpdate.Keywords = job.Keywords;
                    jobToUpdate.Title = job.Title;
                    jobToUpdate.YearsOfExperience = job.YearsOfExperience;

                    context.SaveChanges();

                    return jobToUpdate.Map();
                }
            }
        }

        /// <summary>
        /// Saves the job application when the job seeker/ employee clicks Apply button
        /// </summary>
        /// <param name="employeeJob"></param>
        /// <returns></returns>

        public EmployeeJobModel SaveJobApplication(EmployeeJobModel employeeJob)
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                if (employeeJob.ID <= 0)
                {
                    var employee = context.Employees.FirstOrDefault(x => x.ID == employeeJob.EmployeeID);

                    var newJobApplication = new EmployeeJob()
                    {
                        EmployeeID = employeeJob.EmployeeID,
                        JobID = employeeJob.JobID,
                        Resume = employee.Resume,
                        DateLogged = DateTime.Now,
                        Status = "New"
                    };

                    context.EmployeeJobs.Add(newJobApplication);
                    context.SaveChanges();

                    return newJobApplication.Map();
                }
                else
                {
                    var employeeJobToUpdate =
                         context.EmployeeJobs.FirstOrDefault(x => x.ID == employeeJob.ID);

                    if (employeeJobToUpdate == null)
                        return null;

                    employeeJobToUpdate.EmployeeID = employeeJob.EmployeeID;
                    employeeJobToUpdate.JobID = employeeJob.JobID;
                    employeeJobToUpdate.Resume = employeeJob.Resume;

                    context.SaveChanges();

                    return employeeJobToUpdate.Map();
                }
            }
        }

        /// <summary>
        /// Gets a job application for a job seeker
        /// </summary>
        /// <param name="employeeJobID"></param>
        /// <returns></returns>
        public EmployeeJobModel GetJobApplicationByID(int employeeJobID)
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                var employeeJob = context.EmployeeJobs.FirstOrDefault(x=>x.ID == employeeJobID);

                var mappedEmployeeJob = employeeJob.Map();

                return mappedEmployeeJob;
            }
        }

        /// <summary>
        ///  Gets list of jobs using filters supplied. A list view by a job seeker/ employee
        /// </summary>
        /// <param name="filterArgs"></param>
        /// <returns></returns>
        public List<JobModel> GetJobs(JobFilterArgument filterArgs)
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                var employeeID = filterArgs.EmployeeID;

                var mappedJobs = (from job in context.Jobs
                            join empJob in context.EmployeeJobs on job.ID equals empJob.JobID into queryResult
                            where (job.CompanyID == filterArgs.CompanyID || (filterArgs.CompanyID <= 0 || filterArgs.CompanyID == null || filterArgs.CompanyID == 0))
                            where (job.YearsOfExperience <= filterArgs.NoOfYersOfExperience || filterArgs.NoOfYersOfExperience == null)
                            from item in queryResult.DefaultIfEmpty()
                            select new  JobModel()
                            {
                                AlreadyApplied = (item != null && item.EmployeeID == employeeID ? true : false),
                                ID = job.ID,
                                Category = job.Category,
                                CompanyID = job.CompanyID,
                                JobDescription = job.JobDescription,
                                Keywords = job.Keywords,
                                Title = job.Title,
                                YearsOfExperience = job.YearsOfExperience,
                                CompanyName = job.Company.Name
                            }).ToList();

                if (!string.IsNullOrEmpty(filterArgs.Keyword))
                {
                    mappedJobs = filterJobsByKeywords(mappedJobs, filterArgs.Keyword);
                }

                return mappedJobs;
            }
        }

        /// <summary>
        /// Returns the number of applications for a job
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        private int GetNoOfApplicationsOfJob(int jobID)
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                var noOfApplications = context.EmployeeJobs.Where(x => x.JobID == jobID).Count();

                return noOfApplications;
            }
        }
        
        /// <summary>
        ///  Gets list of jobs using filters supplied. This is for the job list viewd by an employer
        /// </summary>
        /// <param name="filterArgs"></param>
        /// <returns></returns>
                                                    
        public List<JobListModel> GetJobList(JobFilterArgument filterArgs)
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                var employeeID = filterArgs.EmployeeID;

                var jobs = (from job in context.vw_Jobs
                            where (job.CompanyID == filterArgs.CompanyID || (filterArgs.CompanyID <= 0 || filterArgs.CompanyID == null || filterArgs.CompanyID == 0))
                            where (job.YearsOfExperience <= filterArgs.NoOfYersOfExperience || filterArgs.NoOfYersOfExperience == null)
                            orderby job.Title
                            select job).ToList();

               var mappedJobs = jobs.Map();

                return mappedJobs;
            }

        }

        /// <summary>
        ///     This method gets the job applications for a specific job
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        public List<EmployerAppliedJob> GetEmployerAppliedJobs(int jobID)
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                var jobs = (from job in context.vw_AppliedJobs
                            where (job.JobID == jobID)
                            orderby job.Title
                            select job).ToList();

                var mappedJobs = jobs.Map();

                return mappedJobs;
            }
        }


        /// <summary>
        /// The method responsible for saving jab status. Accepted or Rejected
        /// </summary>
        /// <param name="employeeJobID"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        public EmployeeJobModel SaveJobApplicationStatus(int employeeJobID, string newStatus)
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                var job = context.EmployeeJobs.FirstOrDefault(x => x.ID == employeeJobID);
                job.Status = newStatus;

                context.SaveChanges();

                var mappedJob = job.Map();
                SendEmail(employeeJobID);

                return mappedJob;
            }
        }

        /// <summary>
        /// Gets the list of jobs that are filtered by keywords
        /// </summary>
        /// <param name="jobs"></param>
        /// <param name="filterKeywords"></param>
        /// <returns></returns>
        private List<JobModel> filterJobsByKeywords(List<JobModel> jobs,string filterKeywords)
        {
            var result =  new List<JobModel>();

            var filterKeywordsArray = filterKeywords.Split(' ');
            foreach (var job in jobs)
            {
                foreach (var keyword in filterKeywordsArray)
                {
                    if ((job.Keywords.ToLower().Contains(keyword.ToLower()) || job.Keywords.ToLower().Contains(keyword.ToLower())) && (!string.IsNullOrEmpty(keyword.ToLower()) && keyword.Length > 1))
                    {
                        result.Add(job);
                        break;
                    }    
                }
            }

            return result;
        }

        /// <summary>
        /// Sends the email to the applicant when the job application status changes
        /// </summary>
        /// <param name="employeeJobID"></param>
        private void SendEmail(int employeeJobID)
        {
            using (var context = new FleetConnectAssessmentEntities())
            {
                var jobApplication = context.vw_EmployeeEmailFromJobApplication.FirstOrDefault(x => x.EmployeeJobID == employeeJobID);

                if(jobApplication != null)
                {
                   var email = jobApplication.Email;
                    var name = jobApplication.Name;
                    var jobTitle = jobApplication.JobTitle;
                    var jobStatus = jobApplication.JobStatus;


                    var fromAddress = new MailAddress("assessment@gmail.com", "From Fleet Connect Recruitment");
                    var toAddress = new MailAddress(email, name);
                    const string fromPassword = "Password1";
                    const string subject = "Job Application Status";
                    var body = string.Format("Your application for a job {0} has been {1}", jobTitle,jobStatus);

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                }
            }    
        }

        #endregion

    }
}