using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample_project.model
{
    public class Courier
    {
        public int CourierID { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public double Weight { get; set; }
        public string Status { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int LocationID { get; set; }
        public int EmployeeID { get; set; }
        public int ServiceID { get; set; }

        public Courier() { }
        public Courier(int courierID, string senderName, string senderAddress,
                       string receiverName, string receiverAddress, double weight,
                       string status, string trackingNumber, DateTime deliveryDate,
                       int locationID, int employeeID, int serviceID)
        {
            CourierID = courierID;
            SenderName = senderName;
            SenderAddress = senderAddress;
            ReceiverName = receiverName;
            ReceiverAddress = receiverAddress;
            Weight = weight;
            Status = status;
            TrackingNumber = trackingNumber;
            DeliveryDate = deliveryDate;
            LocationID = locationID;
            EmployeeID = employeeID;
            ServiceID = serviceID;
        }

        public override string ToString()
        {
            return $"CourierID: {CourierID}, Sender: {SenderName}, Receiver: {ReceiverName}, " +
                   $"Weight: {Weight}kg, Status: {Status}, Tracking: {TrackingNumber}, " +
                   $"Delivery: {DeliveryDate.ToShortDateString()}, LocationID: {LocationID}, " +
                   $"EmployeeID: {EmployeeID}, ServiceID: {ServiceID}";
        }
    }
}
