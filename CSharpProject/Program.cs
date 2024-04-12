using CSharpProject;
using System.Runtime.CompilerServices;

List<string> projects = new List<string>() { "Study", "Dinner", "Summer Hike" };

string file = @"C:\Dev\CSharp\Todo\data.txt";

List<TodoTask> tasks = new List<TodoTask>();

//Read whole file line by line and add tasks to list. Assumes the "attributes" are separated with a comma.
if (File.Exists(file))
{
	foreach (string line in File.ReadAllLines(file))
	{
        string[] attributes = line.Split(",");
        string[] date = attributes[2].Split("-");
		tasks.Add(new TodoTask(attributes[0], new DateOnly(Convert.ToInt32(date[0]), Convert.ToInt32(date[1]), Convert.ToInt32(date[2])), attributes[1]));
	}
} 

//Used as argument for the Menu
string[] optionsMenu = ["Show task list (by date or project)", "Add new task", "Edit task (update, mark as done, remove)", "Save and quit"];
string[] optionsEdit = ["Update", "Mark as done", "Remove", "Go to main menu"];

//Prints a welcome message with number of tasks left to do and tasks completed
Menu.Welcome(tasks.Where(t => t.IsCompleted == false).Count(), tasks.Where(t => t.IsCompleted == true).Count());

//Main menu
bool run = true;
while (run)
{
    string command = Menu.GetCommand(optionsMenu);
    switch (command)
	{
		case "1":
			Menu.PrintAll(tasks.OrderBy(t => t.Date).ThenBy(t => t.Project).ToList());
			break;
		case "2":
			AddTask();
			Menu.PrintAdded();
			break;
		case "3":
			EditTask();
			break;
		case "4":
            SaveToFile();
			Menu.Goodbye();
			run = false;
            break;
		default:
			Menu.PrintError(command);
			break;
	}
}

//Functions for adding, editing and saving to file
void AddTask()
{
    string title = Menu.GetTitle();
    while (true)
	{
		string project = Menu.GetProject(projects);
		if (!projects.Any(p => p.Equals(project))) {  //If the project doesn't exist
			Menu.PrintError("Please choose an existing project.");
		} else
		{
            tasks.Add(new TodoTask(title, DateOnly.FromDateTime(DateTime.Now), project));
			break;
        }
	}
}

void EditTask()
{
	bool run = true;
	while (run)
	{
		string command = Menu.GetCommand(optionsEdit);
        
        switch (command)
		{
            case "1":
                Menu.PrintAll(tasks);
                UpdateTask();
                run = false;
				break;
			case "2":
                Menu.PrintAll(tasks);
                MarkAsDone();
                run = false;
                break;
			case "3":
                Menu.PrintAll(tasks);
                RemoveTask();
                run = false;
                break;
			case "4":
				run = false;
				break;
			default:
				Menu.PrintError(command);
				break;
		}
	}
}

//Not done 
void UpdateTask()
{
    //string title = Menu.GetTitle();

}

//Annoyingly similar to RemoveTask() 
void MarkAsDone()
{
    string title = Menu.GetTitle();
    TodoTask task;
    bool run = true;
    while (run)
    {
        try
        {
            task = tasks.Single(t => t.Title == title);
            task.IsCompleted = true;
            run = false;
        }
        catch (InvalidOperationException)
        {
            Menu.PrintError("Please choose an existing task, or press 'q' to go to menu.");
            title = Menu.GetTitle();
            if (title == "q")
            {
                run = false;
            }
        }
    }
}

void RemoveTask()
{
    string title = Menu.GetTitle();
	TodoTask task;
    bool run = true;
	while (run)
	{
        try  
        {
			task = tasks.Single(t => t.Title == title);
            tasks.Remove(task);
			run = false;
        }
        catch (InvalidOperationException)
        {
            Menu.PrintError("Please choose an existing task. Press 'q' to go to menu.");
            title = Menu.GetTitle();
			if (title == "q")
			{
				run = false;
			}
        }
    }
}

void SaveToFile()
{
	File.Delete(file);  //We know the file exists here, so it can just be deleted
    foreach (var t in tasks)
	{
        File.AppendAllText(file, t.ToCSV() + "\n");  //Creates a new file and appends to it
    }
    
}