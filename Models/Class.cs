using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TutionClass.DBConnection;
using System.Linq;
using System.Web;

namespace TutionClass.Models
{
    public class Class
    {
        public string SubjectName { get; set; }
        public int Grade { get; set; }
        public string TeacherName { get; set; }
        public decimal Price { get; set; }
        public string NameOfDay { get; set; }

        public static List<Class> GetClasses()
        {
            List<Class> classList = new List<Class>();
            MSSQLConnectionManager connectionManager = new MSSQLConnectionManager();
            using (SqlConnection connection = connectionManager.GetConnection())
            {
                string query = "SELECT subjectName, grade, teacherName, price, nameOfDay FROM class";

                SqlCommand command = new SqlCommand(query, connection);
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Class classObj = new Class
                    {
                        SubjectName = reader["subjectName"].ToString(),
                        Grade = Convert.ToInt32(reader["grade"]),
                        TeacherName = reader["teacherName"].ToString(),
                        Price = Convert.ToDecimal(reader["price"]),
                        NameOfDay = reader["nameOfDay"].ToString()
                    };

                    classList.Add(classObj);
                }

                reader.Close();
                connectionManager.CloseConnection(connection);
            }

            return classList;
        }
        public static Class GetClass(string subjectName, int grade)
        {
            Class classInfo = null;
            MSSQLConnectionManager connectionManager = new MSSQLConnectionManager();

            using (SqlConnection connection = connectionManager.GetConnection())
            {
                string query = "SELECT * FROM Class WHERE subjectName = @SubjectName AND grade = @Grade";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SubjectName", subjectName);
                command.Parameters.AddWithValue("@Grade", grade);

                try
                {
                    
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        classInfo = new Class
                        {
                            SubjectName = reader["subjectName"].ToString(),
                            Grade = Convert.ToInt32(reader["grade"]),
                            TeacherName = reader["teacherName"].ToString(),
                            Price = Convert.ToDecimal(reader["price"]),
                            NameOfDay = reader["nameOfDay"].ToString()
                        };
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Handle exception (e.g., log error, throw exception)
                    Console.WriteLine("Error retrieving class information: " + ex.Message);
                }
                connectionManager.CloseConnection(connection);
            }

            return classInfo;
        }

        public bool AddClass(Class classObj)
        {
            MSSQLConnectionManager connectionManager = new MSSQLConnectionManager();
            using (SqlConnection connection = connectionManager.GetConnection())
            {
                string query = "INSERT INTO class (subjectName, grade, teacherName, price, nameOfDay) " +
                               "VALUES (@SubjectName, @Grade, @TeacherName, @Price, @NameOfDay)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@SubjectName", classObj.SubjectName);
                command.Parameters.AddWithValue("@Grade", classObj.Grade);
                command.Parameters.AddWithValue("@TeacherName", classObj.TeacherName);
                command.Parameters.AddWithValue("@Price", classObj.Price);
                command.Parameters.AddWithValue("@NameOfDay", classObj.NameOfDay);

                int rowsAffected = command.ExecuteNonQuery();

                connectionManager.CloseConnection(connection);
                return rowsAffected > 0;
            }
        }

        public static bool DeleteClass(string subjectName, int grade)
        {
            MSSQLConnectionManager connectionManager = new MSSQLConnectionManager();
            using (SqlConnection connection = connectionManager.GetConnection())
            {
                string query = "DELETE FROM class WHERE subjectName = @SubjectName AND grade = @Grade";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SubjectName", subjectName);
                command.Parameters.AddWithValue("@Grade", grade);

                int rowsAffected = command.ExecuteNonQuery();
                connectionManager.CloseConnection(connection);
                return rowsAffected > 0;
            }
        }
        public static List<string> GetUniqueSubjects()
        {
            List<string> subjects = new List<string>();
            MSSQLConnectionManager connectionManager = new MSSQLConnectionManager();

            using (SqlConnection connection = connectionManager.GetConnection())
            {
                string query = "SELECT DISTINCT subjectName FROM Class";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                   
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string subject = reader["subjectName"].ToString();
                        subjects.Add(subject);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Handle exception (e.g., log error, throw exception)
                    Console.WriteLine("Error retrieving unique subjects: " + ex.Message);
                }
            }

            return subjects;
        }
    }
}
