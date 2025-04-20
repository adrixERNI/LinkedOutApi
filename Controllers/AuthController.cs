using LinkedOutApi.DTOs.Auth;
using LinkedOutApi.Interfaces;
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


    public AuthController(IHttpClientFactory httpClientFactory, IWebHostEnvironment env, IAwsService awsService, IAuthService authService, IImageService imageService)
    {
        _httpClientFactory = httpClientFactory;
        _env = env;
        _awsService = awsService;
        _authService = authService;
        _imageService = imageService;
    }

    [HttpPost("google/trainee")]
    public async Task<IActionResult> GoogleSignIn([FromBody] GoogleSignInRequestDTO request)
    {
        try
        {

            var userInfo = await _authService.AuthenticateWithGoogleAsync(request.Code);

            var existingUser = await _authService.GetUserByEmailAsync(userInfo.Email);

            if (existingUser != null)
            {
                return Ok(new { message = "Were done" });
            }

            var imageStream = await _imageService.DownloadImageAsync(userInfo.Picture);

            var storedUser = await _authService.CreateUserAsync(userInfo.Name, userInfo.Email, "trainee");

            var fileKey = $"profile-pictures/{storedUser!.Id}.jpg"; // File name should be userId or atleast uniquely bind to user
            var pictureUrl = await _awsService.UploadImageToS3Async(imageStream, fileKey);

            return Ok(new { userInfo.Name, userInfo.Email, PictureUrl = pictureUrl });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


}


