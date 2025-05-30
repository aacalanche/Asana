using Asana.Library.Models;

namespace Asana
{
    public class Program
    {
        
        public static void PrintProject(Project project)
        {
            Console.WriteLine($"Project ID: {project.Id}");
            Console.WriteLine($"Name: {project.Name}");
            Console.WriteLine($"Description: {project.Description}");
            Console.WriteLine($"Created At: {project.CreatedAt}");
            Console.WriteLine($"Updated At: {project.UpdatedAt}");
            Console.WriteLine($"ToDos Count: {project.ToDos.Count}");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            var toDos = new List<ToDo>();
            var projects = new List<Project>();
            var toDoCount = 0;
            var projectCount = 0;
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

                        Console.Write("Priority (Low/Normal/High): ");
                        var priority = Console.ReadLine();
                        if (string.IsNullOrEmpty(priority) ||
                            !(priority.Equals("Low", StringComparison.OrdinalIgnoreCase) ||
                              priority.Equals("Normal", StringComparison.OrdinalIgnoreCase) ||
                              priority.Equals("High", StringComparison.OrdinalIgnoreCase)))
                        {
                            Console.WriteLine("Invalid priority. Setting to 'Normal'.");
                            priority = "Normal";
                        }

                        var toDo = new ToDo
                        {
                            Name = name,
                            Description = description,
                            Id = ++toDoCount,
                            IsCompleted = false,
                            Priority = priority,
                            ProjectId = null // Assuming no project is assigned initially
                        };

                        toDos.Add(toDo);
                        break;
                    case "2":
                        Console.Write("Enter ToDo ID to delete: ");
                        if (long.TryParse(Console.ReadLine(), out var idToDelete))
                        {
                            var toDoToDelete = toDos.FirstOrDefault(t => t.Id == idToDelete);
                            if (toDoToDelete != null)
                            {
                                toDos.Remove(toDoToDelete);
                                Console.WriteLine("ToDo deleted.");
                            }
                            else
                            {
                                Console.WriteLine("ToDo not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID format.");
                        }
                        break;
                    case "3":
                        Console.Write("Enter ID of ToDo to update: ");
                        if (long.TryParse(Console.ReadLine(), out var idToUpdate))
                        {
                            var toDoToUpdate = toDos.FirstOrDefault(t => t.Id == idToUpdate);
                            if (toDoToUpdate != null)
                            {
                                Console.Write("Name: ");
                                toDoToUpdate.Name = Console.ReadLine();

                                Console.Write("Description: ");
                                toDoToUpdate.Description = Console.ReadLine();

                                Console.Write("Is it Completed? (true/false): ");
                                if (bool.TryParse(Console.ReadLine(), out var isCompleted))
                                {
                                    toDoToUpdate.IsCompleted = isCompleted;
                                }

                                Console.Write("Priority (Low/Normal/High): ");
                                toDoToUpdate.Priority = Console.ReadLine();

                                Console.WriteLine("ToDo updated.");
                            }
                            else
                            {
                                Console.WriteLine("ToDo not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID format.");
                        }
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