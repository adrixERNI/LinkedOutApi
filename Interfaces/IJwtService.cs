using System.Security.Claims;
using LinkedOutApi.Models;

public interface IJwtService
{
    Task<string> GenerateTokenAsync(string userId, UserRole role);
    ClaimsPrincipal ValidateToken(string token);
}