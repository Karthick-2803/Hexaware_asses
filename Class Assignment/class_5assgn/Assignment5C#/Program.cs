namespace Assignment5C_
{
    internal class Program
    {
        /*static void Main(string[] args)
        {
            Program program = new Program();
            //program.length();
            //program.reverse();
            program.equals();
        }*/

        public void length()
        {
            Console.WriteLine("Enter the word:");
            string ? word = Console.ReadLine();

            Console.WriteLine($"The length of the word : {word.Length}");
        }

        public void reverse()
        {
            Console.WriteLine("Enter the name:");
            string ? name= Console.ReadLine();
            char[] charArray = name.ToCharArray();
            Array.Reverse( charArray );
            string reversedname = new string(charArray);
            Console.WriteLine($"The reversed name is : {reversedname}");
        }

        public void equals()
        {
            Console.WriteLine("Enter the word1:");
            string ? Word1 = Console.ReadLine();

            Console.WriteLine("Enter the word2:");
            string ? Word2 = Console.ReadLine();

            if (Word1.Equals(Word2, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("They are same..");
            }
            else
            {
                Console.WriteLine("Not same");

            }
        }
    }
}
