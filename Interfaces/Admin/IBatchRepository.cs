using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces.Admin
{
    public interface IBatchRepository
    {
        Task<Batch> CreateBatchAsync(Batch batch);
        Task<Batch> UpdateBatchAsync(Batch batch);
        Task<Batch> DeleteBatchAsync(int id);
        Task<Batch> GetBatchAsync(int id);

    }
}
