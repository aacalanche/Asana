//Arturo Calanche
//Project: Asana CLI Application

using System;
using Asana.Library.Models;

namespace Asana.Library.Services
{
    //Class to encapsulate ToDo methods
    public class ToDoServiceProxy
    {
        //List to hold all ToDo items
        public List<ToDo> ToDos {get; private set;}

        private ToDoServiceProxy()
        {
            ToDos = new List<ToDo>
            {
                //Sample ToDos for demonstration purposes
                new ToDo { Id = 1, Name = "Sample ToDo 1", Description = "This is a sample ToDo", Priority = "High", IsCompleted = false },
                new ToDo { Id = 2, Name = "Sample ToDo 2", Description = "This is another sample ToDo", Priority = "Medium", IsCompleted = true }
            };
        }

        //Auto assign an incrementing ID to each new ToDo
        private int NextId => ToDos.Count > 0 ? ToDos.Max(t => t.Id) + 1 : 1;

        private static ToDoServiceProxy? instance;

        //Singleton pattern to ensure only one instance of ToDoServiceProxy exists
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

        //Method to create a new ToDo
        public void CreateToDo(ToDo toDo)
        {
            if (toDo.Id == 0)
            {
                //Auto assign a unique ID to the ToDo and add it to the list
                toDo.Id = NextId;
                ToDos.Add(toDo);
                Console.WriteLine("ToDo created.");
                if (toDo.ProjId.HasValue)
                {
                    //If the ToDo is associated with a project, add it to that project's ToDos
                    var project = ProjectServiceProxy.Current.Projects
                        .FirstOrDefault(p => p.Id == toDo.ProjId.Value);
                    if (project != null)
                    {
                        project.ToDos.Add(toDo);
                    }
                }
            }

        }

        //Method to delete a ToDo by ID
        public void DeleteToDo()
        {
            Console.Write("Enter ID of ToDo to delete: ");
            if (int.TryParse(Console.ReadLine(), out var idToDelete))
            {
                var toDoToDelete = ToDos.FirstOrDefault(t => t.Id == idToDelete);
                if (toDoToDelete != null)
                {
                    //Remove the ToDo from the list
                    ToDos.Remove(toDoToDelete);

                    //Remove ToDo from its associated project if it exists
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

        //Method to update an existing ToDo
        public void UpdateToDo()
        {
            Console.Write("Enter ID of ToDo to update: ");
            if (long.TryParse(Console.ReadLine(), out var idToUpdate))
            {
                var toDoToUpdate = ToDos.FirstOrDefault(t => t.Id == idToUpdate);
                if (toDoToUpdate != null)
                {
                    //Rename if the user wants to
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

                    //Update completion status
                    Console.Write("Is it Completed? (y/n): ");
                    var isCompleted = Console.ReadLine()?.Trim().ToLower() == "y";
                    toDoToUpdate.IsCompleted = isCompleted;

                    if (toDoToUpdate.ProjId.HasValue)
                    {
                        //If the ToDo is associated with a project, update it in that project's ToDos
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

        //Method to list all ToDos
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