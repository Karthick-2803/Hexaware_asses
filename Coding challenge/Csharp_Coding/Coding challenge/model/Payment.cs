using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_challenge.model
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public Client Client { get; set; }

        // Default constructor
        public Payment() { }

        // Parameterized constructor
        public Payment(int paymentId, DateTime paymentDate, decimal paymentAmount, Client client)
        {
            PaymentId = paymentId;
            PaymentDate = paymentDate;
            PaymentAmount = paymentAmount;
            Client = client;
        }

        // ToString override
        public override string ToString()
        {
            return $"PaymentID: {PaymentId}, Date: {PaymentDate:yyyy-MM-dd}\n" +
                   $"Amount: {PaymentAmount:C}\n" +
                   $"Client: {Client?.ClientName} ({Client?.ClientId})";
        }
    }


}
