//Arturo Calanche
//Project: Asana CLI Application

using System;

namespace Asana.Library.Models
{
    //Class representing a Project item in the application
    public class Project
    {
        //Attributes of the Project item
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        //Auto calculate the percentage of completed ToDos in the project
        public int CompletePercent
        {
            get
            {
                if (ToDos.Any())
                {
                    int completed = ToDos.Count(t => t.IsCompleted == true);
                    return completed / ToDos.Count * 100;
                }
                return 0;
            }
        }
        public List<ToDo>? ToDos { get; set; }

        //Override ToString method to print Project details
        public override string ToString()
        {
            return $"[{Id}] {Name}: {Description}\n" +
                   $"    ({(ToDos.Any() ?
                   $"ToDos: {ToDos.Count}, {CompletePercent}% Completed" :
                   "No ToDos")})";
        }
    }
}