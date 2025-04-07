using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample_project.model
{
    public class Employee
    {
            public int EmployeeID { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string ContactNumber { get; set; }
            public string Role { get; set; }
            public double Salary { get; set; }

         public Employee() { }
        public Employee(int employeeID, string Name, string email, string contactNumber, string role, double salary)
            {
                EmployeeID = employeeID;
                Name = Name;
                Email = email;
                ContactNumber = contactNumber;
                Role = role;
                Salary = salary;
            }
            public override string ToString()
            {
                return $"Employee ID: {EmployeeID}, Name: {Name}, Role: {Role}, Salary: {Salary}";
            }
     }
}
