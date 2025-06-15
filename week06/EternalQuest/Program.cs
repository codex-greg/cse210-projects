using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        List<Goal> goals = new List<Goal>();
        int totalPoints = 0;

        // Load goals from file if exists
        LoadGoals(goals);

        while (true)
        {
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Show Goals");
            Console.WriteLine("4. Show Total Points");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoal(goals);
                    break;
                case "2":
                    totalPoints += RecordEvent(goals);
                    break;
                case "3":
                    ShowGoals(goals);
                    break;
                case "4":
                    Console.WriteLine($"Total Points: {totalPoints}");
                    break;
                case "5":
                    SaveGoals(goals);
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void CreateGoal(List<Goal> goals)
    {
        Console.Write("Enter goal type (simple, eternal, checklist): ");
        string type = Console.ReadLine();
        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter goal description: ");
        string description = Console.ReadLine();
        Console.Write("Enter points: ");
        int points = int.Parse(Console.ReadLine());

        if (type.ToLower() == "simple")
        {
            goals.Add(new SimpleGoal(name, description, points));
        }
        else if (type.ToLower() == "eternal")
        {
            goals.Add(new EternalGoal(name, description, points));
        }
        else if (type.ToLower() == "checklist")
        {
            Console.Write("Enter required times: ");
            int requiredTimes = int.Parse(Console.ReadLine());
            Console.Write("Enter bonus points: ");
            int bonusPoints = int.Parse(Console.ReadLine());
            goals.Add(new ChecklistGoal(name, description, points, requiredTimes, bonusPoints));
        }
        else
        {
            Console.WriteLine("Invalid goal type.");
        }
    }

    static int RecordEvent(List<Goal> goals)
    {
        Console.Write("Enter the index of the goal to record an event: ");
        int index = int.Parse(Console.ReadLine());
        if (index >= 0 && index < goals.Count)
        {
            return goals[index].RecordEvent();
        }
        Console.WriteLine("Invalid index.");
        return 0;
    }

    static void ShowGoals(List<Goal> goals)
    {
        foreach (var goal in goals)
        {
            Console.WriteLine(goal.GetGoalStatus());
        }
    }

    static void SaveGoals(List<Goal> goals)
    {
        using (StreamWriter outputFile = new StreamWriter("goals.txt"))
        {
            foreach (var goal in goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }
    }

    static void LoadGoals(List<Goal> goals)
    {
        if (File.Exists("goals.txt"))
        {
            string[] lines = File.ReadAllLines("goals.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(",");
                string type = parts[0];

                if (type == "SimpleGoal")
                {
                    goals.Add(new SimpleGoal(parts[1], parts[2], int.Parse(parts[3])));
                }
                else if (type == "EternalGoal")
                {
                    goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));
                }
                else if (type == "ChecklistGoal")
                {
                    goals.Add(new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5])));
                }
            }
        }
    }
}
