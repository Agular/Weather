using Hangfire;
using MongoDB.Driver;
using Refit;
using WebApi.Database.Configuration;
using WebApi.Database.Extensions;
using WebApi.Database.Repositories;
using WebApi.Integrations.OpenWeatherMap.Handlers;
using WebApi.Integrations.OpenWeatherMap.Interfaces;
using WebApi.Integrations.OpenWeatherMap.Services;
using WebApi.Models.Weather.Configuration;
using WebApi.Server.Extensions;
using WebApi.Services.Locations;
using WebApi.Services.Weather;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddOptions();
services.Configure<OwmConfiguration>(builder.Configuration.GetSection(OwmConfiguration.SectionKey));
services.Configure<WeatherAppConfiguration>(builder.Configuration.GetSection(WeatherAppConfiguration.SectionKey));
services.Configure<MongoDbConfiguration>(builder.Configuration.GetSection(MongoDbConfiguration.SectionKey));

var mongoDbConfiguration = builder.Configuration.GetSection(MongoDbConfiguration.SectionKey).Get<MongoDbConfiguration>();
var mongoClient = new MongoClient(mongoDbConfiguration.ConnectionString);
services.AddSingleton<IMongoClient>(mongoClient);

var openWeatherMapConfig = builder.Configuration.GetSection(OwmConfiguration.SectionKey).Get<OwmConfiguration>();
services.AddTransient<OwmAuthHandler>();
services.AddRefitClient<IOpenWeatherMapApi>()
	.ConfigureHttpClient(c => c.BaseAddress = new Uri(openWeatherMapConfig.Endpoint))
	.AddHttpMessageHandler<OwmAuthHandler>();

services.AddScoped<ILocationService, LocationService>();
services.AddScoped<IWeatherService, WeatherService>();
services.AddScoped<ICommonRepository, CommonRepository>();
services.AddScoped<IExternalWeatherService, OwmWeatherService>();

builder.AddCustomHangfire(mongoClient, mongoDbConfiguration.DatabaseName);
services.AddHangfireServer();

var app = builder.Build();

await app.SeedDatabase();
app.AddHangfireDashboard();
app.AddHangfireJobs();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
