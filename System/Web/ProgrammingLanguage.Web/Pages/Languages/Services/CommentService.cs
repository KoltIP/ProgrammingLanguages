using Blazored.LocalStorage;
using ProgrammingLanguage.Web.Pages.Languages.Models;
using ProgrammingLanguages.Shared.Common.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;

namespace ProgrammingLanguage.Web.Pages.Languages.Services
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;


        public CommentService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }

        public async Task<ErrorResponse> AddComment(CommentModel model)
        {
            var token = await localStorage.GetItemAsync<string>("authToken");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            var idUser = tokenS.Claims.First(claim => claim.Type == "sub").Value;

            model.UserId = Guid.Parse(idUser);

            string url = $"{Settings.ApiRoot}/v1/comments/add";

            var body = JsonSerializer.Serialize(model);
            var request = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, request);

            var content = await response.Content.ReadAsStringAsync();

            var error = new ErrorResponse();
            if (!response.IsSuccessStatusCode)
            {
                error = JsonSerializer.Deserialize<ErrorResponse>(content);
                if (error.ErrorCode == -1)
                    error.Message = "Operation failed";
            }
            return error;

        }

        public async Task<ErrorResponse> DeleteComment(int commentId)
        {
            string url = $"{Settings.ApiRoot}/v1/comments/delete/{commentId}";

            var response = await httpClient.DeleteAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            var error = new ErrorResponse();
            if (!response.IsSuccessStatusCode)
            {
                error = JsonSerializer.Deserialize<ErrorResponse>(content);
                if (error.ErrorCode == -1)
                    error.Message = "Operation failed";
            }
            return error;

        }

        public async Task<CommentListItem> GetComment(int id)
        {
            string url = $"{Settings.ApiRoot}/v1/comments/get/one/{id}";

            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            var data = JsonSerializer.Deserialize<CommentListItem>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new CommentListItem();

            return data;

        }

        public async Task<IEnumerable<CommentListItem>> GetComments(int languageId)
        {
            string url = $"{Settings.ApiRoot}/v1/comments/get/many/{languageId}";

            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            var data = JsonSerializer.Deserialize<IEnumerable<CommentListItem>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<CommentListItem>();

            return data;

        }

        public async Task<ErrorResponse> UpdateComment(int commentId, CommentModel model)
        {
            var token = await localStorage.GetItemAsync<string>("authToken");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            var idUser = tokenS.Claims.First(claim => claim.Type == "sub").Value;

            model.UserId = Guid.Parse(idUser);

            string url = $"{Settings.ApiRoot}/v1/comments/update/{commentId}";

            var body = JsonSerializer.Serialize(model);
            var request = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(url, request);

            var content = await response.Content.ReadAsStringAsync();

            var error = new ErrorResponse();
            if (!response.IsSuccessStatusCode)
            {
                error = JsonSerializer.Deserialize<ErrorResponse>(content);
                if (error.ErrorCode == -1)
                    error.Message = "Operation failed";
            }
            return error;

        }

    }
}
