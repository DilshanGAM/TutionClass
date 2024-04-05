using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutionClass.DBConnection;
using TutionClass.Models;

namespace TutionClass.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            if (Session["AdminAccessed"] == null)
            {
                Response.Redirect("~/Access"); // Redirect to login page
            }
            Student student = new Student();
            /*
             * CREATE TABLE student(
	            studentID VARCHAR(255) NOT NULL UNIQUE,
	            firstName VARCHAR(255) ,
	            lastname VARCHAR(255),
	            grade INT,
	            school VARCHAR(255),
	            gender VARCHAR(255),
	            CONSTRAINT STUDENTPK PRIMARY KEY(studentID)
            );
             */
            //read data from student table and create student object list and pass it to View object
            List<Student> studentList = Student.GetStudents();
            return View(studentList);
        }
        public ActionResult AddStudent()
        {           
            return View();
        }
        public ActionResult Create(Student student)
        {
            student.
                AddStudent(student);
            return RedirectToAction("Index", "Student");            
        }

        public ActionResult Delete(string studentID)
        {
            try
            {
                Student.DeleteStudent(studentID);
                ViewBag.Message = "Student deleted successfully!";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error deleting student: " + ex.Message;
            }

            // Redirect back to the Student List page
            return RedirectToAction("Index");
        }
    }
}