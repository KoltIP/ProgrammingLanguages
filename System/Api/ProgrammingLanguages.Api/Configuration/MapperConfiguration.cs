using ProgrammingLanguages.Shared.Common.Helpers;

namespace ProgrammingLanguages.Api.Configuration
{
    public static class MapperConfiguration
    {
        public static IServiceCollection AddAutoMappers(this IServiceCollection services)
        {
            AutoMappersRegisterHelper.Register(services);

            return services;
        }
    }
}
