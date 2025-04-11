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
    public class PayrollServiceRepository : IPayrollServiceRepository
    {
        public string connectionString;
        SqlConnection sqlconnection = null;
        SqlCommand cmd = null;

        public PayrollServiceRepository()
        {
            //sqlconnection = new SqlConnection("Server=LAPTOP-49SCBJN2;Database=PayrollDb;Trusted_Connection=True");
            connectionString = DBConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }

        public List<Payroll> GetPayrollById(int payrollId)
        {
            List<Payroll> payrollList = new List<Payroll>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Select *from Payroll where PayrollID=@PayrollID";
                    cmd.Parameters.AddWithValue("@PayrollID", payrollId);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Payroll payroll = new Payroll();
                        payroll.PayrollID = (int)reader["PayrollID"];
                        payroll.EmployeeID = (int)reader["EmployeeID"];
                        payroll.PayPeriodStartDate= (DateTime)reader["PayPeriodStartDate"];
                        payroll.PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"];
                        payroll.BasicSalary = (decimal)reader["BasicSalary"];
                        payroll.OvertimePay = (decimal)reader["OvertimePay"];
                        payroll.Deductions = (decimal)reader["Deductions"];
                        payroll.NetSalary = (decimal)reader["NetSalary"];
                        payrollList.Add(payroll);

                    }
                    else
                    {
                        // If no records found, throw an exception
                        throw new PayrollGenerationException("Payroll not generated for the specified PayrollID.");
                    }
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            catch (PayrollGenerationException ex)
            {
                Console.WriteLine("Payroll generation failed: " + ex.Message);
            }
            return payrollList;
        }
        public List<Payroll> GetPayrollsForEmployee(int employeeId)
        {
            List<Payroll> payrollsList = new List<Payroll>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Select * from Payroll where EmployeeID=@EmployeeID";
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    bool hasRecords = false;
                    while (reader.Read())
                    {
                        hasRecords = true;
                        Payroll payroll = new Payroll();
                        payroll.PayrollID = (int)reader["PayrollID"];
                        payroll.EmployeeID = (int)reader["EmployeeID"];
                        payroll.PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"];
                        payroll.BasicSalary = (decimal)reader["BasicSalary"];
                        payroll.OvertimePay = (decimal)reader["OvertimePay"];
                        payroll.Deductions = (decimal)reader["Deductions"];
                        payroll.NetSalary = (decimal)reader["NetSalary"];
                        payrollsList.Add(payroll);
                    }

                    if (!hasRecords)
                    {
                        throw new PayrollGenerationException("Payroll not generated for the specified EmployeeID.");
                    }

                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            catch (PayrollGenerationException ex)
            {
                Console.WriteLine("Payroll generation failed: " + ex.Message);
            }
            return payrollsList;
        }
        public List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate)
        {
            List<Payroll> payrollsListForPeriod = new List<Payroll>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Select *from Payroll where PayPeriodStartDate>=@PayPeriodStartDate and PayPeriodEndDate<=@PayPeriodEndDate";
                    cmd.Parameters.AddWithValue("@PayPeriodStartDate", startDate);
                    cmd.Parameters.AddWithValue("@PayPeriodEndDate", endDate);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Payroll payroll = new Payroll();
                            payroll.PayrollID = (int)reader["PayrollID"];
                            payroll.EmployeeID = (int)reader["EmployeeID"];
                            payroll.PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"];
                            payroll.BasicSalary = (decimal)reader["BasicSalary"];
                            payroll.OvertimePay = (decimal)reader["OvertimePay"];
                            payroll.Deductions = (decimal)reader["Deductions"];
                            payroll.NetSalary = (decimal)reader["NetSalary"];
                            payrollsListForPeriod.Add(payroll);
                        }
                    }
                    else
                    {
                        throw new PayrollGenerationException("Payroll is not generated for the specified Payperiod");
                    }
                    cmd.Parameters.Clear();
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            catch (PayrollGenerationException ex)
            {
                Console.WriteLine("Payroll generation failed: " + ex.Message);
            }
            return (payrollsListForPeriod);
        }
        public List<Payroll> GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate)
        {
            List<Payroll> payrollList = new List<Payroll>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Select *from Payroll where EmployeeID =@EmployeeID and PayPeriodStartDate>=@PayPeriodStartDate and PayPeriodEndDate<=@PayPeriodEndDate";
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.Parameters.AddWithValue("@PayPeriodStartDate", startDate);
                    cmd.Parameters.AddWithValue("@PayPeriodEndDate", endDate);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Payroll payroll = new Payroll();
                        payroll.PayrollID = (int)reader["PayrollID"];
                        payroll.EmployeeID = (int)reader["EmployeeID"];
                        payroll.PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"];
                        payroll.BasicSalary = (decimal)reader["BasicSalary"];
                        payroll.OvertimePay = (decimal)reader["OvertimePay"];
                        payroll.Deductions = (decimal)reader["Deductions"];
                        payroll.NetSalary = (decimal)reader["NetSalary"];
                        payrollList.Add(payroll);
                    }
                    else
                    {
                        throw new PayrollGenerationException("Payroll is not generated for the specified condition");
                    }
                    cmd.Parameters.Clear();
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            catch (PayrollGenerationException ex)
            {
                Console.WriteLine("Payroll generation failed: " + ex.Message);
            }
            return (payrollList);

        }
    }
}
