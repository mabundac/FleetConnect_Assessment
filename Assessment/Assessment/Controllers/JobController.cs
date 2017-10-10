using Assessment.Models;
using Assessment.Models.Filters;
using Assessment.Models.Repositories;
using Assessment.Security;
using Assessment.Security.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assessment.Controllers
{
    public class JobController : Controller
    {
        private readonly IAssessmentRepository _repo = new AssessmentRepository();
        // GET: Job

        [HasPermission(Permission = Permissions.Assessment.CAN_APPLY_FOR_A_JOB)]
        public ActionResult Index()
        {
          var  userId = (int)Security.Helpers.SessionHelpers.LOGGED_IN_USER.Value;

            var employee = _repo.GetEmployeeByUserID(userId);
            var employeeID = employee != null ? employee.ID : 0;

            var model = new CurrentEmployeeModel()
            {
                EmployeeID = employeeID,
                Keywords = employee != null ? employee.Keywords : null,
                NoOfYersOfExperience = employee != null ? employee.YearsOfExperience : 0
            };

            return View(model);
        }

        [HasPermission(Permission = Permissions.Assessment.CAN_SAVE_A_JOB)]
        public ActionResult SaveJobView(int? jobID)
        {
            var lJobID = jobID.HasValue ? jobID.Value : 0;
            var job = _repo.GetJobByID(lJobID);

            return View(job);
        }

        [HasPermission(Permission = Permissions.Assessment.CAN_VIEW_A_JOBS_AS_EMPLOYER)]
        public ActionResult EmployerJobsView()
        {
            return View();
        }

        [HasPermission(Permission = Permissions.Assessment.CAN_VIEW_A_JOBS_AS_EMPLOYER)]
        public ActionResult EmployerAppliedJobsView(int jobID)
        {
            var job = _repo.GetJobByID(jobID);

            return View(job);
        }

        [HasPermission(Permission = Permissions.Assessment.CAN_APPLY_FOR_A_JOB)]
        public ActionResult ViewJobDetailsView(int jobID)
        {
            var job = _repo.GetJobByID(jobID);

            return View(job);
        }   

        [HasPermission(Permission = Permissions.Assessment.CAN_SAVE_A_JOB)]
        public JsonResult SaveJob(JobModel job)
        {
            try
            {
                var userId = (int)Security.Helpers.SessionHelpers.LOGGED_IN_USER.Value;
                var company = _repo.GetCompanyByUserID(userId);
                var companyID = company != null ? company.ID : 0;
                job.CompanyID = companyID;

                var data = _repo.SaveJob(job);
                var Result = new
                {
                    Data = data
                };

                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(string.Format("There was an error when saving company's details: {0}", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetJobByID(int jobID)
        {
            var result = _repo.GetJobByID(jobID);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
     
        [HasPermission(Permission = Permissions.Assessment.CAN_APPLY_FOR_A_JOB)]
        public JsonResult SaveEmployeeJob(EmployeeJobModel employeeJob)
        {
            var userId = (int)Security.Helpers.SessionHelpers.LOGGED_IN_USER.Value;

            var employee = _repo.GetEmployeeByUserID(userId);
            var employeeID = employee != null ? employee.ID : 0;
            employeeJob.EmployeeID = employeeID;

            var result = _repo.SaveJobApplication(employeeJob);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HasPermission(Permission = Permissions.Assessment.CAN_APPLY_FOR_A_JOB)]
        public JsonResult GetJobs(JobFilterArgument filterArgs)
        {
            var result = _repo.GetJobs(filterArgs);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetJobList(JobFilterArgument filterArgs)
        {
            try
            {
                var userId = (int)Security.Helpers.SessionHelpers.LOGGED_IN_USER.Value;

                var employee = _repo.GetEmployeeByUserID(userId);
                var employeeID = employee != null ? employee.ID : 0; 
                var company = _repo.GetCompanyByUserID(userId);
                var companyID = company != null ? company.ID : 0;

                filterArgs.EmployeeID = employeeID;
                filterArgs.CompanyID = companyID;

                var data = _repo.GetJobList(filterArgs);
                var Result = new
                {
                    Data = data
                };

                var jsonResult = Json(Result, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                return Json(string.Format("There was an error when fetching data: {0}", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        [HasPermission(Permission = Permissions.Assessment.CAN_VIEW_A_JOBS_AS_EMPLOYER)]
        public JsonResult GetEmployerAppliedJobs(int jobID)
        {
            try
            {
                var data = _repo.GetEmployerAppliedJobs(jobID);
                var Result = new
                {
                    Data = data
                };

                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(string.Format("There was an error when fetching data: {0}", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        [HasPermission(Permission = Permissions.Assessment.CAN_SAVE_A_JOB)]
        public JsonResult SaveJobApplicationStatus(int employeeJobID, string newStatus)
        {
            try
            {
                var data = _repo.SaveJobApplicationStatus(employeeJobID, newStatus);
                var Result = new
                {
                    Data = data
                };

                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(string.Format("There was an error when saving company's details: {0}", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        [HasPermission(Permission = Permissions.Assessment.CAN_SAVE_A_JOB)]
        public ActionResult DownloadResume(int employeeJobID)
        {
            //Getting the byte array for a  job application Resume
            var data = _repo.GetJobApplicationByID(employeeJobID);

            string fileName = string.Format("{0}.pdf", Guid.NewGuid());
            string filepath = Server.MapPath("~/App_Data/Files/") + fileName;
            byte[] filedata = data.Resume;
            string contentType = MimeMapping.GetMimeMapping(filepath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = fileName,
                Inline = true,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(filedata, contentType);
        }
    }
}