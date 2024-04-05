using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutionClass.Models;

namespace TutionClass.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index()
        {
            if (Session["AdminAccessed"] == null)
            {
                Response.Redirect("~/Access"); // Redirect to login page
            }
            List<Payment> payments = Payment.GetAllPayments();
            return View(payments);
        }

        public ActionResult AddPayment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Payment payment)
        {

            payment.CreatePayment();                
            
            
            return View("AddPayment", payment);
        }
    }
}
