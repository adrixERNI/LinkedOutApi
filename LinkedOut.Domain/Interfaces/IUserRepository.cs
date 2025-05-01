using System;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Repositories.UserRepostiory;

public interface IUserRepository
{
    Task<List<Entities.User>> GetAllUserAsync();
    Task<List<Entities.User>> GetAllMentorAsync();

    Task<Entities.User> GetByIdTraineeCertificationAsync(Guid id);

    Task<Entities.User> GetByIdTraineeAndCertificationAsync(Guid userId, int certificationId);

    Task<bool> AddUsersToBatch(int batchId, List<Guid> userIds);
    Task<bool> RemoveUsersFromBatch(int batchId, List<Guid> userIds);

}