using System.Net.Http;
using System.Text.Json;
using LinkedOutApi.Data;
using LinkedOutApi.DTOs.Auth;
using LinkedOutApi.Entities;
using LinkedOutApi.Exceptions;
using LinkedOutApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
            throw new HttpResponseException("Token exchange failed", 400);

        var json = await response.Content.ReadAsStringAsync();
        var tokenResult = JsonSerializer.Deserialize<GoogleTokenResultDTO>(json);

        if (tokenResult == null || string.IsNullOrEmpty(tokenResult.AccessToken))
            throw new HttpResponseException("Invalid token response", 400);

        var userRequest = new HttpRequestMessage(HttpMethod.Get, "https://www.googleapis.com/oauth2/v2/userinfo");
        userRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenResult.AccessToken);
        var userResponse = await http.SendAsync(userRequest);

        if (!userResponse.IsSuccessStatusCode)
            throw new HttpResponseException("Failed to get user info", 400);

        var userJson = await userResponse.Content.ReadAsStringAsync();
        var googleUser = JsonSerializer.Deserialize<GoogleUserDTO>(userJson);

        if (googleUser == null)
            throw new HttpResponseException("Invalid user info response", 400);

        return googleUser;
    }

    public async Task<User?> CreateUserAsync(string name, string email, string googleId, int role)
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
                RoleId = role,
                GoogleId = googleId,
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

    /*
    LinkedIn Authentication Implementation
    Key differences from Google:
    1. Different endpoints:
       - Token exchange: https://www.linkedin.com/oauth/v2/accessToken
       - User info: https://api.linkedin.com/v2/me
    2. Different required scopes: r_liteprofile r_emailaddress
    3. Different response structure
    4. Different environment variables needed:
       - LINKEDIN_CLIENT_ID
       - LINKEDIN_SECRET
       - LINKEDIN_REDIRECT_URI
    */
    public async Task<LinkedInUserDTO> AuthenticateWithLinkedInAsync(string code)
    {
        var http = _httpClientFactory.CreateClient();

        var values = new Dictionary<string, string>
        {
            { "code", code },
            { "client_id", Environment.GetEnvironmentVariable("LINKEDIN_CLIENT_ID")! },
            { "client_secret", Environment.GetEnvironmentVariable("LINKEDIN_SECRET")! },
            { "redirect_uri", Environment.GetEnvironmentVariable("LINKEDIN_REDIRECT_URI")! },
            { "grant_type", "authorization_code" }
        };

        var content = new FormUrlEncodedContent(values);
        var response = await http.PostAsync("https://www.linkedin.com/oauth/v2/accessToken", content);

        if (!response.IsSuccessStatusCode)
            throw new HttpResponseException("LinkedIn token exchange failed", 400);

        var json = await response.Content.ReadAsStringAsync();
        var tokenResult = JsonSerializer.Deserialize<LinkedInTokenResultDTO>(json);

        if (tokenResult == null || string.IsNullOrEmpty(tokenResult.AccessToken))
            throw new HttpResponseException("Invalid LinkedIn token response", 400);

        // LinkedIn requires a different endpoint and headers for user info
        var userRequest = new HttpRequestMessage(HttpMethod.Get, "https://api.linkedin.com/v2/me");
        userRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenResult.AccessToken);
        userRequest.Headers.Add("X-Restli-Protocol-Version", "2.0.0");
        
        var userResponse = await http.SendAsync(userRequest);

        if (!userResponse.IsSuccessStatusCode)
            throw new HttpResponseException("Failed to get LinkedIn user info", 400);

        var userJson = await userResponse.Content.ReadAsStringAsync();
        var linkedInUser = JsonSerializer.Deserialize<LinkedInUserDTO>(userJson);

        if (linkedInUser == null)
            throw new HttpResponseException("Invalid LinkedIn user info response", 400);

        return linkedInUser;
    }

    /*
    Auth0 Authentication Implementation
    This method is used to get user info from Auth0's userinfo endpoint
    after the frontend has already authenticated with Auth0
    */
    public async Task<Auth0UserDTO> GetAuth0UserInfoAsync(string accessToken)
    {
        var http = _httpClientFactory.CreateClient();

        var userRequest = new HttpRequestMessage(HttpMethod.Get, $"https://{Environment.GetEnvironmentVariable("AUTH0_DOMAIN")}/userinfo");
        userRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        
        var userResponse = await http.SendAsync(userRequest);

        if (!userResponse.IsSuccessStatusCode)
            throw new HttpResponseException("Failed to get Auth0 user info", 400);

        var userJson = await userResponse.Content.ReadAsStringAsync();
        var auth0User = JsonSerializer.Deserialize<Auth0UserDTO>(userJson);

        if (auth0User == null)
            throw new HttpResponseException("Invalid Auth0 user info response", 400);

        return auth0User;
    }
}