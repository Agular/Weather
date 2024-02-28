using Refit;
using WebApi.Integrations.Base.Services;
using WebApi.Integrations.OpenWeatherMap.Interfaces;
using WebApi.Integrations.OpenWeatherMap.Models;
using WebApi.Models.Base.Transport;
using WebApi.Models.Locations.Transport;
using WebApi.Models.Weather.Transport;

namespace WebApi.Integrations.OpenWeatherMap.Services;

public class OwmWeatherService : BaseIntegrationService<OwmWeatherService>, IExternalWeatherService {
	private readonly IOpenWeatherMapApi _openWeatherMapApi;

	public OwmWeatherService(ILogger<OwmWeatherService> logger, IOpenWeatherMapApi openWeatherMapApi) : base(logger) {
		_openWeatherMapApi = openWeatherMapApi;
	}

	public async Task<BaseResult<WeatherDetailsDto>> GetCurrentWeatherDetails(LocationDto locationDto) {
		return await GetCurrentWeatherDetails(locationDto.Latitude, locationDto.Longitude);
	}

	public async Task<BaseResult<WeatherDetailsDto>> GetCurrentWeatherDetails(double latitude, double longitude) {
		try {
			var request = new OwmWeatherDataRequest {
				Latitude = latitude,
				Longitude = longitude,
				Units = "metric"
			};

			var response = await _openWeatherMapApi.GetCurrentWeatherData(request);
			var weatherDetailsDto = ToDto(response);
			return new BaseResult<WeatherDetailsDto>(weatherDetailsDto);
		}
		catch (ApiException ex) {
			await LogException(ex);
			return BaseResult<WeatherDetailsDto>.Faulty("Failed to fetch current weather details from external API.");
		}
	}

	private static WeatherDetailsDto ToDto(OwmWeatherData owmWeatherData) {
		var offsetSeconds = owmWeatherData.TimeZoneSeconds;
		var weatherDetailsDto = new WeatherDetailsDto {
			LocationName = owmWeatherData.CityName,
			Latitude = owmWeatherData.Coordinates.Lat,
			Longitude = owmWeatherData.Coordinates.Lon,
			TemperatureC = owmWeatherData.Main.Temperature,
			FeelsLikeTemperatureC = owmWeatherData.Main.FeelsLikeTemperature,
			MinTemperatureC = owmWeatherData.Main.MinTemperature,
			MaxTemperatureC = owmWeatherData.Main.MaxTemperature,
			PressureHpa = owmWeatherData.Main.PressureHpa,
			SeaLevelPressureHpa = owmWeatherData.Main.SeaLevelPressureHpa,
			GroundLevelPressureHpa = owmWeatherData.Main.GroundLevelPressureHpa,
			Humidity = owmWeatherData.Main.Humidity,
			Visibility = owmWeatherData.Visibility,
			WindSpeed = owmWeatherData.Wind?.Speed,
			WindDegrees = owmWeatherData.Wind?.Degrees,
			WindGust = owmWeatherData.Wind?.Gust,
			Cloudiness = owmWeatherData.Clouds?.CloudinessPercentage,
			RainLast1hMm = owmWeatherData.Rain?.Last1hMm,
			SnowLast1hMm = owmWeatherData.Snow?.Last1hMm,
			SunriseTime = ConvertTimestampToDateTimeOffset(owmWeatherData.System.SunriseTime, offsetSeconds),
			SunsetTime = ConvertTimestampToDateTimeOffset(owmWeatherData.System.SunsetTime, offsetSeconds),
			MeasuredTime = ConvertTimestampToDateTimeOffset(owmWeatherData.Timestamp, offsetSeconds)
		};
		return weatherDetailsDto;
	}

	private static DateTimeOffset ConvertTimestampToDateTimeOffset(long timestamp, int offsetSeconds) {
		var utcDate = DateTime.SpecifyKind(DateTimeOffset.FromUnixTimeSeconds(timestamp + offsetSeconds).UtcDateTime, DateTimeKind.Unspecified);
		var localTime = new DateTimeOffset(utcDate, TimeSpan.FromSeconds(offsetSeconds));
		return localTime;
	}
}