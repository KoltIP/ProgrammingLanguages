using ProgrammingLanguages.Api.Controllers.Languages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Language
{
    public partial class LanguageIntegrationTest
    {
        public static AddLanguageRequest AddLanguageRequest(int categoryId, string name, string description)
        {
            return new AddLanguageRequest()
            {
                CategoryId = categoryId,
                Name = name,
                Description = description
            };
        }

        public static UpdateLanguageRequest UpdateLanguageRequest(int categoryId, string name, string description)
        {
            return new UpdateLanguageRequest()
            {
                CategoryId = categoryId,
                Name = name,
                Description = description
            };
        }
    }
}
