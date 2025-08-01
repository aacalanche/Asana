using Asana.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Library.DTOs
{
    public class ToDoDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Priority { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? DueDate { get; set; } 
        public int? ProjId { get; set; }
        public int Id { get; set; }
        public ToDoDTO() { }
        public ToDoDTO(ToDo td)
        {
            Id = td.Id;
            Name = td.Name;
            Description = td.Description;
            Priority = td.Priority;
            IsCompleted = td.IsCompleted;
            ProjId = td.ProjId;
            DueDate = td.DueDate;
        }
    }
}
