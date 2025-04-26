using LinkedOutApi.DTOs.User.UserFolder.BatchFolder;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces.Admin
{
    public interface IBatchRepository
    {
        Task<Batch> CreateBatchAsync(Batch batch);
        Task<Batch> UpdateBatchAsync(int id, Batch batch);
        Task<Batch> DeleteBatchAsync(int id);
        Task<Batch> GetBatchByIdAsync(int id);
        Task<ICollection<Batch>> GetBatchesAsync();
    }
}
