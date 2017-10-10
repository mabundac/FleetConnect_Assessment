using Assessment.Models;
using Assessment.Models.Repositories;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assessment.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IAssessmentRepository _repo = new AssessmentRepository();
        private static byte[] ResumeData; 
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveEmployeeView()
        {
            var userId = (int)Security.Helpers.SessionHelpers.LOGGED_IN_USER.Value;
            var user = _repo.GetUserByID(userId);
            return View(user);
        }

        public JsonResult SaveEmployee(EmployeeModel employee)
        {
            var userId = (int)Security.Helpers.SessionHelpers.LOGGED_IN_USER.Value;

            employee.Resume = ResumeData;
            try
            {
              var data = _repo.SaveEmployee(employee, userId);
              var Result = new
              {
                  Data = data
              };

              return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(string.Format("There was an error when saving job seeker profile: {0}", ex.Message), JsonRequestBehavior.AllowGet);
            }                
        }

        [HttpPost]
        public ActionResult PostResume()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        var binaryReader = new BinaryReader(file.InputStream);
                        byte[] fileData = binaryReader.ReadBytes(file.ContentLength);

                        ResumeData = fileData;                     
                    }

                    return Json(string.Format("Resume uploaded Successfully"));
                }
                catch (Exception ex)
                {
                    return Json(string.Format("Error occurred. Error details: {0}", ex.Message));
                }
            }
            else
            {
                return Json(string.Format("No files selected."));
            }
        }

        public JsonResult GetEmployeeByID(int employeeID)
        {
            var result =string.Empty;

            try
            {
                var data = _repo.GetEmployeeByID(employeeID);

                var Result = new
                {
                    Data = data
                };

                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(string.Format("There was an error when saving job seeker profile: {0}", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetEmployees()
        {
            try
            {
                var data = _repo.GetEmployees();

                var Result = new
                {
                    Data = data
                };

                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("There was an error when saving job seeker profile: {0}", ex.Message, JsonRequestBehavior.AllowGet);    
            }         
        }
    }
}