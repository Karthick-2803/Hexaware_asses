using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PayrollManagement.model;
using PayrollManagement.Utility;

namespace PayrollManagement.Repository
{
    public class TaxServiceRepository : ITaxServiceRepository
    {
        public string connectionString;
        SqlConnection sqlconnection = null;
        SqlCommand cmd = null;
        public TaxServiceRepository()
        {
            //sqlconnection = new SqlConnection("Server=LAPTOP-49SCBJN2;Database=PayrollDb;Trusted_Connection=True");
            connectionString = DBConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }
        public List<Tax> GetTaxById(int taxId)
        {
            List<Tax> taxList = new List<Tax>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Select *from Tax where TaxID=@taxID";
                    cmd.Parameters.AddWithValue("@TaxID", taxId);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Tax tax = new Tax();
                        tax.TaxID = (int)reader["TaxID"];
                        tax.EmployeeID = (int)reader["EmployeeID"];
                        tax.TaxYear = (int)reader["TaxYear"];
                        tax.TaxableIncome = (decimal)reader["TaxableIncome"];
                        tax.TaxAmount = (decimal)reader["TaxAmount"];
                        taxList.Add(tax);
                    }
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            return taxList;
        }
        public List<Tax> GetTaxesForEmployee(int employeeId)
        {

            List<Tax> taxList = new List<Tax>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Select *from Tax where EmployeeID=@employeeID";
                    cmd.Parameters.AddWithValue("@employeeID", employeeId);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Tax tax = new Tax();
                        tax.TaxID = (int)reader["TaxID"];
                        tax.EmployeeID = (int)reader["EmployeeID"];
                        tax.TaxYear = (int)reader["TaxYear"];
                        tax.TaxableIncome = (decimal)reader["TaxableIncome"];
                        tax.TaxAmount = (decimal)reader["TaxAmount"];
                        taxList.Add(tax);
                    }
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            return taxList;
        }
        public List<Tax> GetTaxesForYear(int taxYear)
        {
            List<Tax> taxList = new List<Tax>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Select *from Tax where TaxYear=@TaxYear";
                    cmd.Parameters.AddWithValue("@TaxYear", taxYear);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Tax tax = new Tax();
                        tax.TaxID = (int)reader["TaxID"];
                        tax.EmployeeID = (int)reader["EmployeeID"];
                        tax.TaxYear = (int)reader["TaxYear"];
                        tax.TaxableIncome = (decimal)reader["TaxableIncome"];
                        tax.TaxAmount = (decimal)reader["TaxAmount"];
                        taxList.Add(tax);
                    }
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            return taxList;
        }
        public decimal CalculateTax(int employeeId, int taxYear)
        {
            decimal taxAmount = 0;
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Select *from Tax where EmployeeID=@EmployeeId and TaxYear=@TaxYear";
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmd.Parameters.AddWithValue("@TaxYear", taxYear);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        decimal taxableIncome = (decimal)reader["TaxableIncome"];
                        taxAmount = taxableIncome * 0.20m;
                    }
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            return taxAmount;
        }
    }
}
