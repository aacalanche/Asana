using System;

namespace Asana.Library.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
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

        public override string ToString()
        {
            return $"[{Id}] {Name}: {Description}\n" +
                   $"({(ToDos.Any() ?
                   $"ToDos: {ToDos.Count}, {CompletePercent}% Completed" :
                   "No ToDos")})";
        }
    }
}