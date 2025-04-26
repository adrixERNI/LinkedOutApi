using LinkedOutApi.DTOs.User.UserFolder.BatchFolder;

namespace LinkedOutApi.Interfaces.Admin
{
    public interface IBatchService
    {
        Task<BatchReadDTO> CreateBatchAsync(BatchCreateDTO batchDTO);
        Task<BatchUpdateDTO> UpdateBatchAsync(int id, BatchUpdateDTO batchDTO);
        Task<BatchReadDTO> DeleteBatchAsync(int id);
        Task<BatchReadDTO> GetBatchByIdAsync(int id);
        Task<ICollection<BatchReadDTO>> GetBatchesAsync();
    }
}
