using System;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Repositories.TraineeRepository;

public interface IUserRepository
{
    Task<List<Entities.User>> GetAllUserAsync();
    Task<List<Entities.User>> GetAllMentorAsync();

}