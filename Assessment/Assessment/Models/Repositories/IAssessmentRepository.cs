using Assessment.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assessment.Models.Repositories
{
    public interface IAssessmentRepository
    {
        #region User

        UserModel SaveUser(UserModel user);
        UserModel GetUserByID(int userID);
        //UserModel GetUserByIdentityID(Guid userID);

        #endregion

        #region Company

        CompanyModel SaveCompany(CompanyModel company, int userID);
        CompanyModel GetCompanyByID(int companyID);
        CompanyModel GetCompanyByUserID(int userID);
        List<CompanyModel> GetCompanies();

        #endregion

        #region Employee

        EmployeeModel SaveEmployee(EmployeeModel employee, int userId);
        EmployeeModel GetEmployeeByID(int employeeID);
        EmployeeModel GetEmployeeByUserID(int userID);
        List<EmployeeModel> GetEmployees();

        #endregion

        #region Job

        JobModel SaveJob(JobModel job);
        JobModel GetJobByID(int jobID);
        List<JobModel> GetJobsByCompanyID(int companyID);
        EmployeeJobModel SaveJobApplication(EmployeeJobModel employeeJob);
        EmployeeJobModel GetJobApplicationByID(int employeeJobID);
        List<JobModel> GetEmployeeJobs(int employeeID);
        List<JobModel> GetJobs(JobFilterArgument filterArgs);
        List<JobListModel> GetJobList(JobFilterArgument filterArgs);
        List<EmployerAppliedJob> GetEmployerAppliedJobs(int jobID);
        EmployeeJobModel SaveJobApplicationStatus(int employeeJobID, string newStatus);

        #endregion
    }
}