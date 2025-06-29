using System;

namespace Asana.Maui.ViewModels
{
    public class ProjectDetailViewModel
    {
        public ProjectDetailViewModel()
        {
            Model = new Project();
            DeleteCommand = new Command(DoDelete);
        }

        private Project? _originalProject;

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

        public void DoDelete()
        {
            ProjectServiceProxy.Current.DeleteProject(Model);
        }

        public Project? Model { get; set; }
        public ICommand? DeleteCommand { get; set;
    }
}