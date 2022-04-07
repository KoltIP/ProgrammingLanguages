using ProgrammingLanguages.Api;
using ProgrammingLanguages.Api.Configuration;
using ProgrammingLanguages.Settings;
using ProgrammingLanguages.Settings.Settings;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Logger
builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
{
    loggerConfiguration
    .Enrich.WithCorrelationId()
    .ReadFrom.Configuration(hostBuilderContext.Configuration);
});

var settings = new ApiSettings(new SettingsSource());

// Add services to the container.

var services = builder.Services;

services.AddHttpContextAccessor();

services.AddAppDbContext(settings);

services.AddAppHealthCheck();

services.AddAppVersions();

services.AddAppSwagger(settings);

services.AddAppCors();

services.AddAppAuth(settings);

services.AddAppServices();

services.AddControllers().AddValidator();

services.AddRazorPages();

services.AddAutoMappers();

var app = builder.Build();

// Configure the HTTP request pipeline.

Log.Information("Start");

app.UseMiddlewares();

app.UseStaticFiles();

app.UseRouting();

app.UseAppCors();

app.UseAppHealthCkeck();

app.UseSerilogRequestLogging();

app.UseAppSwagger();

app.UseAppAuth();

app.MapRazorPages();

app.MapControllers();

app.UseAppDbContext();

app.Run();