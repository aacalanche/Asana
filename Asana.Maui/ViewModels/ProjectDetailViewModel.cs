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
    public class ProjectDetailViewModel
    {
        // ViewModel for managing project details, including adding, updating, and deleting projects
        public ProjectDetailViewModel()
        {
            Model = new Project();
            DeleteCommand = new Command(DoDelete);
        }

        public ProjectDetailViewModel(int id)
        {
            Model = ProjectServiceProxy.Current.GetById(id) ?? new Project();
            DeleteCommand = new Command(DoDelete);
        }

        public ProjectDetailViewModel(Project? model)
        {
            Model = model ?? new Project();
            DeleteCommand = new Command(DoDelete);
        }
        // Method to delete the current project model using the ProjectServiceProxy
        public void DoDelete()
        {
            ProjectServiceProxy.Current.DeleteProject(Model);
        }

        public Project? Model { get; set; }
        public ICommand? DeleteCommand { get; set; }

        // Method to add or update the current project model using the ProjectServiceProxy
        public void AddOrUpdateProject()
        {
            ProjectServiceProxy.Current.AddOrUpdate(Model);
        }

        // Methods to get display strings for project details for the UI
        public string ToDosDisplay
        {
            get
            {
                return Model?.ToDos == 0 || Model?.ToDos == null ?
                "No ToDos" : $"{Model?.ToDos} ToDos";
            }
        }

        public string CompletedPercentDisplay
        {
            get
            {
                return Model?.ToDos == 0 || Model?.ToDos == null ?
                "" : $"{Model?.CompletePercent}% Completed";
            }
        }              
    }
}