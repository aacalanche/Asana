using Asana.Library.Models;
using Asana.Library.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Maui.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        // Uses ToDoServiceProxy and ProjectServiceProxy to access data
        private ToDoServiceProxy _toDoSvc;
        private ProjectServiceProxy _projectSvc;

        public MainPageViewModel()
        {
            _toDoSvc = ToDoServiceProxy.Current;
            _projectSvc = ProjectServiceProxy.Current;
        }

        // Properties for managing ToDos
        public ToDoDetailViewModel? SelectedToDo { get; set; }
        public ObservableCollection<ToDoDetailViewModel> ToDos
        {
            get
            {
                var toDos = _toDoSvc.ToDos
                        .Select(t => new ToDoDetailViewModel(t));
                if (!IsShowCompleted)
                {
                    toDos = toDos.Where(t => !t?.Model?.IsCompleted ?? false);
                }
                return new ObservableCollection<ToDoDetailViewModel>(toDos);
            }
        }
        
        // Returns the ID of the selected ToDo, or 0 if no ToDo is selected
        public int SelectedToDoId => SelectedToDo?.Model?.Id ?? 0;

        // Property to toggle the visibility of completed ToDos
        private bool isShowCompleted;
        public bool IsShowCompleted
        {
            get
            {
                return isShowCompleted;
            }

            set
            {
                if (isShowCompleted != value)
                {
                    isShowCompleted = value;
                    NotifyPropertyChanged(nameof(ToDos));
                }
            }
        }

        // Deletes the selected ToDo item
        public void DeleteToDo()
        {
            if (SelectedToDo == null)
            {
                return;
            }

            ToDoServiceProxy.Current.DeleteToDo(SelectedToDoId);
            NotifyPropertyChanged(nameof(ToDos));
        }              
        
        // Properties for managing Projects
        public ObservableCollection<ProjectDetailViewModel> Projects
        {
            get
            {
                return new ObservableCollection<ProjectDetailViewModel>(
                    _projectSvc.Projects.Select(p => new ProjectDetailViewModel(p)));
            }
        }
        public ProjectDetailViewModel? SelectedProject { get; set; }
        public int SelectedProjectId => SelectedProject?.Model?.Id ?? 0;
        public void DeleteProject()
        {
            if (SelectedProject == null)
            {
                return;
            }

            ProjectServiceProxy.Current.DeleteProject(SelectedProjectId);
            NotifyPropertyChanged(nameof(Projects));
        }

        // Refreshes the page by notifying property changes for ToDos and Projects
        public void RefreshPage()
        {
            NotifyPropertyChanged(nameof(ToDos));
            NotifyPropertyChanged(nameof(Projects));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
