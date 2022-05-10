using ProgrammingLanguage.Web.Pages.Operators.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ProgrammingLanguages.Shared.Common.Responses;

namespace ProgrammingLanguage.Web.Pages.Operators.Services
{
    public class OperatorService : IOperatorService
    {
        private readonly HttpClient _httpClient;

        public OperatorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ErrorResponse> AddOperator(OperatorModel model)
        {
            string url = $"{Settings.ApiRoot}/v1/operator";

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

        public async Task<ErrorResponse> DeleteOperator(int operatorId)
        {
            string url = $"{Settings.ApiRoot}/v1/operator/{operatorId}";

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

        public async Task<ErrorResponse> EditOperator(int operatorId, OperatorModel model)
        {
            string url = $"{Settings.ApiRoot}/v1/operator/{operatorId}";

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

        public async Task<IEnumerable<LanguageModel>> GetLanguageList()
        {
            string url = $"{Settings.ApiRoot}/v1/language";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            var data = JsonSerializer.Deserialize<IEnumerable<LanguageModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<LanguageModel>();

            return data;
        }

        public async Task<OperatorListItem> GetOperator(int operatorId)
        {
            string url = $"{Settings.ApiRoot}/v1/operator/{operatorId}";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            var data = JsonSerializer.Deserialize<OperatorListItem>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new OperatorListItem();

            return data;
        }

        public async Task<IEnumerable<OperatorListItem>> GetOperators(int offset = 0, int limit = 10)
        {
            string url = $"{Settings.ApiRoot}/v1/operator?offset={offset}&limit={limit}";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            var data = JsonSerializer.Deserialize<IEnumerable<OperatorListItem>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<OperatorListItem>();

            return data;
        }
    }
}
