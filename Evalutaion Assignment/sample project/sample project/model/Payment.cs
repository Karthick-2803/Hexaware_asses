using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample_project.model
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int CourierID { get; set; }
        public int LocationID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int EmployeeID { get; set; }
        public Payment () { }

        public Payment(int paymentID, int courierID, int locationID, decimal amount, DateTime paymentDate, int employeeID)
        {
            PaymentID = paymentID;
            CourierID = courierID;
            LocationID = locationID;
            Amount = amount;
            PaymentDate = paymentDate;
            EmployeeID = employeeID;
        }

        public override string ToString()
        {
            return $"PaymentID: {PaymentID}, CourierID: {CourierID}, LocationID: {LocationID}, " +
                   $"Amount: {Amount}, PaymentDate: {PaymentDate.ToShortDateString()}, " +
                   $"EmployeeID: {EmployeeID}";
        }
    }
}
