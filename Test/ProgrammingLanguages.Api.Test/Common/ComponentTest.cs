using Duende.IdentityServer.Test;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ProgrammingLanguages.Api.Test.Common.Extensions;
using ProgrammingLanguages.Api.Test.TestApiServer;
using ProgrammingLanguages.Api.Test.TestIdentityServer;
using ProgrammingLanguages.Db.Context.Context;
using ProgrammingLanguages.Db.Entities;
using ProgrammingLanguages.Settings.Interface;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace ProgrammingLanguages.Api.Test.Common
{
    public abstract partial class ComponentTest
    {
        protected TestServer apiServer;
        protected HttpClient apiClient;
        protected IApiSettings apiSettings;


        protected TestServer identityServer;
        protected HttpClient identityClient;


        protected IDbContextFactory<MainDbContext> contextFactory;
        protected UserManager<User> userManager;

        [OneTimeSetUp]
        public async virtual Task OneTimeSetUp()
        {
            identityServer = IdentityServerInitializer.Initialize(IdentityServerConfiguration, IdentityServerConfigurator);
            identityClient = identityServer.CreateClient();

            apiServer = ApiServerInitializer.Initialize(identityClient, ApiServerConfiguration, ApiServerConfigurator);
            apiClient = apiServer.CreateClient();

            contextFactory = apiServer.ResolveService<IDbContextFactory<MainDbContext>>();
            userManager = apiServer.ResolveService<UserManager<User>>();
            apiSettings = apiServer.ResolveService<IApiSettings>();
        }

        [TearDown]
        public async virtual Task TearDown()
        {
            await ClearDb();
        }

        [OneTimeTearDown]
        public async virtual Task OneTimeTearDown()
        {
            apiServer?.Dispose();
        }

        protected virtual IConfiguration? ApiServerConfiguration => null;
        protected virtual Action<IServiceCollection>? ApiServerConfigurator => null;

        protected virtual IConfiguration? IdentityServerConfiguration => null;
        protected virtual Action<IServiceCollection>? IdentityServerConfigurator => null;

        protected async Task<MainDbContext> DbContext() => await contextFactory.CreateDbContextAsync();

        protected async virtual Task ClearDb()
        {
            // Nothing to do
        }

        protected async Task<TokenResponse> AuthenticateTestUser(string username, string password, string scope)
        {
            var clientId = apiSettings.IdentityServer.ClientId;
            var clientSecret = apiSettings.IdentityServer.ClientSecret;
            return await identityClient.GetApiAccessToken(username, password, scope, clientId, clientSecret);
        }

        protected async Task GetTestUser()
        {
            var user = new User()
            {
                Status = UserStatus.Active,
                FullName = "test@test.ru",
                UserName = "test@test.ru",
                Email = "test@test.ru",
                EmailConfirmed = true,
                PhoneNumber = null,
                PhoneNumberConfirmed = false
            };
            await userManager.CreateAsync(user, "test");
            //return AppApiTestUser.First();
        }
    }

}
