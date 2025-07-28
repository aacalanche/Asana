using Asana.Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Asana.API.Database
{
    public class ProjectFilebase
    {
        private string _root;
        private string _projectRoot;
        private static ProjectFilebase _instance;

        public static ProjectFilebase Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProjectFilebase();
                }

                return _instance;
            }
        }

        private ProjectFilebase()
        {
            // Use a cross-platform location in the user's home directory
            _root = Path.Combine(Environment.GetFolderPath
            (Environment.SpecialFolder.ApplicationData), "Asana");
            _projectRoot = Path.Combine(_root, "Projects");

            // Ensure the directory exists
            Directory.CreateDirectory(_projectRoot);
        }

        public int LastKey
        {
            get
            {
                if (Projects.Any())
                {
                    return Projects.Select(x => x.Id).Max();
                }
                return 0;
            }
        }

        public Project AddOrUpdate(Project project)
        {
            //set up a new Id if one doesn't already exist
            if (project.Id <= 0)
            {
                project.Id = LastKey + 1;
            }

            //go to the right place            
            string path = Path.Combine(_projectRoot, $"{project.Id}.json");

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(project));

            //return the item, which now has an id
            return project;
        }        

        public List<Project> Projects
        {
            get
            {
                var root = new DirectoryInfo(_projectRoot);
                var _projects = new List<Project>();
                foreach (var file in root.GetFiles())
                {
                    var project = JsonConvert
                        .DeserializeObject<Project>
                        (File.ReadAllText(file.FullName));
                    if (project != null)
                    {
                        _projects.Add(project);
                    }

                }
                return _projects;
            }
        }

        public bool Delete(int id)
        {
            string path = Path.Combine(_projectRoot, $"{id}.json");
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
    }
}
