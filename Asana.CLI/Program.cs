using Asana.Library.Models;
using Asana.Library.Services;

namespace Asana
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var toDoSvc = ToDoServiceProxy.Current;
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

                choice = Console.ReadLine() ?? "2";

                switch (choice)
                {
                    case "1":
                        Console.Write("Name: ");
                        var name = Console.ReadLine();

                        Console.Write("Description: ");
                        var description = Console.ReadLine();

                        Console.Write("Priority: ");
                        var priority = Console.ReadLine();

                        Console.Write("Assign to Project? (enter ID): ");
                        Console.Write("Enter Project ID: ");
                        if (int.TryParse(Console.ReadLine(), out var projId))
                        {
                            toDoSvc.CreateToDo(new ToDo
                            {
                                Name = name,
                                Description = description,
                                Priority = priority,
                                IsCompleted = false,
                                Id = 0,
                                ProjId = projId
                            });
                        }
                        else
                        {
                            toDoSvc.CreateToDo(new ToDo
                            {
                                Name = name,
                                Description = description,
                                Priority = priority,
                                IsCompleted = false,
                                Id = 0
                            });
                        }
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