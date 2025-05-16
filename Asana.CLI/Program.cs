using System;
using Asana.Library.Models;

ToDo MyFirstToDo = new ToDo
{
    Name = "Buy groceries",
    Description = "Milk, Bread, Eggs",
    DueDate = DateTime.Now.AddDays(2),
    IsDone = false,
    Priority = "High"
};