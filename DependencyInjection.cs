using LinkedOutApi.Interfaces.Admin;
using LinkedOutApi.Interfaces;
using LinkedOutApi.Services.Admin;
using LinkedOutApi.Services;
using LinkedOutApi.Profiles;
using LinkedOutApi.Profiles.UserTraineeProfile;
using LinkedOutApi.Interfaces.User;
using LinkedOutApi.Repositories.Mentor;
using LinkedOutApi.Repositories.User;
using LinkedOutApi.Repositories.UserRepostiory;
using LinkedOutApi.Repositories.Admin;
using LinkedOutApi.Interfaces.Common;
using LinkedOutApi.Repositories.Common;

namespace LinkedOutApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMentorAssessmentRepository, MentorAssessmentRepository>();
            services.AddScoped<IUserSkillRepository, UserSkillRepository>();
            services.AddScoped<IBatchRepository, BatchRepository>();
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();

            return services;

        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBatchService, BatchService>();

            return services;
        }

        public static IServiceCollection AddMappingProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserTraineeProfile).Assembly);
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddAutoMapper(typeof(SkillProfile).Assembly);
            return services;
        }
    }
}
