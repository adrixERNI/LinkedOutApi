//using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LinkedOutApi.Models;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.IdentityModel.Tokens;

public class JwtService : IJwtService
{
    private readonly string _secret;
    private readonly string _issuer;
    private readonly string _audience;

    public JwtService()
    {
        _secret = Environment.GetEnvironmentVariable("JWT_SECRET")!;
        _issuer = Environment.GetEnvironmentVariable("JWT_ISSUER")!;
        _audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")!;

        if (string.IsNullOrEmpty(_secret) || string.IsNullOrEmpty(_issuer) || string.IsNullOrEmpty(_audience))
        {
            throw new InvalidOperationException("JWT configuration is missing in environment variables.");
        }
    }

    public async Task<string> GenerateTokenAsync(string userId,string googleId, UserRole role)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("googleId", googleId),
            new Claim(ClaimTypes.Role, role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //var token = new JwtSecurityToken(
        //    issuer: _issuer,
        //    audience: _audience,
        //    claims: claims,
        //    expires: DateTime.Now.AddMinutes(60),
        //    signingCredentials: creds);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = creds,
            Issuer = _issuer,
            Audience = _audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        Console.WriteLine(ValidateToken(tokenString));
        return tokenString;
        //return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secret);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _issuer,
            ValidAudience = _audience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            return principal;
        }
        catch
        {
            Console.WriteLine("Somethingwentwrong");
            return null!;
        }
    }
}