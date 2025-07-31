using Asana.Library.Models;
using Asana.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Asana.Maui.ViewModels
{
    // ViewModel for managing ToDo details, including adding, updating, and deleting ToDos
    public class ToDoDetailViewModel
    {
        public ToDoDetailViewModel()
        {
            Model = new ToDo();

            DeleteCommand = new Command(DoDelete);
        }

        public ToDoDetailViewModel(int id)
        {
            Model = ToDoServiceProxy.Current.GetById(id) ?? new ToDo();

            DeleteCommand = new Command(DoDelete);


        }

        public ToDoDetailViewModel(ToDo? model)
        {
            Model = model ?? new ToDo();
            DeleteCommand = new Command(DoDelete);
        }

        // Method to delete the current ToDo model using the ToDoServiceProxy
        public void DoDelete()
        {
            ToDoServiceProxy.Current.DeleteToDo(Model.Id);
        }

        public ToDo? Model { get; set; }
        public ICommand? DeleteCommand { get; set; }

        // Manages Priority picker options for the ToDo
        public List<string> Priorities
        {
            get
            {
                return new List<string> { "None", "Low", "Medium", "High" };
            }
        }

        // Methods to maanage Project ID picker options for the ToDo
        public List<int?> ProjectIds => new List<int?> { null }
            .Concat(ProjectServiceProxy.Current.Projects.Select(p => (int?)p.Id))
            .ToList();

        public int? SelectedProjectId
        {
            get
            {
                return Model?.ProjId ?? null;
            }
            set
            {
                if (Model != null && Model.ProjId != value)
                {
                    Model.ProjId = value;
                }
            }
        }

        public string ProjIdDisplay
        {
            get
            {
                return Model?.ProjId == null
                    ? ""
                    : $"Project: {Model.ProjId.Value}";
            }
        }

        public string SelectedPriority
        {
            get
            {
                return Model?.Priority ?? "None";
            }
            set
            {
                if (Model != null && Model.Priority != value)
                {
                    Model.Priority = value;
                }
            }
        }

        // Method to add or update the current ToDo model using the ToDoServiceProxy
        public void AddOrUpdateToDo()
        {
            ToDoServiceProxy.Current.AddOrUpdate(Model);
        }

        // Methods to get display strings for ToDo details for the UI
        public string PriorityDisplay
        {
            set
            {
                if (Model == null)
                {
                    return;
                }

                if (string.IsNullOrEmpty(value) || value == "None")
                {
                    Model.Priority = null;
                }
                else
                {
                    Model.Priority = value;
                }
            }

            get
            {
                return Model?.Priority == null ||
                Model?.Priority == "None" ? "Priority: None"
                : $"Priority: {Model.Priority}";
            }
        }

        // Manage the DueDate displayed for the ToDo, defaulting to today's date if not set
        public DateTime DueDate
        {
            get => Model?.DueDate ?? DateTime.Today;
            set
            {
                if (Model != null && Model.DueDate != value)
                {
                    Model.DueDate = value;
                }
            }
        }

        public string DueDateDisplay
        {
            get
            {
                return Model?.DueDate == null
                    ? $"Due Date: {DateTime.Today.ToShortDateString()}"
                    : $"Due Date: {Model.DueDate.Value.ToShortDateString()}";
            }
        }
    }
}
