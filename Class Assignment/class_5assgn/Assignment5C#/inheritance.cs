using System;

namespace Assignment5C_
{
    class Student
    {
        protected int rollNo;
        protected string name;
        protected string studentClass;
        protected int semester;
        protected string branch;
        protected int[] marks=new int[5];

        public Student(int rollNo, string name, string studentClass, int semester, string branch)
        {
            this.rollNo = rollNo;
            this.name = name;
            this.studentClass = studentClass;
            this.semester = semester;
            this.branch = branch;
            //this.marks = marks;
        }
        public void GetMarks()
        {
            Console.WriteLine("Enter the marks:");
            for(int i = 0;i<marks.Length;i++)
            {
                marks[i] = int.Parse(Console.ReadLine());

            }
        }

        public  virtual void DisplayResult()
        {
            int sum = 0;
            bool hasFailedSubject = false;

            for(int i= 0;i< marks.Length;i++)
            {
                if (marks[i] < 35)
                {
                    hasFailedSubject = true;
                }
                sum += marks[i];
            }

            double average = sum / 5.0;

            Console.WriteLine("\n----- Student Result -----");
            if (hasFailedSubject)
            {
                Console.WriteLine("Result: Failed (One or more subjects below 35)");
            }
            else if (average < 50)
            {
                Console.WriteLine("Result: Failed (Average < 50)");
            }
            else
            {
                Console.WriteLine("Result: Passed");
            }
            Console.WriteLine($"Average Marks: {average}");
        }
        public void DisplayData()
        {
            Console.WriteLine($"Student Detials : Roll No : {rollNo} , Name : {name} , StudentClass : {studentClass} , Semester : {semester} , Branch : {branch}");
            Console.WriteLine("Marks : ");
            foreach(var mark in marks)
            {
                Console.WriteLine(mark + " ");
            }
        }
    }
    class GraduateStudent : Student
    {
        public GraduateStudent(int rollNo, string name, string studentClass, int semester, string branch)
            : base(rollNo, name, studentClass, semester, branch) {}

        public override void DisplayResult()
        {
            base.DisplayResult();
        }
    }
    /*class inheritance
    {
        static void Main(string[] args)
        {
            Student student = new Student(101, "John Doe", "10th Grade", 2, "Science");

            student.GetMarks();

            student.DisplayResult();

            student.DisplayData();

            Console.WriteLine("\n-------- Graduate Student --------");

            GraduateStudent gradStudent = new GraduateStudent(201, "Jane Smith", "12th Grade", 4, "Arts");
            gradStudent.GetMarks();
            gradStudent.DisplayResult();
            gradStudent.DisplayData();
        }
    }*/
}
