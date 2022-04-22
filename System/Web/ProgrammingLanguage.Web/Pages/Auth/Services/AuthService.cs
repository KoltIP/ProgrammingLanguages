using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using ProgrammingLanguage.Web.Pages.Auth;
using ProgrammingLanguage.Web.Pages.Auth.Registr;
using ProgrammingLanguage.Web.Providers;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProgrammingLanguage.Web.Pages.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            var url = $"{Settings.IdentityRoot}/connect/token";

            var request_body = new[]
            {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", Settings.ClientId),
            new KeyValuePair<string, string>("client_secret", Settings.ClientSecret),
            new KeyValuePair<string, string>("username", loginModel.Email!),
            new KeyValuePair<string, string>("password", loginModel.Password!)
        };

            var requestContent = new FormUrlEncodedContent(request_body);

            var response = await _httpClient.PostAsync(url, requestContent);

            var content = await response.Content.ReadAsStringAsync();

            var loginResult = JsonSerializer.Deserialize<LoginResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new LoginResult();
            loginResult.Successful = response.IsSuccessStatusCode;

            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await _localStorage.SetItemAsync("authToken", loginResult.AccessToken);
            await _localStorage.SetItemAsync("refreshToken", loginResult.RefreshToken);

            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email!);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.AccessToken);

            return loginResult;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("refreshToken");

            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();

            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<RegistrErrorResponse> Registration(RegistrModel registrationModel)
        {

            string url = $"{Settings.ApiRoot}/v1/accounts";

            var body = JsonSerializer.Serialize(registrationModel);
            var request = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, request);

            var content = await response.Content.ReadAsStringAsync();


            var result = new RegistrErrorResponse();
            if (!response.IsSuccessStatusCode)
            {
                result = JsonSerializer.Deserialize<RegistrErrorResponse>(content);
                return result;
            }
            return result;
        }

        public async Task<bool> InspectConfirmEmail(string email)
        {
            string url = $"{Settings.ApiRoot}/v1/accounts/{email}";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            var result = JsonSerializer.Deserialize<bool>(content);

            return result;
        }

    }
}
