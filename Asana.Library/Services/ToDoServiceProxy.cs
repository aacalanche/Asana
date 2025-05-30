using System;

namespace Asana.Library.Services
{
    public class ToDoServiceProxy
    {
        public void CreateToDo(string name, string description, string priority)
        {
            // Logic to create a ToDo
            Console.WriteLine($"ToDo created: {name}, {description}, {priority}");
        }

        public void DeleteToDo(int id)
        {
            // Logic to delete a ToDo
            Console.WriteLine($"ToDo with ID {id} deleted.");
        }

        public void UpdateToDo(int id, string name, string description, string priority)
        {
            // Logic to update a ToDo
            Console.WriteLine($"ToDo with ID {id} updated: {name}, {description}, {priority}");
        }

        public void ListAllToDos()
        {
            // Logic to list all ToDos
            Console.WriteLine("Listing all ToDos...");
        }
    }
}