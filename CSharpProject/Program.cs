using CSharpProject;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

string file = @"C:\Users\Mattias\Skola\Lexicon\C-Sharp\source\CSharpProject\data.txt";

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

//Used as argument for the Menus
string[] optionsMain = ["Show task list (by date or project)", "Add new task", "Edit task (update, mark as done, remove)", "Save and quit", "Quit without saving"];
string[] optionsPrint = ["Sort by date", "Sort by project", "Show unsorted", "Go to main menu"];
string[] optionsEdit = ["Update task", "Mark a task as done", "Remove task", "Go to main menu"];
string[] optionsUpdate = ["Title", "Project", "Due date", "Go to main menu"];

//Main menu
//Prints a welcome message with number of tasks left to do and tasks completed, and then asks the user for input
bool run = true;
while (run)
{
    Menu.Welcome(tasks.Where(t => t.IsCompleted == false).Count(), tasks.Where(t => t.IsCompleted == true).Count());
    string command = Menu.GetCommand(optionsMain);
    switch (command)
	{
		case "1":
            PrintTasks();
			break;
		case "2":
			AddTask();
			break;
		case "3":
			EditTask();
			break;
		case "4":
            SaveToFile();
			Menu.SaveAndQuit();
			run = false;
            break;
        case "5":
            Menu.Quit();
            run = false;
            break;
		default:
			break;
	}
}

//Prints all tasks in 3 different ways, ordered by date, project, or unordered
void PrintTasks()
{
    bool run = true;
    while (run)
    {
        string command = Menu.GetCommand(optionsPrint);
        switch (command)
        {
            case "1":
                Menu.PrintAll(tasks.OrderBy(t => t.Date).ToList());
                break;
            case "2":
                Menu.PrintAll(tasks.OrderBy(t => t.Project).ToList());
                break;
            case "3":
                Menu.PrintAll(tasks);
                break;
            case "4":
                run = false;
                break;
            default:
                break;
        }
    }
}

//Adds a task to the list. Checks the date format before adding, but not title and project name.
void AddTask()
{
    string title = Menu.GetTitle();
    string project = Menu.GetProject();
    string date;
    string[] parts;
    while (true)
    {
        date = Menu.GetDate();
        parts = date.Split("-");
        if (date == "m")  //If the user wants to go back to the menu
        {
            Menu.BackToMenu();
            break;
        }
        else if (ParseDate(parts)) //If the date has the correct format
        {
            tasks.Add(new TodoTask(title, new DateOnly(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2])), project));
            Menu.PrintAdded();
            break;
        }
    }
}

//Asks the user to specify what type of edit they want to do
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
				break;
		}
	}
}

//Submenu to EditTask
void UpdateTask()
{
    string title = Menu.GetTitle();  //Ask user for the task title
    TodoTask? task = GetTask(title);
    if (task != null)  //If the task exists
    {
        bool run = true;
        while (run)
        {
            string command = Menu.GetUpdate(optionsUpdate, task.Title);  //Ask user for which update(s) they want to do
            switch (command)
            {
                case "1":
                    string changeToTitle = Menu.GetTitle();
                    Menu.TitleUpdated(task.Title, changeToTitle);
                    task.Title = changeToTitle;
                    break;
                case "2":
                    string changeToProject = Menu.GetProject();
                    Menu.ProjectUpdated(task.Title, changeToProject);
                    task.Project = changeToProject;
                    break;
                case "3":
                    string date;
                    string[] parts;
                    while (true)
                    {
                        date = Menu.GetDate();
                        parts = date.Split('-');
                        if (date == "m")
                        {
                            Menu.BackToMenu();
                            break;
                        } else if (ParseDate(parts))
                        {
                            task.Date = new DateOnly(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2]));
                            Menu.DateUpdated(task.Title, date);
                            break;
                        }
                    }
                    break;
                case "4":
                    Menu.BackToMenu();
                    run = false;
                    break;
                default: 
                    break;
            }
        }
    }
}

//Uses help function GetTask to retrieve a task if it exists, and sets IsCompleted to true.
void MarkAsDone()
{
    string title = Menu.GetTitle();
    TodoTask? task = GetTask(title);
    if (task != null)
    {
        task.IsCompleted = true;
        Menu.MarkedAsDone(task.Title);
    }
}

//Uses help function GetTask to retrieve a task if it exist, and removes it from the list.
void RemoveTask()
{
    string title = Menu.GetTitle();
	TodoTask? task = GetTask(title);
    if (task != null)
    {
        Menu.Removed(task.Title);
        tasks.Remove(task);
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

//Help function to match a task title with a task in the list
TodoTask? GetTask(string title)
{
    string title2 = title;
    while (true)
    {
        try
        {
            TodoTask task = tasks.Single(t => t.Title == title2);  //Searches the list after the specified title
            return task;
        }
        catch (Exception)
        {
            Menu.PrintError("Please choose an existing task. Press 'm' to go back to the menu.");
            title2 = Menu.GetTitle();
            if (title2 == "m")
            {
                Menu.BackToMenu();
                return null;
            }
        }
    }
}

//Help function to parse a date entered by the user. Returns true if the date was parsed correctly, false if not.
bool ParseDate(string[] date)
{
    if (date.Length != 3)
    {
        Menu.PrintError("Wrong date format. Press 'm' to go back to the menu.");
        return false;
    }
    else if (!Regex.IsMatch(date[0], "^[2-2][0-9]{3}$")) //2000 to 2999
    {
        Menu.PrintError("Wrong year format. Press 'm' to go back to the menu.");
        return false;
    }
    else if (!Regex.IsMatch(date[1], "(^0[1-9]{1}$)|(^1[0-2]{1}$)")) //01 to 09, or 10 to 12
    {
        Menu.PrintError("Wrong month format. Press 'm' to go back to the menu.");
        return false;
    }
    else if (!Regex.IsMatch(date[2], "(^0[1-9]{1}$)|(^[1-2][0-9]{1}$)|(^3[0-1]{1}$)")) //01 to 09, or 10 to 29, or 30 to 31
    {
        Menu.PrintError("Wrong day format. Press 'm' to go back to the menu.");
        return false;
    }
    else   //Here we know the date has the correct format
    {
        return true;
    }
}