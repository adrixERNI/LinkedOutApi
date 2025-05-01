using System.Text.Json.Serialization;

namespace LinkedOutApi.DTOs.Auth;

public class LinkedInTokenResultDTO
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
} 

// ONLY FOR LINKEDIN; TBD