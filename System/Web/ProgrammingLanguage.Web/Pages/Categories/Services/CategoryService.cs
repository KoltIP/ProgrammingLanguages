using ProgrammingLanguage.Web.Pages.Categories.Models;
using ProgrammingLanguages.Shared.Common.Responses;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProgrammingLanguage.Web.Pages.Categories.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoryListItem>> GetCategories(int offset = 0, int limit = 10)
        {
            string url = $"{Settings.ApiRoot}/v1/category?offset={offset}&limit={limit}";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            var data = JsonSerializer.Deserialize<IEnumerable<CategoryListItem>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<CategoryListItem>();

            return data;
        }

        public async Task<CategoryListItem> GetCategory(int categoryId)
        {
            string url = $"{Settings.ApiRoot}/v1/category/{categoryId}";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            var data = JsonSerializer.Deserialize<CategoryListItem>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new CategoryListItem();

            return data;
        }

        public async Task<ErrorResponse> AddCategory(CategoryModel model)
        {
            string url = $"{Settings.ApiRoot}/v1/category";

            var body = JsonSerializer.Serialize(model);
            var request = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, request);

            var content = await response.Content.ReadAsStringAsync();

            var error = new ErrorResponse();
            if (!response.IsSuccessStatusCode)
            {
                error = JsonSerializer.Deserialize<ErrorResponse>(content);
                if (error.ErrorCode == -1)
                    error.Message = "An unexpected error has occurred. Transaction rejected.";
            }
            return error;

        }

        public async Task<ErrorResponse> EditCategory(int categoryId, CategoryModel model)
        {
            string url = $"{Settings.ApiRoot}/v1/category/{categoryId}";

            var body = JsonSerializer.Serialize(model);
            var request = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, request);

            var content = await response.Content.ReadAsStringAsync();

            var error = new ErrorResponse();
            if (!response.IsSuccessStatusCode)
            {
                error = JsonSerializer.Deserialize<ErrorResponse>(content);
                if (error.ErrorCode == -1)
                    error.Message = "An unexpected error has occurred. Transaction rejected.";
            }
            return error;
        }

        public async Task<ErrorResponse> DeleteCategory(int categoryId)
        {
            string url = $"{Settings.ApiRoot}/v1/category/{categoryId}";

            var response = await _httpClient.DeleteAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            var error = new ErrorResponse();
            if (!response.IsSuccessStatusCode)
            {
                error = JsonSerializer.Deserialize<ErrorResponse>(content);
                if (error.ErrorCode == -1)
                    error.Message = "An unexpected error has occurred. Transaction rejected.";
            }
            return error;
        }
    }
}
