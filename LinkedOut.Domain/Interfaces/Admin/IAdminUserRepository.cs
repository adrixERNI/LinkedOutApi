using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces.Admin
{
    public interface IAdminUserRepository
    {
        Task<Entities.User> AddUserToBatchAsync(int batchId, Guid userId);
        Task<Entities.User> DeleteUserFromBatchAsync(int batchId, Guid userId);
        Task<Entities.User> AddImageToUserAsync(int imageId,  Guid userId);
        Task<Entities.User> DeleteImageFromUserAsync(int imageId, Guid userId);
        Task<Entities.User> UpdateImageToUserAsync(int imageId, Guid userId);
    }
}
