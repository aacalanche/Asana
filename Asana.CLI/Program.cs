//Arturo Calanche
//Project: Asana CLI Application

using System;
using Asana.Library.Models;
using Asana.Library.Services;

namespace Asana
{
    public class Program
    {
        //Main method
        public static void Main(string[] args)
        {
            var toDoSvc = ToDoServiceProxy.Current;
            var projectSvc = ProjectServiceProxy.Current;
            var choice = "";

            //Display menu options and handle user input
            do
            {
                Console.WriteLine("\n1. Create a ToDo");
                Console.WriteLine("2. Delete a ToDo");
                Console.WriteLine("3. Update a ToDo");
                Console.WriteLine("4. List all ToDos");
                Console.WriteLine("5. Create a Project");
                Console.WriteLine("6. Delete a Project");
                Console.WriteLine("7. Update a Project");
                Console.WriteLine("8. List all Projects");
                Console.WriteLine("9. List all ToDos in a Project");
                Console.WriteLine("10. Exit");
                Console.Write("Choose a menu option: ");

                choice = Console.ReadLine() ?? "10";

                switch (choice)
                {
                    case "1":
                        //Create a new ToDo task
                        Console.Write("Name: ");
                        var name = Console.ReadLine();

                        Console.Write("Description: ");
                        var description = Console.ReadLine();

                        Console.Write("Priority: ");
                        var priority = Console.ReadLine();

                        //Create a new ToDo object with user input
                        ToDo newToDo = new ToDo
                        {
                            Name = name,
                            Description = description,
                            Priority = priority,
                            IsCompleted = false,
                            Id = 0
                        };

                        //Check if there are any projects to assign the ToDo to
                        if (projectSvc.Projects.Any())
                        {
                            //If there are projects, ask user for project ID to assign the ToDo
                            Console.Write("Assign to Project? (enter ID): ");
                            if (int.TryParse(Console.ReadLine(), out var projId) &&
                                projectSvc.Projects.Any(p => p.Id == projId))
                            {
                                newToDo.ProjId = projId;
                                Console.WriteLine($"ToDo will be assigned to Project ID: {projId}");
                            }
                            else
                            {
                                //If the ID is invalid or not provided, do not assign to a project
                                newToDo.ProjId = null;
                                Console.WriteLine("Invalid Project ID. " +
                                "ToDo will not be assigned to a project.");
                            }
                        }
                        //Add ToDo to list (and project if applicable)                        
                        toDoSvc.CreateToDo(newToDo);
                        break;
                    case "2":
                        //Delete a ToDo task
                        toDoSvc.DeleteToDo();
                        break;
                    case "3":
                        //Update an existing ToDo task
                        toDoSvc.UpdateToDo();
                        break;
                    case "4":
                        //List all ToDo tasks
                        toDoSvc.ListAllToDos();
                        break;
                    case "5":
                        //Create a new Project
                        Console.Write("Name: ");
                        var projName = Console.ReadLine();

                        Console.Write("Description: ");
                        var projDescription = Console.ReadLine();

                        //Create a new Project object and add it to list
                        projectSvc.CreateProject(new Project
                        {
                            Name = projName,
                            Description = projDescription,
                            Id = 0
                        });
                        break;
                    case "6":
                        //Delete an existing Project (and all associated ToDos)
                        projectSvc.DeleteProject();
                        break;
                    case "7":
                        //Update an existing Project
                        projectSvc.UpdateProject();
                        break;
                    case "8":
                        //List all Projects
                        projectSvc.ListAllProjects();
                        break;
                    case "9":
                        //List all ToDos in a given Project
                        projectSvc.ListAllToDosInProject();
                        break;
                    case "10":
                        //Exit the application
                        break;
                    default:
                        //Handle invalid input
                        Console.WriteLine("ERROR: Invalid choice.");
                        break;
                }
            } while (choice != "10"); //Loop until user chooses 10 to exit
        }
    }
}