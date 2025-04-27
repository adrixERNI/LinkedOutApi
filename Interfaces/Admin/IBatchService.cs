using LinkedOutApi.DTOs.User.UserFolder.BatchFolder;

namespace LinkedOutApi.Interfaces.Admin
{
    public interface IBatchService
    {
        Task<BatchReadDTO> CreateBatchAsync(BatchCreateDTO batchDTO);
        Task<BatchReadDTO> UpdateBatchAsync(int id, BatchReadDTO batchDTO);
        Task<BatchReadDTO> DeleteBatchAsync(int id);
        Task<BatchReadUserTopicDTO> GetBatchByIdAsync(int id);
        Task<ICollection<BatchReadUserTopicDTO>> GetBatchesAsync();
    }
}
