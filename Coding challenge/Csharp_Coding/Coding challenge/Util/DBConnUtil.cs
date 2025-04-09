using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
//using Microsoft.Extensions.Configuration;


namespace Coding_challenge.Util
{
    public static class DBConnUtil
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["InsuranceDB"].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
    //public static class DBConnUtil
    //{
    //    public static SqlConnection GetConnection(string connectionString)
    //    {
    //        //return new SqlConnection(connectionString);
    //        return ConfigurationManager.ConnectionStrings["CourierDB"].ConnectionString;
    //    }
    //}
}
