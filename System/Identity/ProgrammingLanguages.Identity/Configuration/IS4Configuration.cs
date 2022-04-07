using Microsoft.AspNetCore.Identity;
using ProgrammingLanguages.Db.Context.Context;
using ProgrammingLanguages.Db.Entities;
using ProgrammingLanguages.Identity.Configuration.IS4;

namespace ProgrammingLanguages.Identity.Configuration
{
    public static class IS4Configuration
    {
        public static IServiceCollection AddIS4(this IServiceCollection services)
        {
            services
                .AddIdentity<User, IdentityRole<Guid>>(opt =>
                {
                    opt.Password.RequiredLength = 0;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<MainDbContext>()
                .AddUserManager<UserManager<User>>()
                .AddDefaultTokenProviders();

            services
                .AddIdentityServer()
                .AddAspNetIdentity<User>()
                .AddInMemoryApiScopes(AppApiScopes.ApiScopes)
                .AddInMemoryClients(AppClients.Clients)
                .AddInMemoryApiResources(AppResources.Resources)
                .AddInMemoryIdentityResources(AppIdentityResources.Resources)

                .AddTestUsers(AppApiTestUsers.ApiUsers)

                .AddDeveloperSigningCredential();

            return services;
        }

        public static IApplicationBuilder UseIS4(this IApplicationBuilder app)
        {
            app.UseIdentityServer();

            return app;
        }
    }
}
