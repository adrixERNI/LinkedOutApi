using System.Text.Json.Serialization;

namespace LinkedOutApi.DTOs.Auth;

public class LinkedInUserDTO
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("localizedFirstName")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("localizedLastName")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("emailAddress")]
    public string Email { get; set; } = string.Empty;

    public string Name => $"{FirstName} {LastName}";
} 

// ONLY FOR LINKEDIN; TBD