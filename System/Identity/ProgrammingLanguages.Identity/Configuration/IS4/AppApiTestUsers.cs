using Duende.IdentityServer.Test;

namespace ProgrammingLanguages.Identity.Configuration.IS4
{
    public static class AppApiTestUsers
    {
        public static List<TestUser> ApiUsers =>
            new List<TestUser>
            {
            new TestUser
            {
                SubjectId = "1",
                Username = "ilya@gmail.com",
                Password = "password"
            },
            new TestUser
            {
                SubjectId = "2",
                Username = "kolt@mail.com",
                Password = "password"
            }
            };
    }
}
