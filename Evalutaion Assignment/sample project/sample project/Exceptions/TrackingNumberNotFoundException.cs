using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sample_project.Exceptions;


namespace sample_project.Exceptions
{
    public class TrackingNumberNotFoundException : Exception
    {
        public TrackingNumberNotFoundException() : base("Tracking number not found in the system.")
        {
        }

        public TrackingNumberNotFoundException(string message) : base(message)
        {
        }
    }
}
