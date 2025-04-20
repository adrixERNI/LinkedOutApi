using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost("google")]
    public async Task<IActionResult> ExchangeGoogleCode([FromBody] GoogleAuthRequest request)
    {
        var http = _httpClientFactory.CreateClient();

        var values = new Dictionary<string, string>
        {
            { "code", request.Code },
            { "client_id", "YOUR_GOOGLE_CLIENT_ID" },
            { "client_secret", "YOUR_GOOGLE_CLIENT_SECRET" },
            { "redirect_uri", "http://localhost:3000" },
            { "grant_type", "authorization_code" }
        };

        var content = new FormUrlEncodedContent(values);
        var response = await http.PostAsync("https://oauth2.googleapis.com/token", content);

        var responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return BadRequest(new { message = "Token exchange failed", details = responseString });
        }

        var tokenResult = JsonSerializer.Deserialize<GoogleTokenResponse>(responseString);
        return Ok(tokenResult);
    }
}

public class GoogleAuthRequest
{
    public string Code { get; set; } = string.Empty;
}

public class GoogleTokenResponse
{
    public string access_token { get; set; } = string.Empty;
    public int expires_in { get; set; }
    public string scope { get; set; } = string.Empty;
    public string token_type { get; set; } = string.Empty;
    public string id_token { get; set; } = string.Empty;
}
