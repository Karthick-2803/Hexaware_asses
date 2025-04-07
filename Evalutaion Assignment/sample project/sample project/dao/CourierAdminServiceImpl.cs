using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sample_project.model;
using sample_project.util;
using System.Data.SqlClient;
using sample_project.Exceptions;

namespace sample_project.dao
{
    public class CourierAdminServiceImpl : CourierUserServiceImpl, ICourierAdminService
    {
        public int addCourierStaff(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"INSERT INTO Employee
                                      (EmployeeID,Name, Email, ContactNumber, Role, Salary)
                                      VALUES 
                                      (@EmployeeID,@Name, @Email, @ContactNumber, @Role, @Salary);
                                      SELECT SCOPE_IDENTITY();";
                    cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                    cmd.Parameters.AddWithValue("@ContactNumber", employee.ContactNumber);
                    cmd.Parameters.AddWithValue("@Role", employee.Role);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.Connection = connection;
                    connection.Open();
                    int newId = Convert.ToInt32(cmd.ExecuteScalar());
                    return newId;
                }
            }
        }
    }
}

