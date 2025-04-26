using System;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Repositories.TraineeRepository;

public interface IUserRepository
{
    Task<List<User>> GetAllUserAsync();
    Task<List<User>> GetAllMentorAsync();

}