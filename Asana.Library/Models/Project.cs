using System;

namespace Asana.Library.Models
{
    //Class representing a Project item in the application
    public class Project
    {
        public Project()
        {
            Id = 0;
        }
        //Attributes of the Project item
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        //Auto calculate the percentage of completed ToDos in the project
        public int? CompletePercent
        {
            get
            {
                if (ToDos > 0)
                {
                    return CompletedToDos / ToDos * 100;
                }
                return 0;
            }
        }
        //Find how many ToDos in the ToDos list have their ProjectId == this Project's ID
        public int? ToDos
        {
            get
            {
                .Where(t => (t != null) && (t?.ProjectId == Id))
                                .ToList()
                                .Count();
            }
        }
        public int? CompletedToDos
        {
            get
            {
                .Where(t => (t != null) && (t?.IsCompleted) && (t?.ProjectId == Id))
                                .ToList()
                                .Count();
            }
        }

        //Override ToString method to print Project details
        public override string ToString()
        {
            return $"[{Id}] {Name}: {Description}\n" +
                   $"    ({(ToDos > 0 ?
                   $"ToDos: {ToDos}, {CompletePercent}% Completed" :
                   "No ToDos")})";
        }
    }
}