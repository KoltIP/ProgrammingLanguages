namespace ProgrammingLanguages.Api.Test.Tests.Component.Category
{
    public partial class CategoryIntegrationTest
    {
        public static class Generator
        {
            public static string[] ValidNames =
            {
            new string('1',1),
            new string('1',20),
            new string('1',50)
        };

            public static string[] InvalidNames =
            {
            null,
            "",
            new string('1',51),
        };

            public static string[] ValidDescriptions =
            {
            "",
            new string('1',1),
            new string('1',50),
        };

            public static string[] InvalidDescriptions =
            {
                null,
            new string('1',51),
        };

        };
        
    }
}
