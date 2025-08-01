using System;
using Asana.Library.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        //Constructor to create a ToDo from a ToDoDTO
        public ToDo(ToDoDTO dto)
        {
            Id = dto.Id;
            IsCompleted = dto.IsCompleted;
            Name = dto.Name;
            Priority = dto.Priority;
            Description = dto.Description;
        }
        //Attributes of the ToDo item
        public int Id { get; set; }
        public int? ProjId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsCompleted { get; set; }
        public string? Priority { get; set; }
        
        public DateTime? DueDate { get; set; }        
    }
}