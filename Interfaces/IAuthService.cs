using LinkedOutApi.DTOs.Auth;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces;

public interface IAuthService
{
    Task<GoogleUserDTO> AuthenticateWithGoogleAsync(string code);
    Task<User?> CreateUserAsync(string name, string email, string role);
    Task<User?> GetUserByEmailAsync(string email);
}