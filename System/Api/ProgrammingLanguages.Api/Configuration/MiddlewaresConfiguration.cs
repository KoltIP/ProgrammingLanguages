using ProgrammingLanguages.Api.Middlewares;

namespace ProgrammingLanguages.Api.Configuration
{
    public static class MiddlewaresConfiguration
    {
        public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionsMiddleware>();
        }
    }
}
