
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Data.SqlClient;
using TutionClass.DBConnection;

namespace TutionClass.Models
{
    public class Payment
    {
        public string StudentID { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
        public string SubjectName { get; set; }
        public int Grade { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime Timestamp { get; set; }

        public bool CreatePayment()
        {
            
                MSSQLConnectionManager connectionManager = new MSSQLConnectionManager();
                using (SqlConnection connection = connectionManager.GetConnection())
                {
                    string query = @"INSERT INTO payment (studentID, year, month, subjectName, grade, paymentMethod) 
                                     VALUES (@StudentID, @Year, @Month, @SubjectName, @Grade, @PaymentMethod)";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@StudentID", StudentID);
                    command.Parameters.AddWithValue("@Year", Convert.ToInt32(Year));
                    command.Parameters.AddWithValue("@Month", Month);
                    command.Parameters.AddWithValue("@SubjectName", SubjectName);
                    command.Parameters.AddWithValue("@Grade", Convert.ToInt32(Grade));
                    command.Parameters.AddWithValue("@PaymentMethod", PaymentMethod);

                    
                    int rowsAffected = command.ExecuteNonQuery();
                    connectionManager.CloseConnection(connection);
                    return rowsAffected > 0;
                }
            
            
        }
        public static List<Payment> GetAllPayments()
        {
            List<Payment> payments = new List<Payment>();
            MSSQLConnectionManager connectionManager = new MSSQLConnectionManager();

            using (SqlConnection connection = connectionManager.GetConnection())
            {
                string query = "SELECT * FROM payment";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Payment payment = new Payment
                        {
                            StudentID = reader["studentID"].ToString(),
                            Year = Convert.ToInt32(reader["year"]),
                            Month = reader["month"].ToString(),
                            SubjectName = reader["subjectName"].ToString(),
                            Grade = Convert.ToInt32(reader["grade"]),
                            PaymentMethod = reader["paymentMethod"].ToString(),
                            Timestamp = Convert.ToDateTime(reader["timestamp"])
                        };
                        payments.Add(payment);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Handle exception (e.g., log error, throw exception)
                    Console.WriteLine("Error retrieving payments: " + ex.Message);
                }
            }

            return payments;
        }
        public static List<Payment> GetPayments(string subjectName, int grade)
        {
            List<Payment> payments = new List<Payment>();
            MSSQLConnectionManager connectionManager = new MSSQLConnectionManager();

            using (SqlConnection connection = connectionManager.GetConnection())
            {
                string query = "SELECT * FROM payment WHERE subjectName = @SubjectName AND grade = @Grade";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SubjectName", subjectName);
                command.Parameters.AddWithValue("@Grade", grade);

                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Payment payment = new Payment
                        {
                            StudentID = reader["studentID"].ToString(),
                            Year = Convert.ToInt32(reader["year"]),
                            Month = reader["month"].ToString(),
                            SubjectName = reader["subjectName"].ToString(),
                            Grade = Convert.ToInt32(reader["grade"]),
                            PaymentMethod = reader["paymentMethod"].ToString(),
                            Timestamp = Convert.ToDateTime(reader["timestamp"])
                        };
                        payments.Add(payment);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Handle exception (e.g., log error, throw exception)
                    Console.WriteLine("Error retrieving payments: " + ex.Message);
                }
                connectionManager.CloseConnection(connection);
            }

            return payments;
        }

    }
}
