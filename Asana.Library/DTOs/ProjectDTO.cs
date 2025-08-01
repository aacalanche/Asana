using Asana.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Library.DTOs
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int ToDos { get; set; }
        public int CompletedToDos { get; set; }
        public double CompletePercent { get; set; }

        public ProjectDTO() { }

        public ProjectDTO(Project proj)
        {
            Id = proj.Id;
            Name = proj.Name;
            Description = proj.Description;
            ToDos = proj.ToDos;
            CompletedToDos = proj.CompletedToDos;
            CompletePercent = proj.CompletePercent;
        }
    }
}