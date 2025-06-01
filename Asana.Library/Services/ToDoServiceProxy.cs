//Arturo Calanche

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
                Console.WriteLine("ToDo created.");
                if (toDo.ProjId.HasValue)
                {
                    var project = ProjectServiceProxy.Current.Projects
                        .FirstOrDefault(p => p.Id == toDo.ProjId.Value);
                    if (project != null)
                    {
                        project.ToDos.Add(toDo);
                    }
                }
            }

        }

        public void DeleteToDo()
        {
            Console.Write("Enter ID of ToDo to delete: ");
            if (int.TryParse(Console.ReadLine(), out var idToDelete))
            {
                var toDoToDelete = ToDos.FirstOrDefault(t => t.Id == idToDelete);
                if (toDoToDelete != null)
                {
                    ToDos.Remove(toDoToDelete);
                    if (toDoToDelete.ProjId.HasValue)
                    {
                        var project = ProjectServiceProxy.Current.Projects
                            .FirstOrDefault(p => p.Id == toDoToDelete.ProjId.Value);
                        if (project != null)
                        {
                            project.ToDos.Remove(toDoToDelete);
                        }
                    }
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
                    Console.WriteLine("Rename? (y/n): ");
                    if (Console.ReadLine()?.Trim().ToLower() == "y")
                    {
                        Console.Write("Name: ");
                        var name = Console.ReadLine();
                        toDoToUpdate.Name = name;

                        Console.Write("Description: ");
                        var description = Console.ReadLine();
                        toDoToUpdate.Description = description;
                        Console.WriteLine("Name and description updated.");

                        Console.Write("Priority: ");
                        var priority = Console.ReadLine();
                        toDoToUpdate.Priority = priority;
                    }

                    Console.Write("Is it Completed? (y/n): ");
                    var isCompleted = Console.ReadLine()?.Trim().ToLower() == "y";
                    toDoToUpdate.IsCompleted = isCompleted;

                    if (toDoToUpdate.ProjId.HasValue)
                    {
                        var project = ProjectServiceProxy.Current.Projects
                            .FirstOrDefault(p => p.Id == toDoToUpdate.ProjId);
                        if (project != null)
                        {
                            var existingToDo = project.ToDos.FirstOrDefault(t => t.Id == toDoToUpdate.Id);
                            if (existingToDo != null)
                            {
                                existingToDo.Name = toDoToUpdate.Name;
                                existingToDo.Description = toDoToUpdate.Description;
                                existingToDo.Priority = toDoToUpdate.Priority;
                                existingToDo.IsCompleted = toDoToUpdate.IsCompleted;
                            }
                        }
                    }
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