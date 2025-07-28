﻿using Asana.API.Database;
using Asana.Library.Models;

namespace Asana.API.Enterprise
{
    public class ToDoEC
    {
        public ToDoEC() { 
            
        }

        public IEnumerable<ToDo>GetToDos()
        {
            return ToDoFilebase.Current.ToDos.Take(100);
        }

        public ToDo? GetById(int id)
        {
            return GetToDos().FirstOrDefault(t => t.Id == id);
        }

        public ToDo? Delete(int id)
        {
            var toDoToDelete = GetById(id);
            if (toDoToDelete != null)
            {
                ToDoFilebase.Current.Delete(toDoToDelete.Id);
            }
            return toDoToDelete;
        }

        public ToDo? AddOrUpdate(ToDo? toDo)
        {
            ToDoFilebase.Current.AddOrUpdate(toDo);
            return toDo;
        }
    }
}
