using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProgrammingLanguage.Web.Pages.Languages.Models
{
    public class LanguageService : ILanguageService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public LanguageService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<IEnumerable<LanguageListItem>> GetLanguages(int offset = 0, int limit = 10)
        {
            string url = $"{Settings.ApiRoot}/v1/language?offset={offset}&limit={limit}";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            var data = JsonSerializer.Deserialize<IEnumerable<LanguageListItem>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<LanguageListItem>();

            return data;
        }

        public async Task<LanguageListItem> GetLanguage(int languageId)
        {
            string url = $"{Settings.ApiRoot}/v1/language/{languageId}";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            var data = JsonSerializer.Deserialize<LanguageListItem>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new LanguageListItem();

            return data;
        }

        public async Task AddLanguage(LanguageModel model)
        {
            string url = $"{Settings.ApiRoot}/v1/language";

            var body = JsonSerializer.Serialize(model);
            var request = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, request);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }

        public async Task EditLanguage(int languageId, LanguageModel model)
        {
            string url = $"{Settings.ApiRoot}/v1/language/{languageId}";

            var body = JsonSerializer.Serialize(model);
            var request = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, request);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }

        public async Task DeleteLanguage(int languageId)
        {
            string url = $"{Settings.ApiRoot}/v1/language/{languageId}";

            var response = await _httpClient.DeleteAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }

        public async Task<IEnumerable<CategoryModel>> GetCategoryList()
        {
            string url = $"{Settings.ApiRoot}/v1/category";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            var data = JsonSerializer.Deserialize<IEnumerable<CategoryModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<CategoryModel>();

            return data;
        }

        public async Task AddSubscribe(int languageId)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            var idUser = tokenS.Claims.First(claim => claim.Type == "sub").Value;

            SubscribeModel model = new SubscribeModel()
            {
                LanguageId = languageId,
                UserId = Guid.Parse(idUser)
            };

            string url = $"{Settings.ApiRoot}/v1/language/sub";

            var body = JsonSerializer.Serialize(model);
            var request = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, request);

            var content = await response.Content.ReadAsStringAsync();

            //var error = new ErrorResponse();
            //if (!response.IsSuccessStatusCode)
            //{
            //    error = JsonSerializer.Deserialize<ErrorResponse>(content);
            //    if (error.ErrorCode == -1)
            //        error.Message = "Operation failed";
            //}
            //return error;
        }

    }
}

