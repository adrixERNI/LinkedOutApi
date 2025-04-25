using LinkedOutApi.DTOs.Auth;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces;

public interface IAuthService
{
    Task<GoogleUserDTO> AuthenticateWithGoogleAsync(string code);
    Task<Entities.User?> CreateUserAsync(string name, string email, string googleId, string role);
    Task<Entities.User?> GetUserByEmailAsync(string email);
}