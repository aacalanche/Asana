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
    public class ToDoDetailViewModel
    {
        public ToDoDetailViewModel()
        {
            Model = new ToDo();

            DeleteCommand = new Command(DoDelete);
        }

        private ToDo? _originalToDo;

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

        public void DoDelete()
        {
            ToDoServiceProxy.Current.DeleteToDo(Model);
        }

        public ToDo? Model { get; set; }
        public ICommand? DeleteCommand { get; set; }

        public List<string> Priorities
        {
            get
            {
                return new List<string> { "None", "Low", "Medium", "High" };
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

        public void AddOrUpdateToDo()
        {
            ToDoServiceProxy.Current.AddOrUpdate(Model);
        }


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
                    Model.Priority = "None";
                }
                else
                {
                    Model.Priority = value;
                }
            }

            get
            {
                return Model?.Priority?.ToString() ?? string.Empty;
            }
        }

        public DateTime DueDate
        {
            get => Model?.DueDate ?? DateTime.Today;
            set
            {
                if (Model != null && Model.DueDate != value)
                {
                    Model.DueDate = value;
                    // Raise property changed if needed
                }
            }
        }


    }
}
