using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ProgrammingLanguage.Web;
using ProgrammingLanguage.Web.Pages.Auth.Services;
using ProgrammingLanguage.Web.Pages.Categories.Services;
using ProgrammingLanguage.Web.Pages.Languages.Models;
using ProgrammingLanguage.Web.Pages.Languages.Services;
using ProgrammingLanguage.Web.Pages.Operators.Services;
using ProgrammingLanguage.Web.Pages.Profile.Services;
using ProgrammingLanguage.Web.Providers;
using ProgrammingLanguage.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<IOperatorService, OperatorService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
