using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sample_project.util;
using sample_project.model;
using sample_project.Exceptions;
using System.Data.SqlClient;

namespace sample_project.dao
{
    public class CourierUserServiceImpl : ICourierUserService
    {
        private readonly string _connectionString;

        public CourierUserServiceImpl()
        {
            _connectionString = DBConnUtil.GetConnString();
        }

        public List<Courier> GetallCouriers()
        {
            List<Courier> couriers = new List<Courier>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Courier";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        couriers.Add(new Courier(
                            Convert.ToInt32(reader["CourierID"]),
                            reader["SenderName"].ToString(),
                            reader["SenderAddress"].ToString(),
                            reader["ReceiverName"].ToString(),
                            reader["ReceiverAddress"].ToString(),
                            Convert.ToDouble(reader["Weight"]),
                            reader["Status"].ToString(),
                            reader["TrackingNumber"].ToString(),
                            Convert.ToDateTime(reader["DeliveryDate"]),
                            Convert.ToInt32(reader["LocationID"]),
                            Convert.ToInt32(reader["EmployeeID"]),
                            Convert.ToInt32(reader["ServiceID"])
                        ));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }
            return couriers;
        }

        public bool placeOrder(Courier courier)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = @"INSERT INTO Courier 
                                   (CourierID, SenderName, SenderAddress, ReceiverName, 
                                    ReceiverAddress, Weight, Status, 
                                    TrackingNumber, DeliveryDate,
                                    LocationID, EmployeeID, ServiceID)
                                   VALUES 
                                   (@CourierID, @SenderName, @SenderAddress, @ReceiverName, 
                                    @ReceiverAddress, @Weight, @Status, 
                                    @TrackingNumber, @DeliveryDate,
                                    @LocationID, @EmployeeID, @ServiceID)";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@CourierID", courier.CourierID);
                    command.Parameters.AddWithValue("@SenderName", courier.SenderName);
                    command.Parameters.AddWithValue("@SenderAddress", courier.SenderAddress);
                    command.Parameters.AddWithValue("@ReceiverName", courier.ReceiverName);
                    command.Parameters.AddWithValue("@ReceiverAddress", courier.ReceiverAddress);
                    command.Parameters.AddWithValue("@Weight", courier.Weight);
                    command.Parameters.AddWithValue("@Status", courier.Status ?? "Processing");
                    command.Parameters.AddWithValue("@TrackingNumber", courier.TrackingNumber);
                    command.Parameters.AddWithValue("@DeliveryDate", courier.DeliveryDate);
                    command.Parameters.AddWithValue("@LocationID", courier.LocationID);
                    command.Parameters.AddWithValue("@EmployeeID", courier.EmployeeID);
                    command.Parameters.AddWithValue("@ServiceID", courier.ServiceID);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }
        }


        public string getOrderStatus(string trackingNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT Status FROM Courier WHERE TrackingNumber = @TrackingNumber";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TrackingNumber", trackingNumber);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result == null)
                    {
                        throw new TrackingNumberNotFoundException($"Tracking number {trackingNumber} not found");
                    }

                    return result.ToString();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }
        }


        public bool cancelOrder(string trackingNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE Courier SET Status = 'Cancelled' WHERE TrackingNumber = @TrackingNumber";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TrackingNumber", trackingNumber);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new TrackingNumberNotFoundException($"Tracking number {trackingNumber} not found");
                    }

                    return true;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }
        }


        public List<Courier> getAssignedOrder(int employeeID)
        {
            List<Courier> assignedOrders = new List<Courier>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Courier WHERE EmployeeID = @EmployeeID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EmployeeID", employeeID);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        assignedOrders.Add(new Courier(
                            Convert.ToInt32(reader["CourierID"]),
                            reader["SenderName"].ToString(),
                            reader["SenderAddress"].ToString(),
                            reader["ReceiverName"].ToString(),
                            reader["ReceiverAddress"].ToString(),
                            Convert.ToDouble(reader["Weight"]),
                            reader["Status"].ToString(),
                            reader["TrackingNumber"].ToString(),
                            Convert.ToDateTime(reader["DeliveryDate"]),
                            Convert.ToInt32(reader["LocationID"]),
                            Convert.ToInt32(reader["EmployeeID"]),
                            Convert.ToInt32(reader["ServiceID"])
                        ));
                    }
                }
                if (assignedOrders.Count == 0)
                {
                    throw new InvalidEmployeeIdException($"No assigned orders found. Employee ID {employeeID} might be invalid.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }

            return assignedOrders;
        }



        public List<Courier> RetrieveDeliveryHistory(string trackingNumber)
        {
            List<Courier> history = new List<Courier>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Courier WHERE TrackingNumber = @TrackingNumber";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TrackingNumber", trackingNumber);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                history.Add(new Courier
                                {
                                    CourierID = Convert.ToInt32(reader["CourierID"]),
                                    SenderName = reader["SenderName"].ToString(),
                                    SenderAddress = reader["SenderAddress"].ToString(),
                                    ReceiverName = reader["ReceiverName"].ToString(),
                                    ReceiverAddress = reader["ReceiverAddress"].ToString(),
                                    Weight = Convert.ToDouble(reader["Weight"]),
                                    Status = reader["Status"].ToString(),
                                    TrackingNumber = reader["TrackingNumber"].ToString(),
                                    DeliveryDate = Convert.ToDateTime(reader["DeliveryDate"]),
                                    LocationID = Convert.ToInt32(reader["LocationID"]),
                                    EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                    ServiceID = Convert.ToInt32(reader["ServiceID"])
                                });
                            }
                        }
                    }
                }
                return history;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving delivery history: {ex.Message}");
                return history;
            }
        }

        public decimal GenerateRevenueReport()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT SUM(Amount) as TotalRevenue FROM Payment";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating revenue report: {ex.Message}");
                return 0;
            }
        }
    }
}
