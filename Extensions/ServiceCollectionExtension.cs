// Extensions/ServiceCollectionExtensions.cs
using LinkedOutApi.Interfaces;
using LinkedOutApi.Services;

namespace LinkedOutApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAwsService, AwsService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}
