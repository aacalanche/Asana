//Arturo Calanche
//Project: Asana CLI Application

using System;
using Asana.Library.Models;

namespace Asana.Library.Services
{
    //Class to encapsulate Project methods
    public class ProjectServiceProxy
    {
        //List to hold all Project items
        public List<Project> Projects;

        private ProjectServiceProxy()
        {
            Projects = new List<Project>();
        }

        //Auto assign an incrementing ID to each new Project
        private int NextId => Projects.Count > 0 ? Projects.Max(p => p.Id) + 1 : 1;

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

        //Method to create a new Project
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

        //Method to delete a Project by ID
        public void DeleteProject()
        {
            Console.Write("Enter ID of Project to delete: ");
            if (int.TryParse(Console.ReadLine(), out var idToDelete))
            {
                var projectToDelete = Projects.FirstOrDefault(p => p.Id == idToDelete);
                if (projectToDelete != null)
                {
                    Projects.Remove(projectToDelete);

                    //Delete all ToDos associated with this project
                    var toDoSvc = ToDoServiceProxy.Current;
                    toDoSvc.ToDos.RemoveAll(t => t.ProjId == idToDelete);
                    Console.WriteLine("Project deleted.");
                    Console.WriteLine("All ToDos associated with this project deleted.");
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

        //Method to update an existing Project
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

        //Method to list all Projects
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

        //Method to list all ToDos in a specific Project
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
