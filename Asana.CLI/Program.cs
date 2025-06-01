//Arturo Calanche

using System;
using Asana.Library.Models;
using Asana.Library.Services;

namespace Asana
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var toDoSvc = ToDoServiceProxy.Current;
            var projectSvc = ProjectServiceProxy.Current;
            var choice = "";

            do
            {
                Console.WriteLine("Choose a menu option:");
                Console.WriteLine("1. Create a ToDo");
                Console.WriteLine("2. Delete a ToDo");
                Console.WriteLine("3. Update a ToDo");
                Console.WriteLine("4. List all ToDos");
                Console.WriteLine("5. Create a Project");
                Console.WriteLine("6. Delete a Project");
                Console.WriteLine("7. Update a Project");
                Console.WriteLine("8. List all Projects");
                Console.WriteLine("9. List all ToDos in a Project");
                Console.WriteLine("10. Exit");

                choice = Console.ReadLine() ?? "10";

                switch (choice)
                {
                    case "1":
                        Console.Write("Name: ");
                        var name = Console.ReadLine();

                        Console.Write("Description: ");
                        var description = Console.ReadLine();

                        Console.Write("Priority: ");
                        var priority = Console.ReadLine();

                        ToDo newToDo = new ToDo
                        {
                            Name = name,
                            Description = description,
                            Priority = priority,
                            IsCompleted = false,
                            Id = 0
                        };

                        if (projectSvc.Projects.Any())
                        {
                            Console.Write("Assign to Project? (enter ID): ");
                            if (int.TryParse(Console.ReadLine(), out var projId) &&
                                projectSvc.Projects.Any(p => p.Id == projId))
                            {
                                newToDo.ProjId = projId;
                                Console.WriteLine($"ToDo will be assigned to Project ID: {projId}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Project ID. " +
                                "ToDo will not be assigned to a project.");
                            }
                        }
                        toDoSvc.CreateToDo(newToDo);
                        break;
                    case "2":
                        toDoSvc.DeleteToDo();
                        break;
                    case "3":
                        toDoSvc.UpdateToDo();
                        break;
                    case "4":
                        toDoSvc.ListAllToDos();
                        break;
                    case "5":
                        Console.Write("Name: ");
                        var projName = Console.ReadLine();

                        Console.Write("Description: ");
                        var projDescription = Console.ReadLine();

                        projectSvc.CreateProject(new Project
                        {
                            Name = projName,
                            Description = projDescription,
                            Id = 0
                        });
                        break;
                    case "6":
                        projectSvc.DeleteProject();
                        break;
                    case "7":
                        projectSvc.UpdateProject();
                        break;
                    case "8":
                        projectSvc.ListAllProjects();
                        break;
                    case "9":
                        projectSvc.ListAllToDosInProject();
                        break;
                    case "10":
                        break;
                    default:
                        Console.WriteLine("ERROR: Invalid choice.");
                        break;
                }
            } while (choice != "10");
        }
    }

}
