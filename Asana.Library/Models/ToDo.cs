using System;

namespace Asana.Library.Models
{
    //Class representing a ToDo item in the application
    public class ToDo
    {
        public ToDo()
        {
            Id = 0;
            IsCompleted = false;
        }
        //Attributes of the ToDo item
        public int Id { get; set; }
        public int? ProjId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsCompleted { get; set; }
        public string? Priority { get; set; }
        
        public DateTime? DueDate { get; set; }

        //Override ToString method to print ToDo details
        public override string ToString()
        {
            return $"[{Id}] {Name}: {Description}\n" +
                   $"    (Priority: {Priority}, Completed: {IsCompleted}" +
                   $"{(ProjId.HasValue ? $", Project ID: {ProjId.Value}" : "")})";
        }
    }
}