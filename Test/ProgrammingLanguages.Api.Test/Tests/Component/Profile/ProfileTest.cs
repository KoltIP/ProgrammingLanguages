using NUnit.Framework;
using ProgrammingLanguages.Db.Entities;
using ProgrammingLanguages.Shared.Common.Security;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProgrammingLanguages.Api.Test.Common;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Profile
{
    [TestFixture]
    public partial class ProfileIntegrationTest : ComponentTest
    {
        const string TestEmail = "testovij@yandex.ru";
        const string TestPassword = "password";

        [SetUp]
        public async Task SetUp()
        {
            await using var context = await DbContext();
            context.Users.RemoveRange(context.Users);

            var user = new User()
            {
                Status = UserStatus.Active,
                FullName = TestEmail,
                UserName = TestEmail,
                Email = TestEmail,
                EmailConfirmed = true,
                PhoneNumber = null,
                PhoneNumberConfirmed = false
            };
            await userManager.CreateAsync(user, TestPassword);

            context.SaveChanges();
        }

        [TearDown]
        public async override Task TearDown()
        {
            await using var context = await DbContext();
            context.Users.RemoveRange(context.Users);
            context.SaveChanges();
            await base.TearDown();
        }

        protected static class Urls
        {
            public static string Register() => $"/api/v1/accounts";

            public static string GetUser(string token) => $"/api/v1/accounts/find/profile/{token}";

            public static string ChangeName(string token, string name) => $"/api/v1/accounts/change/name/{token}/{name}";

            public static string ChangeEmail(string token, string email) => $"/api/v1/accounts/change/email/{token}/{email}";

            public static string ChangePassword(string token) => $"/api/v1/accounts/change/password/{token}";


        }

        public static class Scopes
        {
            public static string ReadLanguage => $"offline_access {AppScopes.LanguageRead}";

            public static string WriteLanguage => $"offline_access {AppScopes.LanguageWrite}";

            public static string ReadAndWriteLanguage => $"offline_access {AppScopes.LanguageRead} {AppScopes.LanguageWrite}";

            public static string Empty => "offline_access";
        }

        public async Task<string> AuthenticateUser_ReadAndWriteLanguageScope()
        {
            var tokenResponse = await AuthenticateTestUser(TestEmail, TestPassword, Scopes.ReadAndWriteLanguage);
            return tokenResponse.AccessToken;
        }

        public async Task<string> AuthenticateUser_EmptyScope()
        {
            var tokenResponse = await AuthenticateTestUser(TestEmail, TestPassword, Scopes.Empty);
            return tokenResponse.AccessToken;
        }

        public async Task CheckTestUser()
        {
            var user1 = userManager.FindByEmailAsync(TestEmail);
            if (!user1.IsCompleted)
            {
                var user = new User()
                {
                    Status = UserStatus.Active,
                    FullName = TestEmail,
                    UserName = TestEmail,
                    Email = TestEmail,
                    EmailConfirmed = true,
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false
                };
                await userManager.CreateAsync(user, TestPassword);
            }
        }

        public async Task<Guid> GetExistedUserId()
        {
            return (await userManager.FindByEmailAsync(TestEmail)).Id;
        }

        public async Task<Guid> GetNotExistedGuidId()
        {
            return Guid.NewGuid();
        }

    }
}
