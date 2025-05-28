using Asana.Library.Models;

namespace Asana
{
    public class Program
    {
        static void Main(string[] args)
        {
            var toDos = new List<ToDo>();
            var projects = new List<Project>();            
            var choice = "";

            do {

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

                        var toDo = new ToDo
                        {
                            Name = name,
                            Description = description
                        };

                        toDos.Add(toDo);
                        break;
                    case "4":
                        if (toDos.Any())
                        {
                            Console.WriteLine("ToDo List:");
                            toDos.ForEach(Console.WriteLine);
                        }
                        else
                        {
                            Console.WriteLine("No ToDos created.");
                        }
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