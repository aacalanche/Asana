using System;
using Newtonsoft.Json;
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

        [JsonIgnore] // Newtonsoft.Json
        [System.Text.Json.Serialization.JsonIgnore] // System.Text.Json
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

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
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

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public double CompletePercent
        {
            get
            {
                if (ToDos == 0) return 0;
                return Math.Round(CompletedToDos / (double)ToDos * 100);
            }
        }
    }
}