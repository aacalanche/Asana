using System;
using Asana.Library.Models;

namespace Asana.Library.Services
{
    public class ProjectServiceProxy
    {
        public List<Project> Projects;

        private ProjectServiceProxy()
        {
            Projects = new List<Project>();
        }

        private int NextId => Projects.Count > 0 ? Projects.Max(p => p.Id) + 1 : 1;

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

        public void CreateProject(Project project)
        {
            if (project.Id == 0)
            {
                project.Id = NextId;
                Projects.Add(project);
                Console.WriteLine("Project created.");
                project.ToDos = new List<ToDo>();
            }
        }

        public void DeleteProject()
        {
            Console.Write("Enter ID of Project to delete: ");
            if (int.TryParse(Console.ReadLine(), out var idToDelete))
            {
                var projectToDelete = Projects.FirstOrDefault(p => p.Id == idToDelete);
                if (projectToDelete != null)
                {
                    Projects.Remove(projectToDelete);
                    // Also remove all ToDos associated with this project
                    var toDoService = ToDoServiceProxy.Current;
                    toDoService.ToDos.RemoveAll(t => t.ProjId == idToDelete);
                    Console.WriteLine("All ToDos associated with this project have been removed.");
                    Console.WriteLine("Project deleted.");
                }
                else
                {
                    Console.WriteLine("Project not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        public void UpdateProject()
        {
            Console.Write("Enter ID of Project to update: ");
            if (int.TryParse(Console.ReadLine(), out var idToUpdate))
            {
                var projectToUpdate = Projects.FirstOrDefault(p => p.Id == idToUpdate);
                if (projectToUpdate != null)
                {
                    Console.Write("Name: ");
                    projectToUpdate.Name = Console.ReadLine();

                    Console.Write("Description: ");
                    projectToUpdate.Description = Console.ReadLine();                    

                    Console.WriteLine("Project updated.");
                }
                else
                {
                    Console.WriteLine("Project not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }
        public void ListAllProjects()
        {
            if (Projects.Any())
            {
                Console.WriteLine("Project list:");
                Projects.ForEach(Console.WriteLine);
            }
            else
            {
                Console.WriteLine("No Projects found.");
            }
        }

        public void ListAllToDosInProject()
        {
            Console.Write("Enter Project ID: ");
            if (int.TryParse(Console.ReadLine(), out var projId))
            {
                var projectToList = Projects.FirstOrDefault(p => p.Id == projId);
                if (projectToList != null)
                {
                    if (projectToList.ToDos.Any())
                    {
                        Console.WriteLine("ToDos in this Project:");
                        projectToList.ToDos.ForEach(Console.WriteLine);
                    }
                    else
                    {
                        Console.WriteLine("No ToDos in this Project.");
                    }
                }
                else
                {
                    Console.WriteLine("Project not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

    }
}
