using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces
{
    public interface ICVRepository
    {
        Task<IEnumerable<CV>> GetAllCVsAsync();
        Task<CV> GetCVByIdAsync(int id);
        Task AddCVAsync(CV cv);
        Task UpdateCVAsync(CV cv);
        Task DeleteCVAsync(int id);
    }
}