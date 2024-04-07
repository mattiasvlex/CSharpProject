using CSharpProject;
using System.Runtime.CompilerServices;

List<string> projects = new List<string>() { "Study", "Dinner", "Summer Hike" };

string file = "";

//File.AppendAllText(file, "\n" + tasks[2].ToCSV());

List<TodoTask> tasks = new List<TodoTask>();

if (File.Exists(file))
{
	foreach (string line in File.ReadAllLines(file))
	{
		Console.WriteLine(line);
		string[] attributes = line.Split(",");
		string[] date = attributes[2].Split("-");
		tasks.Add(new TodoTask(attributes[0], new DateOnly(Convert.ToInt32(date[0]), Convert.ToInt32(date[1]), Convert.ToInt32(date[2])), attributes[1]));
	}
}


string[] options = ["Show task list (by date or project)", "Add new task", "Edit task (update, mark as done, remove)", "Save and quit"];

Menu.Welcome(2, 1);
Menu.GetCommand(options);
Menu.PrintAll(tasks);

