using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TutionClass.DBConnection;

namespace TutionClass.Models
{
    public class Student
    {
        public string StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Grade { get; set; }
        public string School { get; set; }
        public string Gender { get; set; }
        public static List<Student> GetStudents()
        {
            List<Student> studentList = new List<Student>();
            MSSQLConnectionManager connectionManager = new MSSQLConnectionManager();
            using (SqlConnection connection = connectionManager.GetConnection())
            {
                string query = "SELECT studentID, firstName, lastName, grade, school, gender FROM student";

                SqlCommand command = new SqlCommand(query, connection);
                //if connectionis not opened open connection
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Student student = new Student
                    {
                        StudentID = reader["studentID"].ToString(),
                        FirstName = reader["firstName"].ToString(),
                        LastName = reader["lastName"].ToString(),
                        Grade = Convert.ToInt32(reader["grade"]),
                        School = reader["school"].ToString(),
                        Gender = reader["gender"].ToString()
                    };

                    studentList.Add(student);
                }


                reader.Close();
                connectionManager.CloseConnection(connection);

                // Dispose the connection if needed
                connectionManager.DisposeConnection(connection);
            }

            return studentList;
        }
        public bool AddStudent(Student student)
        {
            MSSQLConnectionManager connectionManager = new MSSQLConnectionManager();
            using (SqlConnection connection = connectionManager.GetConnection())
            {
                string query = "INSERT INTO student (studentID, firstName, lastName, grade, school, gender) " +
                               "VALUES (@StudentID, @FirstName, @LastName, @Grade, @School, @Gender)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@StudentID", student.StudentID);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@Grade", student.Grade);
                command.Parameters.AddWithValue("@School", student.School);
                command.Parameters.AddWithValue("@Gender", student.Gender);

                
                int rowsAffected = command.ExecuteNonQuery();

                
                connectionManager.CloseConnection(connection);

                // Dispose the connection if needed
                connectionManager.DisposeConnection(connection);
                return rowsAffected > 0;
            }


        }
        public static bool DeleteStudent(string StudentID)
        {
            MSSQLConnectionManager connectionManager = new MSSQLConnectionManager();
            using (SqlConnection connection = connectionManager.GetConnection())
            {
                string query = "DELETE FROM student WHERE studentID = @StudentID ";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentID", StudentID);
                int rowsAffected = command.ExecuteNonQuery();
                connectionManager.CloseConnection(connection);

                // Dispose the connection if needed
                connectionManager.DisposeConnection(connection);
                return rowsAffected > 0;
            }


        }
    }

}
