using System;

namespace Asana.Library.Models
{
    public class ToDo
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? IsDone { get; set; }
        public string? Priority { get; set; }
        
        public override string ToString()
        {
            return $"{Name} - {Description}";
        }
    }
}