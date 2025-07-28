using Asana.Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Asana.API.Database
{
    public class ToDoFilebase
    {
        private string _root;
        private string _toDoRoot;
        private static ToDoFilebase _instance;

        public static ToDoFilebase Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ToDoFilebase();
                }

                return _instance;
            }
        }

        private ToDoFilebase()
        {
            // Use a cross-platform location in the user's home directory
            _root = Path.Combine(Environment.GetFolderPath
            (Environment.SpecialFolder.ApplicationData), "Asana");
            _toDoRoot = Path.Combine(_root, "ToDos");

            // Ensure the directory exists
            Directory.CreateDirectory(_toDoRoot);
        }

        public int LastKey
        {
            get
            {
                if (ToDos.Any())
                {
                    return ToDos.Select(x => x.Id).Max();
                }
                return 0;
            }
        }

        public ToDo AddOrUpdate(ToDo toDo)
        {
            //set up a new Id if one doesn't already exist
            if (toDo.Id <= 0)
            {
                toDo.Id = LastKey + 1;
            }

            //go to the right place            
            string path = Path.Combine(_toDoRoot, $"{toDo.Id}.json");



            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(toDo));

            //return the item, which now has an id
            return toDo;
        }

        public List<ToDo> ToDos
        {
            get
            {
                var root = new DirectoryInfo(_toDoRoot);
                var _toDos = new List<ToDo>();
                foreach (var file in root.GetFiles())
                {
                    var toDo = JsonConvert
                        .DeserializeObject<ToDo>
                        (File.ReadAllText(file.FullName));
                    if (toDo != null)
                    {
                        _toDos.Add(toDo);
                    }

                }
                return _toDos;
            }
        }


        public bool Delete(int id)
        {
            string path = Path.Combine(_toDoRoot, $"{id}.json");
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
    }



}