using System;

namespace Asana.Library.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public int? ProjId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsCompleted { get; set; }
        public string? Priority { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name}: {Description}\n" +
                   $"    (Priority: {Priority}, Completed: {IsCompleted}" +
                   $"{(ProjId.HasValue ? $", Project ID: {ProjId.Value}" : "")})";
        }
    }
}