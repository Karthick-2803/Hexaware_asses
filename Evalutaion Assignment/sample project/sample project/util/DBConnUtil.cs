using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace sample_project.util
{
    public static class DBConnUtil
    {
        public static string GetConnString()
        {
            return ConfigurationManager.ConnectionStrings["CourierDB"].ConnectionString;
        }
    }
}
