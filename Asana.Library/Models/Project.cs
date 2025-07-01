using System;
using System.Linq;
using Asana.Library.Services;

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

        public int ToDos
        {
            get
            {
                //Find how many ToDos in the ToDos list have their ProjectId == this Project's ID
                return ToDoServiceProxy.Current.ToDos
                                .Where(t => (t != null) && (t?.ProjId == Id))
                                .ToList()
                                .Count();
            }
        }
        public int CompletedToDos
        {
            get
            {
                //Find how many ToDos in the ToDos list have their ProjectId == this Project's ID and are completed
                return ToDoServiceProxy.Current.ToDos
                                .Where(t => (t != null) && (t?.ProjId == Id) && (t?.IsCompleted == true))
                                .ToList()
                                .Count();
            }
        }        

        //Override ToString method to print Project details
        public override string ToString()
        {
            return $"[{Id}] {Name}: {Description}\n" +
                   $"    ({(ToDos > 0 ?
                   $"ToDos: {ToDos}, X% Completed" :
                   "No ToDos")})";
        }
    }
}