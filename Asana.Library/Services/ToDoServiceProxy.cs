using System;
using Asana.Library.Models;

namespace Asana.Library.Services
{
    public class ToDoServiceProxy
    {
        public List<ToDo> ToDos;

        private ToDoServiceProxy()
        {
            ToDos = new List<ToDo>();
        }

        private int NextId => ToDos.Count > 0 ? ToDos.Max(t => t.Id) + 1 : 1;

        private static ToDoServiceProxy? instance;

        public static ToDoServiceProxy Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ToDoServiceProxy();
                }
                return instance;
            }
        }

        public void CreateToDo(ToDo toDo)
        {
            if (toDo.Id == 0)
            {
                toDo.Id = NextId;
                ToDos.Add(toDo);
                Console.WriteLine($"ToDo created");
            }

        }

        public void DeleteToDo()
        {
            Console.Write("Enter ToDo ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out var idToDelete))
            {
                var toDoToDelete = ToDos.FirstOrDefault(t => t.Id == idToDelete);
                if (toDoToDelete != null)
                {
                    ToDos.Remove(toDoToDelete);
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
        }

        public void UpdateToDo()
        {
            Console.Write("Enter ID of ToDo to update: ");
            if (long.TryParse(Console.ReadLine(), out var idToUpdate))
            {
                var toDoToUpdate = ToDos.FirstOrDefault(t => t.Id == idToUpdate);
                if (toDoToUpdate != null)
                {
                    Console.WriteLine($"Rename? (y/n): ");
                    var rename = Console.ReadLine()?.Trim().ToLower() == "y";
                    if (rename)
                    {
                        Console.Write("Name: ");
                        toDoToUpdate.Name = Console.ReadLine();

                        Console.Write("Description: ");
                        toDoToUpdate.Description = Console.ReadLine();
                        Console.WriteLine("Name and description updated.");
                    }

                    Console.Write("Is it Completed? (y/n): ");
                    if (Console.ReadLine()?.Trim().ToLower() == "y")
                    {
                        toDoToUpdate.IsCompleted = true;
                    }
                    else
                    {
                        toDoToUpdate.IsCompleted = false;
                    }

                    Console.Write("Priority: ");
                    toDoToUpdate.Priority = Console.ReadLine();

                    Console.WriteLine("ToDo updated.");
                }
                else
                {
                    Console.WriteLine("ERROR: ToDo not found.");
                }
            }
            else
            {
                Console.WriteLine("ERROR: Invalid ID format.");
            }
        }

        public void ListAllToDos()
        {
            if (ToDos.Any())
            {
                Console.WriteLine("ToDo List:");
                ToDos.ForEach(Console.WriteLine);
            }
            else
            {
                Console.WriteLine("No ToDos created.");
            }
        }
    }
}