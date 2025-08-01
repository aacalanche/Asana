using Asana.Library.Models;
using Asana.Maui.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Library.Services
{
    // Class to encapsulate basic ToDo methods
    public class ToDoServiceProxy
    {
        private List<ToDo> _toDoList;
        public List<ToDo> ToDos
        {
            get
            {
                return _toDoList.ToList();
            }

            private set
            {
                if (value != _toDoList)
                {
                    _toDoList = value;
                }
            }
        }
        // Singleton pattern to ensure only one instance of ToDoServiceProxy exists
        private ToDoServiceProxy()
        {
            try
            {
                var todoData = new WebRequestHandler().Get("/ToDo").Result;
                if (!string.IsNullOrEmpty(todoData))
                    ToDos = JsonConvert.DeserializeObject<List<ToDo>>(todoData) ??
                    new List<ToDo>();
                else
                    ToDos = new List<ToDo>();
            }
            catch
            {
                ToDos = new List<ToDo>();
            }
        }


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

        // Method to add or update a ToDo item
        public ToDo? AddOrUpdate(ToDo? toDo)
        {
            if (toDo == null)
            {
                return toDo;
            }
            var isNewToDo = toDo.Id == 0;
            string? todoData = null;
            try
            {
                todoData = new WebRequestHandler().Post("/ToDo", toDo).Result;
            }
            catch
            {
                // Optionally log the error
            }
            var newToDo = !string.IsNullOrEmpty(todoData) ?
            JsonConvert.DeserializeObject<ToDo>(todoData) : null;

            if (newToDo != null)
            {
                if (!isNewToDo)
                {
                    var existingToDo = _toDoList.FirstOrDefault(t => t.Id == newToDo.Id);
                    if (existingToDo != null)
                    {
                        var index = _toDoList.IndexOf(existingToDo);
                        _toDoList.RemoveAt(index);
                        _toDoList.Insert(index, newToDo);
                    }
                }
                else
                {
                    _toDoList.Add(newToDo);
                }
            }

            return toDo;
        }

        // Method to get a ToDo item by its ID
        public ToDo? GetById(int id)
        {
            return ToDos.FirstOrDefault(t => t.Id == id);
        }

        // Method to delete a ToDo item
        public void DeleteToDo(int id)
        {
            if (id == 0)
            {
                return;
            }
            string? todoData = null;
            try
            {
                todoData = new WebRequestHandler().Delete($"/ToDo/{id}").Result;
            }
            catch
            {
                // Optionally log the error
            }
            var toDoToDelete = !string.IsNullOrEmpty(todoData) ? JsonConvert.DeserializeObject<ToDo>(todoData) : null;
            if (toDoToDelete != null)
            {
                var localToDo = _toDoList.FirstOrDefault(t => t.Id == toDoToDelete.Id);
                if (localToDo != null)
                {
                    _toDoList.Remove(localToDo);
                }
            }
        }

    }
}
