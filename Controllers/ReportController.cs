using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutionClass.Models;

namespace TutionClass.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            if (Session["AdminAccessed"] == null)
            {
                Response.Redirect("~/Access"); // Redirect to login page
            }
            ViewBag.Subjects = Class.GetUniqueSubjects();
            return View();
        }
        [HttpPost]
        public ActionResult Generate(ReportFilterViewModel filter)
        {
            // Assuming you have methods to get class information and payments based on filter
            // ReportViewModel should contain the required data for the report view
            ReportViewModel report = new ReportViewModel();
            report.ClassInfo = Class.GetClass(filter.SubjectName, filter.Grade);
            report.Payments = Payment.GetPayments(filter.SubjectName, filter.Grade);

            return View(report);
        }
    }

}