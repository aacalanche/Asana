//Arturo Calanche
//Project: Asana CLI Application

using System;
using Asana.Library.Models;

namespace Asana.Library.Services
{
    //Class to encapsulate Project methods
    public class ProjectServiceProxy
    {
        private List<Project> _projectList;
        public List<Project> Projects
        {
            get
            {
                //Return only the first 100 projects for performance
                return _projectList.Take(100).ToList();
            }
            private set
            {
                if (value != _projectList)
                {
                    _projectList = value;
                }
            }
        }

        private ProjectServiceProxy()
        {
            Projects = new List<Project>();
        }

        //Auto assign an incrementing ID to each new Project
        private int nextKey
        {
            get
            {
                if (Projects.Any())
                {
                    return Projects.Select(t => t.Id).Max() + 1;
                }
                return 1;
            }
        }

        private static ProjectServiceProxy? instance;

        //Singleton pattern to ensure only one instance of ProjectServiceProxy exists
        public static ProjectServiceProxy Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProjectServiceProxy();
                }
                return instance;
            }
        }

        //Method to create or update a Project
        public Project? AddOrUpdate(Project? project)
        {
            if (project != null && project.Id == 0)
            {
                project.Id = nextKey;
                _projectList.Add(project);
            }

            return project;
        }

        //Method to delete a Project by ID
        public void DisplayProjects()
        {
            Projects.ForEach(Console.WriteLine);
        }

        public void DisplayToDosInProject(bool isShowCompleted = false)
        {
            if (isShowCompleted)
            {
                Projects.SelectMany(p => p.ToDos ?? new List<ToDo>())
                        .ToList()
                        .ForEach(Console.WriteLine);
            }
            else
            {
                Projects.SelectMany(p => p.ToDos ?? new List<ToDo>())
                        .Where(t => (t != null) && !(t?.IsCompleted ?? false))
                        .ToList()
                        .ForEach(Console.WriteLine);
            }
        }

        public Project? GetById(int id)
        {
            return Projects.FirstOrDefault(p => p.Id == id);
        }

        public void DeleteProject(Project? project)
        {
            if (project != null)
            {
                _projectList.Remove(project);
            }
        }
    }
}
