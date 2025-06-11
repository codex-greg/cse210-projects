using System;

namespace InheritanceAssignment
{
    // Base class
    public class Assignment
    {
        private string _studentName;
        private string _topic;

        public Assignment(string studentName, string topic)
        {
            _studentName = studentName;
            _topic = topic;
        }

        public string GetSummary()
        {
            return $"{_studentName} - {_topic}";
        }

        //Option 1: Make the variable protected
        protected string GetStudentName()
        {
            return _studentName;
        }

        //Option 2: Create a public getter method
        public string StudentName
        {
            get { return _studentName; }
        }
    }

    // Derived class: MathAssignment
    public class MathAssignment : Assignment
    {
        private string _textbookSection;
        private string _problems;

        public MathAssignment(string studentName, string topic, string textbookSection, string problems)
            : base(studentName, topic)
        {
            _textbookSection = textbookSection;
            _problems = problems;
        }

        public string GetHomeworkList()
        {
            return $"Section {_textbookSection} Problems {_problems}";
        }
    }

    // Derived class: WritingAssignment
    public class WritingAssignment : Assignment
    {
        private string _title;

        public WritingAssignment(string studentName, string topic, string title)
            : base(studentName, topic)
        {
            _title = title;
        }

        public string GetWritingInformation()
        {
            //Option 1: Use the protected getter method
            //return $"{_title} by {GetStudentName()}";

            //Option 2: Use the public getter property
            return $"{_title} by {StudentName}";
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Test Assignment class
            Assignment assignment = new Assignment("Samuel Bennett", "Multiplication");
            Console.WriteLine(assignment.GetSummary()); // Output: Samuel Bennett - Multiplication
            Console.WriteLine();

            // Test MathAssignment class
            MathAssignment mathAssignment = new MathAssignment("Roberto Rodriguez", "Fractions", "7.3", "8-19");
            Console.WriteLine(mathAssignment.GetSummary()); // Output: Roberto Rodriguez - Fractions
            Console.WriteLine(mathAssignment.GetHomeworkList()); // Output: Section 7.3 Problems 8-19
            Console.WriteLine();

            // Test WritingAssignment class
            WritingAssignment writingAssignment = new WritingAssignment("Mary Waters", "European History", "The Causes of World War II");
            Console.WriteLine(writingAssignment.GetSummary()); // Output: Mary Waters - European History
            Console.WriteLine(writingAssignment.GetWritingInformation()); // Output: The Causes of World War II by Mary Waters
        }
    }
}
