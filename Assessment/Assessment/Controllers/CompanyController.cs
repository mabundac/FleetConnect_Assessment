using Assessment.Models;
using Assessment.Models.Repositories;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assessment.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly IAssessmentRepository _repo = new AssessmentRepository();
        private static byte[] LogoData;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveCompanyView()
        {
            return View();
        }

        public JsonResult SaveCompany(CompanyModel company)
        {
            company.Logo = LogoData;
            try
            {
                var userId = (int)Security.Helpers.SessionHelpers.LOGGED_IN_USER.Value;
                var data = _repo.SaveCompany(company, userId);
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

        public JsonResult GetCompanyByID(int companyID)
        {
            var result = _repo.GetCompanyByID(companyID);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCompanies()
        {
            var result = _repo.GetCompanies();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PostLogo()
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

                        LogoData = fileData;
                    }

                    return Json(string.Format("Logo uploaded Successfully"));
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
    }
}