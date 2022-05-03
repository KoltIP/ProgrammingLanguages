using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;


namespace ProgrammingLanguages.Api.Test.Common.Extensions
{
    public static class TestServerExtensions
    {
        public static T ResolveService<T>(this TestServer testServer)
        {
            return testServer.Services.GetService<T>();
        }
    }

}
