using System;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        string connectionString = "Data Source=journal.db";
        bool running = true;

        while (running)
        {
            Console.WriteLine("Journal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load journal from file");
            Console.WriteLine("5. Save journal to database");
            Console.WriteLine("6. Load journal from database");
            Console.WriteLine("7. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter your response: ");
                    string response = Console.ReadLine();
                    Console.WriteLine("Select an emotion:");
                    string[] emotions = journal.GetEmotions();
                    for (int i = 0; i < emotions.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {emotions[i]}");
                    }
                    int emotionChoice = int.Parse(Console.ReadLine()) - 1;
                    journal.AddEntry(response, emotions[emotionChoice]);
                    break;
                case "2":
                    journal.DisplayEntries();
                    break;
                case "3":
                    Console.Write("Enter filename to save: ");
                    string saveFilename = Console.ReadLine();
                    journal.SaveToFile(saveFilename);
                    break;
                case "4":
                    Console.Write("Enter filename to load: ");
                    string loadFilename = Console.ReadLine();
                    journal.LoadFromFile(loadFilename);
                    break;
                case "5":
                    journal.SaveToDatabase(connectionString);
                    break;
                case "6":
                    journal.LoadFromDatabase(connectionString);
                    break;
                case "7":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
