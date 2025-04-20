using System.Text.Json;
using LinkedOutApi.Data;
using LinkedOutApi.DTOs.Auth;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkedOutApi.Services;

public class AuthService : IAuthService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<AuthService> _logger;
    private readonly AppDbContext _db;


    public AuthService(IHttpClientFactory httpClientFactory, ILogger<AuthService> logger, AppDbContext db)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _db = db;
    }

    public async Task<GoogleUserDTO> AuthenticateWithGoogleAsync(string code)
    {
        var http = _httpClientFactory.CreateClient();

        var values = new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID")! },
                { "client_secret", Environment.GetEnvironmentVariable("GOOGLE_SECRET")! },
                { "redirect_uri", Environment.GetEnvironmentVariable("REDIRECT_URI_DEV")! },
                { "grant_type", "authorization_code" }
            };

        var content = new FormUrlEncodedContent(values);
        var response = await http.PostAsync("https://oauth2.googleapis.com/token", content);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Token exchange failed");

        var json = await response.Content.ReadAsStringAsync();
        var tokenResult = JsonSerializer.Deserialize<GoogleTokenResponseDTO>(json);

        if (tokenResult == null || string.IsNullOrEmpty(tokenResult.AccessToken))
            throw new Exception("Invalid token response");

        var userRequest = new HttpRequestMessage(HttpMethod.Get, "https://www.googleapis.com/oauth2/v2/userinfo");
        userRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenResult.AccessToken);
        var userResponse = await http.SendAsync(userRequest);

        if (!userResponse.IsSuccessStatusCode)
            throw new Exception("Failed to get user info");

        var userJson = await userResponse.Content.ReadAsStringAsync();
        var googleUser = JsonSerializer.Deserialize<GoogleUserDTO>(userJson);

        if (googleUser == null)
            throw new Exception("Invalid user info response");

        return googleUser;
    }

    public async Task<User?> CreateUserAsync(string name, string email, string role)
    {
        try
        {
            var existingUser = await _db.Users.FirstOrDefaultAsync(user => user.Email == email);

            if (existingUser != null)
                return existingUser;

            var user = new User
            {
                Name = name,
                Email = email,
                Role = role,
                IsApproved = false
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return user;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating user: {ex.Message}");
        }
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        try
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving user by email: {ex.Message}");
        }
    }
}