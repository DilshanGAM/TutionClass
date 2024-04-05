using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TutionClass.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {

            if (username == "Admin" && password == "123")
            {
                Session["AdminAccessed"] = true;
                return RedirectToAction("Index", "Home"); // Redirect to home page or any other page
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["AdminAccessed"] = null;
            return RedirectToAction("Index");
        }
    }
}