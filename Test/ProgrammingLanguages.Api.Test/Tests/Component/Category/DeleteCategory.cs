using NUnit.Framework;
using ProgrammingLanguages.Api.Test.Common.Extensions;
using System.Net;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Category
{
    public partial class CategoryIntegrationTest
    {
        [Test]
        public async Task DeleteCategory_ValidParameters_Authenticated_OkResponse()
        {
            int id = await GetExistedCategoryId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.DeleteCategory(id.ToString());
            var response = await apiClient.Delete(url, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task DeleteCategory_NegativeParameters_OkResponse()
        {
            int id = await GetNotExistedCategoryId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.DeleteCategory(id.ToString());
            var response = await apiClient.Delete(url, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task DeleteCategory_Unauthorized()
        {
            int id = await GetExistedCategoryId();
            var url = Urls.DeleteCategory(id.ToString());
            var response = await apiClient.Delete(url);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task DeleteCategory_EmptyScope_Forbidden()
        {
            int id = await GetExistedCategoryId();
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.DeleteCategory(id.ToString());
            var response = await apiClient.Delete(url, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}
