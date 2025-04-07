using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sample_project.model;

namespace sample_project.dao
{
    public interface ICourierUserService
    {
        bool placeOrder(Courier newCourier);
        string getOrderStatus(string trackingNumber);
        bool cancelOrder(string trackingNumber);
        List<Courier> getAssignedOrder(int courierStaffId);

        List<Courier> RetrieveDeliveryHistory(string trackingNumber);
        //Dictionary<string, string> GenerateShipmentStatusReport();
        decimal GenerateRevenueReport();

    }
}
