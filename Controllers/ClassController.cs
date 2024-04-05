
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using TutionClass.DBConnection;
using TutionClass.Models;

namespace TutionClass.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
        public ActionResult Index()
        {
            if (Session["AdminAccessed"] == null)
            {
                Response.Redirect("~/Access"); // Redirect to login page
            }
            List<Class> classList = Class.GetClasses();
            return View(classList);
        }

        public ActionResult AddClass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Class classObj)
        {
            try
            {
                classObj.AddClass(classObj);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error adding class: " + ex.Message;
                return View("AddClass", classObj);
            }
        }

        [HttpPost]
        public ActionResult Delete(string subjectName, int grade)
        {
            try
            {
                Class.DeleteClass(subjectName, grade);
                ViewBag.Message = "Class deleted successfully!";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error deleting class: " + ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
