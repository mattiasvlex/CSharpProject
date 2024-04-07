using System;
using System.Collections.Generic;
using System.Linq;
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

        public static string GetCommand(string[] options)
        {
            Console.WriteLine("\nPick an option: ");
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"({i+1}) {options[i]}");
            }
            
            string? command = Console.ReadLine();
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
            Console.Write("Enter project name: ");
            string? project = Console.ReadLine();
            return project ?? "";
        }

        public static void PrintError(string error)
        {
            Console.WriteLine("Invalid input: " + error);
        }

        public static void PrintAll(List<TodoTask> tasks)
        {
            Console.WriteLine();
            foreach (var t in tasks)
            {
                Console.WriteLine(t);
            }
        }
    }
}
