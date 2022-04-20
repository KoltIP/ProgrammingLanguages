// Configure application
using ProgrammingLanguages.Settings;
using ProgrammingLanguages.Settings.Settings;
using ProgrammingLanguages.Worker;
using ProgrammingLanguages.Worker.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Settings for the initial configuration
var settings = new WorkerSettings(new SettingsSource());

// Logger
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog(logger, true);

// Configure services
var services = builder.Services;

services.AddHttpContextAccessor();
services.AddAppHealthCheck();
services.RegisterServices();


// Start application

var app = builder.Build();

app.UseAppHealthCheck();

app.StartTaskExecutor();

app.Run();