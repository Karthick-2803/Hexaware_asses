using System;

namespace Assignment5C_
{
    interface IStudent
    {
        int StudentId { get; set; }
        string Name { get; set; }
        double Fees { get; set; }

        void ShowDetails();
    }


    class DayScholar : IStudent
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public double Fees { get; set; }

        public DayScholar(int studentId, string name, double fees)
        {
            StudentId = studentId;
            Name = name;
            Fees = fees;
        }

        public void ShowDetails()
        {
            Console.WriteLine($"DayScholar Details:: Student Id: {StudentId} , Student Name: {Name} , Fees: {Fees}");
        }
    }
    class Resident : IStudent
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public double Fees { get; set; }

        public double AccomodationFees { get; set; }

        public Resident(int studentId, string name, double fees, double accomodationFees)
        {
            StudentId = studentId;
            Name = name;
            Fees = fees;
            AccomodationFees = accomodationFees;
        }

        public void ShowDetails()
        {
            Console.WriteLine($"Resident DetailsStudent Id: {StudentId} , Student Name: {Name} , Fees: {Fees} , Accomodation Fees: {AccomodationFees}");
        }

    }
    /*internal class Interface
    {
        public static void Main()
        {
            DayScholar dayScholar = new DayScholar(101, "Alice", 5000);
            dayScholar.ShowDetails();

            Resident resident = new Resident(102, "Bob", 7000, 3000);
            resident.ShowDetails();
        }
    }*/
}
