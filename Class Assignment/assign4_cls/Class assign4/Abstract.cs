using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_assign4
{

    abstract class Furniture
    {
        public string ? Material;
        public double ? Price;

        public abstract void DisplayDetails();
    }

    class Chair : Furniture
    {
        public int NumberOfLegs;
        public void SetDetails(string material, double price, int numberOfLegs)
        {
            Material = material;
            Price = price;
            NumberOfLegs = numberOfLegs;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine($"Chair Details:\nMaterial: {Material},Price: {Price}, Legs: {NumberOfLegs}");
        }
    }

    class Bookshelf : Furniture
    {
        public int NumberOfShelves;

        public void SetDetails(string material, double price, int numberOfShelves)
        {
            Material = material;
            Price = price;
            NumberOfShelves = numberOfShelves;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine($"Bookshelf Details:\nMaterial: {Material},Price: {Price}, Shelves: {NumberOfShelves}");
        }
    }
}
