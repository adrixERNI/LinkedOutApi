using LinkedOutApi.DTOs.Auth;
using LinkedOutApi.DTOs.Response;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces;
using LinkedOutApi.Models;
using Microsoft.AspNetCore.Authorization;
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
    [ProducesResponseType(typeof(SuccessResponseDTO), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
    public async Task<ActionResult<SuccessResponseDTO>> GoogleSignIn([FromBody] GoogleSignInRequestDTO request)
    {
        Console.WriteLine(request.Code);
        var userInfo = await _authService.AuthenticateWithGoogleAsync(request.Code);
        var existingUser = await _authService.GetUserByEmailAsync(userInfo.Email);
        User user;
        if (existingUser == null)
        {
            var imageStream = await _imageService.DownloadImageAsync(userInfo.Picture);
            //insert image repo call here
            var storedUser = await _authService.CreateUserAsync(userInfo.Name, userInfo.Email, userInfo.Id, 1);
            user = storedUser!;
            var fileKey = $"profile-pictures/{storedUser!.Id}.jpg";
            //also  edIT Image table after
            //does not exist 
            var pictureUrl = await _awsService.UploadImageToS3Async(imageStream, fileKey);
        }
        else
        {
            user = existingUser;
        }

        var accessToken = await _jwtService.GenerateTokenAsync(user.Id.ToString(),userInfo.Id, UserRole.Trainee); //userID, googleID, Role
        Response.Cookies.Append("access_token", accessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = DateTime.Now.AddMinutes(30)
        });

        return Ok(new SuccessResponseDTO
        {
            Message = "Login successfully",
            StatusCode = 200,
            Success = true
        });
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

    [Authorize]
    [HttpGet("test")]
    public IActionResult GetUserDetailsForTrainee()
    {
        return Ok(new { name = Environment.GetEnvironmentVariable("JWT_SECRET")! });
    }

    [HttpGet("test/trainee-token")]
    public async Task<IActionResult> GetTraineeToken() 
    {
        var accessToken = await _jwtService.GenerateTokenAsync("3FA85F64-5717-4562-B3FC-2C963F66AFA4", "123SAMPLEGOOGLEID", UserRole.Trainee);
        return Ok(accessToken);
    }
    [HttpGet("test/mentor-token")]
    public async Task<IActionResult> GetMentorToken()
    {
        var accessToken = await _jwtService.GenerateTokenAsync("3FA85F64-5717-4562-B3FC-2C963F66AFA6", "123SAMPLEGOOGLEID", UserRole.Mentor);
        return Ok(accessToken);
    }
}

