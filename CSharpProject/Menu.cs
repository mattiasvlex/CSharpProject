using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProject
{
    internal class Menu
    {
        public static void Welcome(int tasksTodo, int tasksDone) 
        {
            Console.WriteLine($"Welcome to Todo list\nYou have {tasksTodo} tasks to do and {tasksDone} tasks are done");
        }
        public static void Quit()
        {
            Console.WriteLine("Thank you for today!");
        }

        public static void SaveAndQuit()
        {
            Console.WriteLine("The tasks are saved. Thank you for today!");
        }

        public static string GetCommand(string[] options)
        {
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"({i+1}) {options[i]}");
            }
            Console.Write("Pick an option: ");
            string? command = Console.ReadLine();
            Console.Clear();
            return command ?? "";
        }

        public static string GetTitle()
        {
            Console.Write("Enter title: ");
            string? title = Console.ReadLine();
            return title ?? "";
        }

        public static string GetProject()
        {
            Console.Write("Enter project: ");
            string? project = Console.ReadLine();
            return project ?? "";
        }

        public static string GetDate()
        {
            Console.Write("Enter due date (yyyy-mm-dd): ");
            string? date = Console.ReadLine();
            return date ?? "";
        } 

        public static string GetUpdate(string[] options, string task)
        {
            Console.WriteLine($"\nUpdate any of the following for task {task}:");
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"({i + 1}) {options[i]}");
            }
            Console.Write("Pick an option: ");
            string? command = Console.ReadLine();
            return command ?? "";
        }
        public static void MarkedAsDone(string task)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Task {task} is now marked as completed.");
            Console.ResetColor();
            Console.WriteLine("\nPress enter to go to back to main menu.");
            Console.ReadLine();
            Console.Clear();
        }

        public static void Removed(string task)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Task {task} was successfully removed.");
            Console.ResetColor();
            Console.WriteLine("\nPress enter to go to back to main menu.");
            Console.ReadLine();
            Console.Clear();
        }

        public static void TitleUpdated(string taskOld, string taskNew)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Task title {taskOld} was successfully updated to {taskNew}.");
            Console.ResetColor();
        }

        public static void ProjectUpdated(string task, string newProject)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Project for task {task} was successfully updated to {newProject}.");
            Console.ResetColor();
        }

        public static void DateUpdated(string task, string date)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Due date for task {task} was successfully updated to {date}.");
            Console.ResetColor();
        }
        public static void BackToMenu()
        {
            Console.Clear();
        }
        public static void PrintAdded()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The task was added successfully");
            Console.ResetColor();
            Console.WriteLine("\nPress enter to go to back to main menu.");
            Console.ReadLine();
            Console.Clear();
        }

        public static void PrintError(string error)
        {
            Console.WriteLine("Invalid input: " + error);
        }

        public static void PrintAll(List<TodoTask> tasks)
        {
            Console.WriteLine("Title".PadRight(15) + "Project".PadRight(15) + "Due date".PadRight(15) + "Status".PadRight(15));
            Console.WriteLine("-----".PadRight(15) + "-------".PadRight(15) + "--------".PadRight(15) + "------".PadRight(15));
            foreach (var t in tasks)
            {
                if (t.Date.CompareTo(DateOnly.FromDateTime(DateTime.Now)) < 0 && !t.IsCompleted)  //If the due date is earlier than today and task is not completed
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(t);
                    Console.ResetColor();
                } 
                else
                {
                    Console.WriteLine(t);
                }                
            }
            Console.WriteLine();
        }
        
    }
}
