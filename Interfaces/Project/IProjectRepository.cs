using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllProjects();
        Task<Project> GetProjectById(int id);
        Task<int> AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(int id);
    }
}