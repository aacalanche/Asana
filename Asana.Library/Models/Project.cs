using System;

namespace Asana.Library.Models
{
    public class Project
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? CompletePercent { get; set; }
        public List<ToDo>? ToDos { get; set; }

        public override string ToString()
        {
            return $"{Id}. {Name}: {Description}";
        }
    }
}