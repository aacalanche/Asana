using Asana.Library.Models;

namespace Asana
{
    public class Program
    {
        static void Main(string[] args)
        {
            var toDos = new List<ToDo>();
            var choice = "";

            do {

                Console.WriteLine("Choose a menu option:");
                Console.WriteLine("1. Create a ToDo");
                Console.WriteLine("2. Exit");

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
                    case "2":
                        break;
                    default:
                        Console.WriteLine("ERROR: Invalid choice.");
                        break;
                }
            } while (choice != "2");

            if (toDos.Any())
            {
                Console.WriteLine("ToDo List:");
                foreach (var item in toDos)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("No ToDos created.");
            }
        }
    }
}