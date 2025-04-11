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
    internal class FinancialRecordServiceRepository : IFinancialRecordServiceRepository
    {
        public string connectionString;
        SqlConnection sqlconnection = null;
        SqlCommand cmd = null;
        public FinancialRecordServiceRepository()
        {
            //sqlconnection = new SqlConnection("Server=LAPTOP-49SCBJN2;Database=CourierDb;Trusted_Connection=True");
            //sqlconnection = new SqlConnection(DBConnUtil.GetConnectionString());
            connectionString = DBConnUtil.GetConnectionString();
            cmd = new SqlCommand();

        }
        public List<FinancialRecord> GetFinancialRecordById(int recordId)
        {
            List<FinancialRecord> financialRecordList = new List<FinancialRecord>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "SELECT * FROM FinancialRecord WHERE RecordID=@RecordID";
                    cmd.Parameters.AddWithValue("@RecordID", recordId);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        FinancialRecord financialRecord = new FinancialRecord();
                        financialRecord.RecordID = (int)reader["RecordID"];
                        financialRecord.EmployeeID = (int)reader["EmployeeID"];
                        financialRecord.RecordDate = (DateTime)reader["RecordDate"];
                        financialRecord.Description = (string)reader["Description"];
                        financialRecord.Amount = (decimal)reader["Amount"];
                        financialRecord.RecordType = (string)reader["RecordType"];
                        financialRecordList.Add(financialRecord);
                    }
                    else
                    {
                        throw new FinancialRecordException("FinancialRecord not found");
                    }
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            catch (FinancialRecordException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return financialRecordList;


        }
        public List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId)
        {
            List<FinancialRecord> financialRecordList = new List<FinancialRecord>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "SELECT * FROM FinancialRecord WHERE EmployeeID=@EmployeeID";
                    cmd.Parameters.AddWithValue("EmployeeID", employeeId);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        while (reader.Read())
                        {
                            FinancialRecord financialRecord = new FinancialRecord();
                            financialRecord.RecordID = (int)reader["RecordID"];
                            financialRecord.EmployeeID = (int)reader["EmployeeID"];
                            financialRecord.RecordDate = (DateTime)reader["RecordDate"];
                            financialRecord.Description = (string)reader["Description"];
                            financialRecord.Amount = (decimal)reader["Amount"];
                            financialRecord.RecordType = (string)reader["RecordType"];
                            financialRecordList.Add(financialRecord);
                        }
                    }
                    else
                    {
                        throw new FinancialRecordException("Financialrecord not Found");
                    }
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            catch (FinancialRecordException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return financialRecordList;
        }
        //public List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate)
        //{
        //    List<FinancialRecord> financialRecordList = new List<FinancialRecord>();
        //    try
        //    {
        //        using (SqlConnection sqlconnection = new SqlConnection(connectionString))
        //        {
        //            cmd.CommandText = "SELECT * FROM FinancialRecord WHERE RecordDate=@RecordDate";
        //            cmd.Parameters.AddWithValue("@RecordDate", recordDate);
        //            cmd.Connection = sqlconnection;
        //            sqlconnection.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            if (reader.Read())
        //            {
        //                while (reader.Read())
        //                {
        //                    FinancialRecord financialRecord = new FinancialRecord();
        //                    financialRecord.RecordID = (int)reader["RecordID"];
        //                    financialRecord.EmployeeID = (int)reader["EmployeeID"];
        //                    financialRecord.RecordDate = (DateTime)reader["RecordDate"];
        //                    financialRecord.Description = (string)reader["Description"];
        //                    financialRecord.Amount = (decimal)reader["Amount"];
        //                    financialRecord.RecordType = (string)reader["RecordType"];
        //                    financialRecordList.Add(financialRecord);
        //                }
        //            }
        //            else
        //            {
        //                throw new FinancialRecordException("Financial Record not Found");
        //            }
        //            cmd.Parameters.Clear();
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine("DataBaseConnection failed" + ex.Message);
        //    }
        //    catch (FinancialRecordException ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return financialRecordList;
        //}
        public List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate)
        {
            List<FinancialRecord> financialRecordList = new List<FinancialRecord>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SELECT * FROM FinancialRecord WHERE RecordDate = @RecordDate";
                    cmd.Parameters.AddWithValue("@RecordDate", recordDate); // Fix: added '@'
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read()) // Fix: use only while loop
                    {
                        FinancialRecord financialRecord = new FinancialRecord();
                        financialRecord.RecordID = (int)reader["RecordID"];
                        financialRecord.EmployeeID = (int)reader["EmployeeID"];
                        financialRecord.RecordDate = (DateTime)reader["RecordDate"];
                        financialRecord.Description = (string)reader["Description"];
                        financialRecord.Amount = (decimal)reader["Amount"];
                        financialRecord.RecordType = (string)reader["RecordType"];
                        financialRecordList.Add(financialRecord);
                    }

                    if (financialRecordList.Count == 0)
                    {
                        throw new FinancialRecordException("Financial Record not Found");
                    }

                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database connection failed: " + ex.Message);
            }
            catch (FinancialRecordException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return financialRecordList;
        }

        public int AddFinancialRecord(FinancialRecord financialRecord)
        {
            int addRecordStatus = 0;
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "INSERT INTO FinancialRecord VALUES (@RecordID,@EmployeeID, @RecordDate, @Description, @Amount, @RecordType)";
                    cmd.Parameters.AddWithValue("@RecordID", financialRecord.RecordID);
                    cmd.Parameters.AddWithValue("@EmployeeID", financialRecord.EmployeeID);
                    cmd.Parameters.AddWithValue("@RecordDate", financialRecord.RecordDate);
                    cmd.Parameters.AddWithValue("@Description", financialRecord.Description);
                    cmd.Parameters.AddWithValue("@Amount", financialRecord.Amount);
                    cmd.Parameters.AddWithValue("@RecordType", financialRecord.RecordType);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    addRecordStatus = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("DataBaseConnection failed" + ex.Message);
            }
            return addRecordStatus;
        }
    }
}
