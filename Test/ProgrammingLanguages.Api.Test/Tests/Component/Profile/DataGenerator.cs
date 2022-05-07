namespace ProgrammingLanguages.Api.Test.Tests.Component.Profile
{
    public partial class ProfileIntegrationTest
    {
        public static class Generator
        {

            public static string[] ValidNames =
        {
            new string('1',5),
            new string('1',100)
        };

            public static string[] InvalidNames =
            {
            new string('1',101),
        };

            public static string[] ValidEmails =
            {
            new string("ilya.kolt@gmail.com")
        };

            public static string[] InvalidEmails =
            {
            new string('1',101)
        };

            public static string[] ValidPasswords =
            {
            new string('1',3),
            new string('1',100)
        };

            public static string[] InvalidPasswords =
            {
            null,
            "",
            new string('1',101),
        };
        }

    }
}
