using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assessment.Security;
using Assessment.Security.Filters;
using Assessment.Models;
using Assessment.Models.Repositories;
using WebMatrix.WebData;

namespace Assessment.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAssessmentRepository _repo = new AssessmentRepository();

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult SaveUser()
        {
            var model = new UserModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUser(UserModel user)
        {

            var ActionName = string.Empty;
            var ControllerName = string.Empty;

            if (user.Roles == "Employer")
            {
                ActionName = "SaveCompanyView";
                ControllerName = "Company";
            }
            else
            {
                ActionName = "SaveEmployeeView";
                ControllerName = "Employee";
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var newUser = _repo.SaveUser(user);
                    var userId = newUser.ID;
                    Security.Helpers.SessionHelpers.LOGGED_IN_USER = userId;

                    if (ModelState.IsValid && WebSecurity.Login(user.EmailAddress, user.Password, persistCookie: true))
                    {
                        return RedirectToAction(ActionName, ControllerName);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", string.Format("There was an error when saving company's details: {0}", ex.Message));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(user);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult PermissionDenied()
        {
            return View();
        }
    }
}