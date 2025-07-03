using Asana.Library.Models;
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
                // Return only the first 100 ToDos for performance
                return _toDoList.Take(100).ToList();
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
            ToDos = new List<ToDo>();
        }


        private static ToDoServiceProxy? instance;

        // Auto assign an incrementing ID to each new ToDo
        private int nextKey
        {
            get
            {
                if (ToDos.Any())
                {
                    return ToDos.Select(t => t.Id).Max() + 1;
                }
                return 1;
            }
        }

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
            if (toDo != null && toDo.Id == 0)
            {
                toDo.Id = nextKey;
                _toDoList.Add(toDo);
            }

            return toDo;
        }

        // Method to get a ToDo item by its ID
        public ToDo? GetById(int id)
        {
            return ToDos.FirstOrDefault(t => t.Id == id);
        }

        // Method to delete a ToDo item
        public void DeleteToDo(ToDo? toDo)
        {
            if (toDo == null)
            {
                return;
            }
            _toDoList.Remove(toDo);
        }

    }
}
