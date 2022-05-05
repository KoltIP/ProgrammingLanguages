namespace ProgrammingLanguages.Api.Test.Tests.Component.Comment
{
    public static class Generator
    {
        public static string[] ValidComments =
        {
            new string('1',1),
            new string('1',1000)
        };

        public static string[] InvalidComments =
        {
            null,
            "",
            new string('1',1001)
        };
    }

}
