using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sample_project.Exceptions;


namespace sample_project.Exceptions
{
    public class InvalidEmployeeIdException: Exception
    {
        public InvalidEmployeeIdException() : base("Employee Not Found/Employee Has not been alloted any assets") { }

        public InvalidEmployeeIdException(string message) : base(message) { }

    }
}
