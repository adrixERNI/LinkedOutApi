using System.Security.Claims;
using LinkedOutApi.Models;

public interface IJwtService
{
    Task<string> GenerateTokenAsync(string userId, string googleId, UserRole role);
    //ClaimsPrincipal ValidateToken(string token);
}