using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProgrammingLanguages.Api.Test.Common
{
    public static class ConfigurationFactory
    {
        private static KeyValuePair<string, string> Val(string key, string value)
        {
            return new KeyValuePair<string, string>(key, value);
        }

        public static IEnumerable<KeyValuePair<string, string>> GetVariables()
        {
            var variables = new List<KeyValuePair<string, string>>
            {
                Val("CONNECTION_STRING_HOST", "localhost,host.docker.internal"),
                Val("CONNECTION_STRING_PORT", "25432"),
                Val("CONNECTION_STRING_DATABASE", "ProgrammingLanguages"),
                Val("CONNECTION_STRING_USER", "postgres"),
                Val("CONNECTION_STRING_PASSWORD", "123456789"),

                Val("IDENTITY_SERVER_URL", "http://localhost_is"),
                Val("IDENTITY_SERVER_CLIENT_ID", "frontend"),
                Val("IDENTITY_SERVER_CLIENT_SECRET", "secret"),
                Val("GENERAL_SWAGGER_VISIBLE", "false")
            }.ToList();

            return variables;
        }

        public static IConfigurationRoot GetApiConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .AddInMemoryCollection(GetVariables())
                .Build();
        }
    }
}

