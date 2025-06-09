using System;
using System.Collections.ObjectModel;
using System.Linq;
using Asana.Library;
using Asana.Library.Models;
using Asana.Library.Services;

namespace Asana.Maui.ViewModels
{
    public class MainPageViewModel
    {
        private ToDoServiceProxy _toDoSvc;

        public MainPageViewModel()
        {
            _toDoSvc = ToDoServiceProxy.Current;
        }

        public ObservableCollection<ToDo> ToDos
        {
            get
            {
                return new ObservableCollection<ToDo>(_toDoSvc.ToDos);
            }
        }

        public bool IsShowCompleted { get; set; }
    }
}
