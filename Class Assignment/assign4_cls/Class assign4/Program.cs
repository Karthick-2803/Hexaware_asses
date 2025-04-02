namespace Class_assign4
{
    class TimePeriod
    {
        private double minutes;

        public double Hours
        {
            get { return minutes/60; }
            set { minutes = value * 60; }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            TimePeriod time=new TimePeriod();
            time.Hours = 1.5;
            Console.WriteLine($"\nTime in Hours: {time.Hours} hrs");
            Console.WriteLine($"Time in Minutes: {time.Hours * 60} min");


            Console.WriteLine("--------Abstract class------------");
            Chair chair = new Chair();
            chair.SetDetails("Wood", 2500, 4);

            Bookshelf bookshelf = new Bookshelf();
            bookshelf.SetDetails("Metal", 5000, 5);




            Console.WriteLine("=== Furniture Details ===");
            chair.DisplayDetails();
            bookshelf.DisplayDetails();
        }

    }
}
