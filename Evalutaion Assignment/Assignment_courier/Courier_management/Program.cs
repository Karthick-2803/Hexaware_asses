
using System.Threading;
using System.Text.RegularExpressions;
using System.Reflection.Metadata;
using System.Net;
using System.Numerics;
using System.Collections.Generic;
namespace Courier_management
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            Program program = new Program();
            program.chk_status();
            program.category();
            program.login();
            program.logic();
            program.specific_cust();
            program.track();
            program.history();
            program.nearest();
            program.parcel_tracking();
            program.data_validation();
            program.address_formating();
            program.order_Cnfrmation();
            program.calulating_cost();
            program.genertor();
            program.similar_address();
        }
        public void chk_status()    //1  
        {
            Console.WriteLine("Enter the order_status: ");
            string order_status = Console.ReadLine();
            if (order_status == "Delivered")
            {
                Console.WriteLine("The order has been Delivered");
            }
            else if (order_status == "Processing")
            {
                Console.WriteLine("The order is still been Processed ");
            }
            else if (order_status == "Cancelled")
            {
                Console.WriteLine("The order has been cancelled");
            }
            else
            {
                Console.WriteLine("Invalid order status. Please check the status again.");
            }
        }

        public void category()   //2
        {
            Console.WriteLine("Enter the weight:");
            int weight = int.Parse(Console.ReadLine());
            switch (weight)
            {
                case int w when w < 5:
                    Console.WriteLine("Light");
                    break;

                case int w when w >= 5 && w < 10:
                    Console.WriteLine("Medium");
                    break;

                case int w when w >= 10:
                    Console.WriteLine("Heavy");
                    break;

                default:
                    Console.WriteLine("Invalid weight");
                    break;


            }
        }

        public void login()      //3
        {
            string employeeUsername = "employee";
            string employeePassword = "employee123";
            string customerUsername = "customer";
            string customerPassword = "customer123";

            Console.Write("Enter username: ");
            string usernameInput = Console.ReadLine();

            Console.Write("Enter password: ");
            string passwordInput = Console.ReadLine();

            if (usernameInput == employeeUsername && passwordInput == employeePassword)
            {
                Console.WriteLine("Employee login successful.");
            }
            else if (usernameInput == customerUsername && passwordInput == customerPassword)
            {
                Console.WriteLine("Customer login successful.");
            }
            else
            {
                Console.WriteLine("Invalid username or password");
            }
        }

        public void logic()  //4
        {
            string[] couriers = { "Anu", "Abi", "Ravi" };
            string[] areas = { "Chennai", "Mumbai", "Delhi" };
            int[] capacities = { 100, 80, 60 };
            int[] loads = { 30, 20, 50 };

            string shipmentArea = "Mumbai";
            int shipmentWeight = 20;

            bool isAssigned = false;

            for (int i = 0; i < couriers.Length; i++)
            {
                bool isSameArea = areas[i] == shipmentArea;
                bool hasCapacity = loads[i] + shipmentWeight <= capacities[i];

                if (isSameArea && hasCapacity)
                {
                    Console.WriteLine($"Shipment successfully assigned to courier: {couriers[i]}");
                    loads[i] += shipmentWeight;
                    isAssigned = true;
                    break;
                }
            }

            if (!isAssigned)
            {
                Console.WriteLine("No suitable courier available for this shipment.");
            }
        }

        public void specific_cust()  //5
        {
            int[] orderid = { 1, 2, 3, 4, 5 };
            string[] customer_name = { "Customer A", "Customer B", "Customer A", "Customer C", "Customer A" };
            string[] status = { "Processing", "Delivered", "Cancelled", "Processing", "Delivered" };

            Console.WriteLine("Enter the name: ");
            string name = Console.ReadLine();

            bool orderFound = false;

            for (int index = 0; index < customer_name.Length; index++)
            {
                if (customer_name[index] == name)
                {
                    Console.WriteLine($"Order ID:{orderid[index]},Status:{status[index]}");
                    orderFound = true;
                }
            }

            if (!orderFound)
            {
                Console.WriteLine("No order Found");
            }
        }


        public void track()  //6
        {
            string courierName = "Courier A";
            int currentLoc = 0;
            int destination = 5;
            while (currentLoc < destination)
            {
                currentLoc++;
                Console.WriteLine($"{courierName}'s currentLoc : {currentLoc}");
                Thread.Sleep(1000);
            }
            Console.WriteLine($"{courierName} has reached the destination ");
            Console.WriteLine("Completed");
        }

        public void history() //7
        {
            string parcelName = "ABC321";
            string[] location = { "Warehouse", "In Transit", "Local Distribution Center", "Delivered" };
            string[,] tracking_hist = new string[location.Length, 2];

            for (int index = 0; index < location.Length; index++)
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                tracking_hist[index, 0] = timestamp;
                tracking_hist[index, 1] = location[index];
                Console.WriteLine($"{timestamp} - Location: {location[index]}");
                Thread.Sleep(1000);
            }
        }

        public void nearest()      //8
        {
            string[] courier = { "Courier A", "Courier B", "Courier C" };
            int[] distance = { 10, 5, 15 };
            int[] capacity = { 20, 10, 25 };

            Console.WriteLine("Enter the order : ");
            int order = int.Parse(Console.ReadLine());

            int nearest_index = -1;
            int shortest_distance = int.MaxValue;

            for (int index = 0; index < courier.Length; index++)
            {
                if (capacity[index] >= order)
                {
                    if (distance[index] < shortest_distance)
                    {
                        shortest_distance = distance[index];
                        nearest_index = index;
                    }
                }
            }
            if (nearest_index != -1)
            {
                Console.WriteLine($"The nearest available courier is: {courier[nearest_index]}");
            }
            else
            {

                Console.WriteLine("No available couriers.");
            }
        }

        public void parcel_tracking()  //9
        {
            string[,] parceldata = { { "ABC123", "Parcel in transit" },
            { "DEF456", "Parcel out for delivery" },
            { "GHI789", "Parcel delivered" },
            { "JKL012", "Parcel in transit" }};

            Console.WriteLine("Enter the tracking_number : ");
            string tracking_number = Console.ReadLine();
            bool isFound = false;
            for (int index = 0; index < parceldata.Length; index++)
            {
                if (parceldata[index, 0] == tracking_number)
                {
                    Console.WriteLine($"Status for {tracking_number} : {parceldata[index, 1]} ");
                    isFound = true;
                    break;
                }
            }

            if (!isFound)
            {
                Console.WriteLine("Not found");
            }
        }

        public void data_validation()     //10
        {
            Console.WriteLine("Enter the name: ");
            String name = Console.ReadLine();
            if (Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
            {
                Console.WriteLine("Valid Name");
            }
            else
            {
                Console.WriteLine("Invalid name");
            }

            Console.WriteLine("Enter the Address: ");
            String address = Console.ReadLine();
            if (Regex.IsMatch(address, @"^[a-zA-Z0-9\s,.-]+$"))
            {
                Console.WriteLine("Valid Address");
            }
            else
            {
                Console.WriteLine("Invalid Address");
            }

            Console.WriteLine("Enter the Phone_number: ");
            String Phone_number = Console.ReadLine();
            if (Regex.IsMatch(Phone_number, @"^\d{3}-\d{3}-\d{4}$"))
            {
                Console.WriteLine("Valid Phone Number");
            }
            else
            {
                Console.WriteLine("Invalid number");
            }
        }

        public void address_formating()       //11
        {
            Console.WriteLine("Enter street address:");
            string street = Console.ReadLine();

            Console.WriteLine("Enter city:");
            string city = Console.ReadLine();

            Console.WriteLine("Enter state:");
            string state = Console.ReadLine();

            Console.WriteLine("Enter zip code:");
            string zipCode = Console.ReadLine();

            string formattedStreet = string.Join(" ", Array.ConvertAll(street.Split(' '), word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));

            string formattedCity = char.ToUpper(city[0]) + city.Substring(1).ToLower();

            string formattedState = state.ToUpper();

            string formattedZipCode = zipCode.Length == 9
                ? zipCode.Substring(0, 5) + "-" + zipCode.Substring(5)
                : zipCode;

            string formattedAddress = $"{formattedStreet}, {formattedCity}, {formattedState} {formattedZipCode}";

            Console.WriteLine("Formatted Address:");
            Console.WriteLine(formattedAddress);
        }

        public void order_Cnfrmation()    //12
        {
            Console.WriteLine("Enter customer's name:");
            string customerName = Console.ReadLine();

            Console.WriteLine("Enter order number:");
            string orderNumber = Console.ReadLine();

            Console.WriteLine("Enter delivery address:");
            string deliveryAddress = Console.ReadLine();

            DateTime expectedDeliveryDate = DateTime.Today.AddDays(2);

            string emailContent = $@"
        Subject: Order Confirmation - Order #{orderNumber}

        Dear {customerName},

        Thank you for placing an order with us! Your order #{orderNumber} has been confirmed.

        Order Details:
        - Order Number: {orderNumber}
        - Delivery Address: {deliveryAddress}
        - Expected Delivery Date: {expectedDeliveryDate:yyyy-MM-dd}

        If you have any questions or concerns, please feel free to contact our customer support.

        Thank you for choosing our service!

        Best regards,
        Your Company Name
        ";
            Console.WriteLine("\nOrder Confirmation Email:");
            Console.WriteLine(emailContent);
        }

        public void calulating_cost() ///13
        {
            Console.WriteLine("Enter source address:");
            string sourceAddress = Console.ReadLine();

            Console.WriteLine("Enter destination address:");
            string destinationAddress = Console.ReadLine();

            Console.WriteLine("Enter parcel weight in kilograms:");
            double parcelWeight = Convert.ToDouble(Console.ReadLine());

            const double BASE_COST = 5.0;
            const double DISTANCE_COST_FACTOR = 0.1;
            const double WEIGHT_COST_FACTOR = 0.2;

            double distanceKm = 50.0;

            double distanceCost = distanceKm * DISTANCE_COST_FACTOR;
            double weightCost = parcelWeight * WEIGHT_COST_FACTOR;
            double totalCost = BASE_COST + distanceCost + weightCost;

            Console.WriteLine($"\nThe estimated shipping cost is: ${totalCost}");
        }
        public void genertor()
        {
            Console.Write("Enter desired password length: ");
            int length = Convert.ToInt32(Console.ReadLine());

            string digits = "0123456789";
            string special = "!@#$%^&*";
            string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lower = "abcdefghijklmnopqrstuvwxyz";

            string allCharacters = upper + lower + digits + special;
            Random rand = new Random();
            string password = "";

            for (int i = 0; i < length; i++)
            {
                int index = rand.Next(allCharacters.Length);
                password += allCharacters[index];
            }

            Console.WriteLine("Generated Password: " + password);
        }

        public void similar_address()
        {
            List<string> addresses = new List<string>
            {
                   "101 Vivekananda Street",
                   "101 Vivekananda St",
                   "200 Tagore Road",
                   "200 Tagore Rd",
                   "305 Bose Nagar",
                   "305 Bose Ngr",
                   "400 Netaji Avenue",
                   "401 Netaji Avenue"

            };

            //Console.WriteLine("Similar addresses found:\n");

            for (int i = 0; i < addresses.Count; i++)
            {
                for (int j = i + 1; j < addresses.Count; j++)
                {
                    string addr1 = addresses[i].ToLower();
                    string addr2 = addresses[j].ToLower();

                    if (addr1.Contains(addr2) || addr2.Contains(addr1))
                    {
                        Console.WriteLine($"Similar addresses are :\n\n \"{addresses[i]}\" \nand\n \"{addresses[j]}\"\n");
                    }
                }
            }
        }
    }
}
