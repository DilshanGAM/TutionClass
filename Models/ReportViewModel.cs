using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutionClass.Models
{
    public class ReportViewModel
    {
        public Class ClassInfo { get; set; }
        public List<Payment> Payments { get; set; }
        public decimal TotalPayments { get; set; }
        public string TeacherName { get; set; }
        public decimal getTotal()
        {
            return Payments.Count * ClassInfo.Price;
        }
    }
}