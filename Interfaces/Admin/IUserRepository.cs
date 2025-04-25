using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces.Admin
{
    public interface IUserRepository
    {
        Task<User> AddUserToBatchAsync(int batchId, Guid userId);
        Task<User> DeleteUserFromBatchAsync(int batchId, Guid userId);
        Task<User> AddImageToUserAsync(int imageId,  Guid userId);
        Task<User> DeleteImageFromUserAsync(int imageId, Guid userId);
        Task<User> UpdateImageToUserAsync(int imageId, Guid userId);
    }
}
