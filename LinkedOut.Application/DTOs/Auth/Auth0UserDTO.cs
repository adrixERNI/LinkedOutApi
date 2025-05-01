using System.Text.Json.Serialization;

namespace LinkedOutApi.DTOs.Auth;

public class Auth0UserDTO
{
    [JsonPropertyName("sub")]
    public string Sub { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("picture")]
    public string? Picture { get; set; }
} 

// ONLY FOR LINKEDIN; TBD