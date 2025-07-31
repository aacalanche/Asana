using Asana.Library.Models;
using Asana.Maui.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Library.Services
{
    // Class to encapsulate basic Project methods
    public class ProjectServiceProxy
    {
        private List<Project> _projectList;
        public List<Project> Projects
        {
            get
            {
                return _projectList.ToList();
            }
            private set
            {
                if (value != _projectList)
                {
                    _projectList = value;
                }
            }
        }

        // Singleton pattern to ensure only one instance of ProjectServiceProxy exists
        private ProjectServiceProxy()
        {
            try
            {
                var projectData = new WebRequestHandler().Get("/Project").Result;
                if (!string.IsNullOrEmpty(projectData))
                    Projects = JsonConvert.DeserializeObject<List<Project>>(projectData) ?? new List<Project>();
                else
                    Projects = new List<Project>();
            }
            catch
            {
                Projects = new List<Project>();
            }
        }

        private static ProjectServiceProxy? instance;

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

        // Method to add or update a Project item
        public Project? AddOrUpdate(Project? project)
        {
            if (project == null)
            {
                return project;
            }
            var isNewProject = project.Id == 0;
            string? projectData = null;
            try
            {
                projectData = new WebRequestHandler().Post("/Project", project).Result;
            }
            catch
            {
                // Optionally log the error
            }
            var newProject = !string.IsNullOrEmpty(projectData) ? JsonConvert.DeserializeObject<Project>(projectData) : null;

            if (newProject != null)
            {
                if (!isNewProject)
                {
                    var existingProject = _projectList.FirstOrDefault(p => p.Id == newProject.Id);
                    if (existingProject != null)
                    {
                        var index = _projectList.IndexOf(existingProject);
                        _projectList.RemoveAt(index);
                        _projectList.Insert(index, newProject);
                    }
                }
                else
                {
                    _projectList.Add(newProject);
                }
            }

            return project;
        }

        // Method to get a Project item by its ID
        public Project? GetById(int id)
        {
            return Projects.FirstOrDefault(p => p.Id == id);
        }

        // Method to delete a Project item
        public void DeleteProject(int id)
        {
            if (id == 0)
            {
                return;
            }
            string? projectData = null;
            try
            {
                projectData = new WebRequestHandler().Delete($"/Project/{id}").Result;
            }
            catch
            {
                // Optionally log the error
            }
            var projectToDelete = !string.IsNullOrEmpty(projectData) ? JsonConvert.DeserializeObject<Project>(projectData) : null;
            if (projectToDelete != null)
            {
                var localProject = _projectList.FirstOrDefault(p => p.Id == projectToDelete.Id);
                if (localProject != null)
                {
                    _projectList.Remove(localProject);
                }
            }
        }
    }
}
