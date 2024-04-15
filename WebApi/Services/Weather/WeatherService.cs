using Hangfire;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApi.Database.Repositories;
using WebApi.Framework;
using WebApi.Integrations.OpenWeatherMap.Interfaces;
using WebApi.Models.Weather.Configuration;
using WebApi.Models.Weather.Entities;
using WebApi.Models.Weather.Extensions;
using WebApi.Models.Weather.Transport;
using WebApi.Services.Base;
using WebApi.Services.Locations;

namespace WebApi.Services.Weather;

public interface IWeatherService {
	Task<WeatherDetailsDto?> GetLocationCurrentWeatherDetails(string locationName);

	[DisableConcurrentExecution(5 * 60)]
	Task SyncDefaultLocationsCurrentWeather();
}

public class WeatherService : BaseService<WeatherService>, IWeatherService {
	private readonly WeatherAppConfiguration _configuration;
	private readonly ILocationService _locationService;
	private readonly IExternalWeatherService _externalWeatherService;

	public WeatherService(ICommonRepository commonRepository,
		ILogger<WeatherService> logger,
		IOptions<WeatherAppConfiguration> configurationOptions,
		ILocationService locationService,
		IExternalWeatherService externalWeatherService) : base(commonRepository, logger) {
		_configuration = configurationOptions.Value;
		_locationService = locationService;
		_externalWeatherService = externalWeatherService;
	}

	public async Task<WeatherDetailsDto?> GetLocationCurrentWeatherDetails(string locationName) {
		var filter = Builders<WeatherDetails>.Filter.Eq(r => r.LocationName, locationName);
		var findOptions = new FindOptions<WeatherDetails> {
			Sort = Builders<WeatherDetails>.Sort.Descending(r => r.MeasuredTime)
		};

		var details = await CommonRepository.Get(filter, findOptions);
		var dto = details?.ToDto();
		return dto;
	}

	public async Task SyncDefaultLocationsCurrentWeather() {
		var defaultLocations = _configuration.DefaultLocations.SplitConfig();
		if (defaultLocations.IsEmpty()) {
			Logger.LogInformation("WeatherService.SyncDefaultLocationsCurrentWeather: Skipped sync since no locations are present in current configuration.");
			return;
		}

		var locations = await _locationService.GetLocationsByNames(defaultLocations);
		if (locations.IsEmpty()) {
			Logger.LogError($"WeatherService.SyncDefaultLocationsCurrentWeather: No matching locations found in database for configured locations, locations={string.Join(", ", defaultLocations)}");
			return;
		}

		foreach (var location in locations) {
			var extWeatherDetailsResult = await _externalWeatherService.GetCurrentWeatherDetails(location);
			if (!extWeatherDetailsResult.IsValid) {
				Logger.LogError($"WeatherService.SyncDefaultLocationsCurrentWeather: Something went wrong when fetching external weather data for location {location.Name}");
				continue;
			}

			var newDbWeatherDetails = extWeatherDetailsResult.GetResult().ToDocument();
			newDbWeatherDetails.LocationName = location.Name;
			await CommonRepository.Add(newDbWeatherDetails);
			Logger.LogInformation($"WeatherService.SyncDefaultLocationsCurrentWeather: Successfully synced current weather data, location={location.Name}");
		}
	}
}