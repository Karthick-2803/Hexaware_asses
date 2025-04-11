using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PayrollManagement.Exceptions;
using PayrollManagement.model;
using PayrollManagement.Utility;

namespace PayrollManagement.Repository
{
    public class EmployeeServiceRepository : IEmployeeServiceRepository
    {

        public string connectionString;
        SqlConnection sqlconnection = null;
        SqlCommand cmd = null;

        public EmployeeServiceRepository()
        {
            //sqlconnection = new SqlConnection("Server=LAPTOP-49SCBJN2;Database=PayrollDb;Trusted_Connection=True");
            //sqlconnection = new SqlConnection(DBConnUtil.GetConnectionString());
            connectionString = DBConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }
        public List<Employee> GetAllEmployees()
        {
            List<Employee> EmployeeList = new List<Employee>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "select *from Employee";
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeID = (int)reader["EmployeeID"];
                        employee.FirstName = (string)reader["FirstName"];
                        employee.LastName = (string)reader["lastName"];
                        employee.DateOfBirth = (DateTime)reader["DateOfBirth"];
                        employee.Gender = (string)reader["Gender"];
                        employee.Mail = (string)reader["Email"];
                        employee.PhoneNumber = (string)reader["PhoneNumber"];
                        employee.Address = (string)reader["Address"];
                        employee.Position = (string)reader["Position"];
                        employee.JoiningDate = (DateTime)reader["JoiningDate"];
                        if (reader["TerminationDate"] != DBNull.Value)
                        {
                            employee.TerminationDate = (DateTime)reader["TerminationDate"];
                        }
                        else
                        {
                            employee.TerminationDate = null;
                        }
                        EmployeeList.Add(employee);

                    }
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            return EmployeeList;
        }
        public List<Employee> GetEmployeeById(int id)
        {
            List<Employee> EmployeeList = new List<Employee>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "select *from Employee where EmployeeID=@EmployeeID";
                    cmd.Parameters.AddWithValue("@EmployeeID", id);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    //if (!reader.HasRows)
                    //{
                    //    throw new EmployeeNotFoundException("Employee ID does not exist.");
                    //}
                    while (reader.Read())
                    {
                        Employee emp = new Employee();
                        emp.EmployeeID = (int)reader["EmployeeID"];
                        emp.FirstName = (string)reader["FirstName"];
                        emp.LastName = (string)reader["lastName"];
                        emp.DateOfBirth = (DateTime)reader["DateOfBirth"];
                        emp.Gender = (string)reader["Gender"];
                        emp.Mail = (string)reader["Email"];
                        emp.PhoneNumber = (string)reader["PhoneNumber"];
                        emp.Address = (string)reader["Address"];
                        emp.Position = (string)reader["Position"];
                        emp.JoiningDate = (DateTime)reader["JoiningDate"];
                        if (reader["TerminationDate"] != DBNull.Value)
                        {
                            emp.TerminationDate = (DateTime)reader["TerminationDate"];
                        }
                        else
                        {
                            emp.TerminationDate = null;
                        }
                        EmployeeList.Add(emp);
                    }
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return EmployeeList;
        }
        public int UpdateEmployee(Employee employee)
        {
            int UpdateStatus = 0;
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "UPDATE employee SET FirstName=@FirstName where EmployeeID=@EmployeeID";
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    UpdateStatus = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            return UpdateStatus;
        }
        public int RemoveEmployee(int employeeId)
        {
            int DeleteStatus = 0;
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Delete from Employee where EmployeeID=@EmployeeID";
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    DeleteStatus = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            return DeleteStatus;
        }
        public int AddEmployee(Employee employee)
        {
            int addEmployeeStatus = 0;
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "INSERT INTO Employee (EmployeeID, FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber, Address, Position, JoiningDate) " +
                      "VALUES (@EmployeeID, @FirstName, @LastName, @DateOfBirth, @Gender, @Email, @PhoneNumber, @Address, @Position, @JoiningDate)";
                    cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@Email", employee.Mail);
                    cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", employee.Address);
                    cmd.Parameters.AddWithValue("@Position", employee.Position);
                    cmd.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    addEmployeeStatus = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            return addEmployeeStatus;
        }
    }
}
