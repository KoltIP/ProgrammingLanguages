namespace ProgrammingLanguages.Api.Test.Tests.Component.Operator
{
    public partial class OperatorIntegrationTest
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

            public static int[] InvalidLanguageIds =
            {
            0,
            -1,
            int.MaxValue
        };
        }
    }
}
