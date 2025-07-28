using Asana.API.Database;
using Asana.Library.Models;

namespace Asana.API.Enterprise
{
    public class ProjectEC
    {
        public IEnumerable<Project> GetProjects()
        {
            return ProjectFilebase.Current.Projects.Take(100);
        }

        public Project? GetById(int id)
        {
            return GetProjects().FirstOrDefault(p => p.Id == id);
        }

        public Project? AddOrUpdate(Project? project)
        {
            ProjectFilebase.Current.AddOrUpdate(project);
            return project;
        }

        public Project? Delete(int id)
        {
            var projectToDelete = GetById(id);
            if (projectToDelete != null)
            {
                ProjectFilebase.Current.Delete(projectToDelete.Id);
            }
            return projectToDelete;
        }
    }
}
