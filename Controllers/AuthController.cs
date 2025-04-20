using LinkedOutApi.DTOs.Auth;
using LinkedOutApi.Interfaces;
using LinkedOutApi.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IWebHostEnvironment _env;
    private readonly IAwsService _awsService;
    private readonly IAuthService _authService;
    private readonly IImageService _imageService;
    private readonly IJwtService _jwtService;


    public AuthController(
        IHttpClientFactory httpClientFactory,
        IWebHostEnvironment env,
        IAwsService awsService,
        IAuthService authService,
        IImageService imageService,
        IJwtService jwtService)
    {
        _httpClientFactory = httpClientFactory;
        _env = env;
        _awsService = awsService;
        _authService = authService;
        _imageService = imageService;
        _jwtService = jwtService;
    }

    [HttpPost("google/trainee")]
    public async Task<ActionResult<GoogleSignInResponseDTO>> GoogleSignIn([FromBody] GoogleSignInRequestDTO request)
    {
        try
        {

            var userInfo = await _authService.AuthenticateWithGoogleAsync(request.Code);
            var existingUser = await _authService.GetUserByEmailAsync(userInfo.Email);

            if (existingUser == null)
            {
                var imageStream = await _imageService.DownloadImageAsync(userInfo.Picture);
                var storedUser = await _authService.CreateUserAsync(userInfo.Name, userInfo.Email, "trainee");
                var fileKey = $"profile-pictures/{storedUser!.Id}.jpg";
                var pictureUrl = await _awsService.UploadImageToS3Async(imageStream, fileKey);
            }

            var accessToken = await _jwtService.GenerateTokenAsync("1", UserRole.Trainee);

            Response.Cookies.Append("access_token", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.Now.AddMinutes(30)
            });

            return Ok(new { message = "Login successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // [HttpPost("jwt/generate")]
    // public async Task<IActionResult> GenerateJWT()
    // {
    //     var token = await _jwtService.GenerateTokenAsync("1", "trainee");
    //     return Ok(new { token });
    // }

    // [Authorize(Roles = "admin")]
    // [HttpGet("user/admin-role")]
    // public IActionResult GetUserDetailsForAdmin()
    // {
    //     return Ok(new { name = "Rodel" });
    // }

    // [Authorize(Roles = "trainee")]
    // [HttpGet("user/trainee-role")]
    // public IActionResult GetUserDetailsForTrainee()
    // {
    //     return Ok(new { name = "Rodel" });
    // }
}