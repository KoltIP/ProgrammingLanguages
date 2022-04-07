using ProgrammingLanguages.Identity;
using ProgrammingLanguages.Identity.Configuration;
using ProgrammingLanguages.Settings;
using ProgrammingLanguages.Settings.Settings;

var builder = WebApplication.CreateBuilder(args);

var settings = new IS4Settings(new SettingsSource());

var services = builder.Services;

services.AddAppCors();

services.AddAppDbContext(settings.Db);

services.AddHttpContextAccessor();

services.AddAppServices();

builder.Services.AddIS4();


//Start
var app = builder.Build();

app.UseAppCors();

app.UseStaticFiles();

app.UseRouting();

app.UseAppDbContext();

app.UseIS4();

app.Run();
