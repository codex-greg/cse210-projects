using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            // Menu and program logic will go here
            DisplayMenu();
            int choice = GetActivityChoice();

            if (choice >= 1 && choice <= 3)
            {
                int duration = GetDurationFromUser();

                if (duration > 0)
                {
                    Activity activity = null;

                    switch (choice)
                    {
                        case 1:
                            activity = new BreathingActivity(duration);
                            break;
                        case 2:
                            activity = new ReflectingActivity(duration);
                            break;
                        case 3:
                            activity = new ListingActivity(duration);
                            break;
                    }

                    activity.Run();
                }
                else
                {
                    Console.WriteLine("Invalid duration. Please enter a positive number.");
                }
            }
            else if (choice != 4)
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }

            Console.WriteLine("Thank you for using the Mindfulness Program!");
        }

        static void DisplayMenu()
        {
            Console.WriteLine("Mindfulness Program Menu:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflecting Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.WriteLine("Enter your choice (1-4): ");
        }

        static int GetActivityChoice()
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 4:");
                DisplayMenu();
            }
            return choice;
        }

        static int GetDurationFromUser()
        {
            int duration;
            Console.Write("Enter the duration in seconds: ");
            while (!int.TryParse(Console.ReadLine(), out duration))
            {
                Console.WriteLine("Invalid input. Please enter a number:");
                Console.Write("Enter the duration in seconds: ");
            }
            return duration;
        }
    }

    class Activity
    {
        private string _name;
        private string _description;
        private int _duration;

        public Activity(string name, string description, int duration)
        {
            _name = name;
            _description = description;
            _duration = duration;
        }

        public string GetName() { return _name; }
        public string GetDescription() { return _description; }
        public int GetDuration() { return _duration; }

        public void DisplayStartingMessage()
        {
            Console.WriteLine($"Starting {_name} Activity");
            Console.WriteLine(_description);
            Console.WriteLine($"Prepare to begin in a few seconds...");
            ShowSpinner(3);
        }

        public void DisplayEndingMessage()
        {
            Console.WriteLine("Good job!");
            ShowSpinner(3);
            Console.WriteLine($"You have completed the {_name} Activity for {_duration} seconds.");
            ShowSpinner(3);
        }

        public void ShowSpinner(int seconds)
        {
            List<string> spinnerChars = new List<string> { "|", "/", "-", "\\" };
            DateTime startTime = DateTime.Now;
            DateTime futureTime = startTime.AddSeconds(seconds);
            int i = 0;

            while (DateTime.Now < futureTime)
            {
                Console.Write(spinnerChars[i]);
                Thread.Sleep(200);
                Console.Write("\b \b");
                i = (i + 1) % spinnerChars.Count;
            }
        }

        public void ShowCountDown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
        }

        public virtual void Run()
        {
            // To be overridden by derived classes
        }
    }

    class BreathingActivity : Activity
    {
        public BreathingActivity(int duration) : base("Breathing", "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.", duration)
        {
        }

        public override void Run()
        {
            DisplayStartingMessage();

            DateTime startTime = DateTime.Now;
            DateTime futureTime = startTime.AddSeconds(GetDuration());

            while (DateTime.Now < futureTime)
            {
                Console.WriteLine("Breathe in...");
                ShowCountDown(3);
                Console.WriteLine("Breathe out...");
                ShowCountDown(3);
            }

            DisplayEndingMessage();
        }
    }

    class ReflectingActivity : Activity
    {
        private List<string> _prompts;
        private List<string> _questions;
        private List<string> _usedPrompts;
        private List<string> _usedQuestions;

        public ReflectingActivity(int duration) : base("Reflecting", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.", duration)
        {
            _prompts = new List<string>()
            {
                "Think of a time when you stood up for someone else.",
                "Think of a time when you did something really difficult.",
                "Think of a time when you helped someone in need.",
                "Think of a time when you did something truly selfless."
            };

            _questions = new List<string>()
            {
                "Why was this experience meaningful to you?",
                "Have you ever done anything like this before?",
                "How did you get started?",
                "How did you feel when it was complete?",
                "What made this time different than other times when you were not as successful?",
                "What is your favorite thing about this experience?",
                "What could you learn from this experience that applies to other situations?",
                "What did you learn about yourself through this experience?",
                "How can you keep this experience in mind in the future?"
            };

            _usedPrompts = new List<string>();
            _usedQuestions = new List<string>();
        }

        private string GetUniquePrompt()
        {
            if (_usedPrompts.Count == _prompts.Count)
            {
                _usedPrompts.Clear(); // Reset if all prompts have been used
            }

            Random random = new Random();
            string prompt;
            do
            {
                prompt = _prompts[random.Next(_prompts.Count)];
            } while (_usedPrompts.Contains(prompt));

            _usedPrompts.Add(prompt);
            return prompt;
        }

        private string GetUniqueQuestion()
        {
            if (_usedQuestions.Count == _questions.Count)
            {
                _usedQuestions.Clear(); // Reset if all questions have been used
            }

            Random random = new Random();
            string question;
            do
            {
                question = _questions[random.Next(_questions.Count)];
            } while (_usedQuestions.Contains(question));

            _usedQuestions.Add(question);
            return question;
        }

        public override void Run()
        {
            DisplayStartingMessage();

            string prompt = GetUniquePrompt();
            Console.WriteLine(prompt);

            DateTime startTime = DateTime.Now;
            DateTime futureTime = startTime.AddSeconds(GetDuration());

            while (DateTime.Now < futureTime)
            {
                string question = GetUniqueQuestion();
                Console.WriteLine(question);
                ShowSpinner(5);
            }

            DisplayEndingMessage();
        }
    }

    class ListingActivity : Activity
    {
        private List<string> _prompts;
        private int _count;
        private List<string> _usedPrompts;

        public ListingActivity(int duration) : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.", duration)
        {
            _prompts = new List<string>()
            {
                "Who are people that you appreciate?",
                "What are personal strengths of yours?",
                "Who are people that you have helped this week?",
                "When have you felt the Holy Ghost this month?",
                "Who are some of your personal heroes?"
            };
            _count = 0;
            _usedPrompts = new List<string>();
        }

        private string GetUniquePrompt()
        {
            if (_usedPrompts.Count == _prompts.Count)
            {
                _usedPrompts.Clear(); // Reset if all prompts have been used
            }

            Random random = new Random();
            string prompt;
            do
            {
                prompt = _prompts[random.Next(_prompts.Count)];
            } while (_usedPrompts.Contains(prompt));

            _usedPrompts.Add(prompt);
            return prompt;
        }

        public override void Run()
        {
            DisplayStartingMessage();

            string prompt = GetUniquePrompt();
            Console.WriteLine(prompt);
            Console.WriteLine("Start listing items. Press Enter after each item.");
            ShowCountDown(5);

            DateTime startTime = DateTime.Now;
            DateTime futureTime = startTime.AddSeconds(GetDuration());

            while (DateTime.Now < futureTime)
            {
                Console.Write("> ");
                Console.ReadLine();
                _count++;
            }

            Console.WriteLine($"You listed {_count} items.");
            DisplayEndingMessage();
        }
    }
}
