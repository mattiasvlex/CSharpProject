using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProject
{
    internal class TodoTask
    {
        public string Title { get; set; }
        public DateOnly Date { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string Project { get; set; }

        public TodoTask(string title, DateOnly date, string project)
        {
            Title = title;
            Date = date;
            Project = project;
        }

        //Private method for printing something more descriptive than true/false
        private string Completed()
        {
            return (this.IsCompleted) ? "Completed" : "Pending";
        }

        //For easy printing of an object
        public override string ToString()
        {
            return this.Title.PadRight(15) + this.Project.PadRight(15) + this.Date.ToString().PadRight(15) + this.Completed();
        }

        //For printing an object to file
		public string ToCSV()
		{
			return this.Title + "," + this.Project + "," + this.Date.ToString() + "," + this.Completed();
		}
    }
}
