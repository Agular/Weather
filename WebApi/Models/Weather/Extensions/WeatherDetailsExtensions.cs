using WebApi.Models.Weather.Entities;
using WebApi.Models.Weather.Transport;

namespace WebApi.Models.Weather.Extensions; 

public static class WeatherDetailsExtensions {
	public static WeatherDetails ToDocument(this WeatherDetailsDto weatherDetailsDto) {
		var document = new WeatherDetails {
			LocationName = weatherDetailsDto.LocationName,
			Latitude = weatherDetailsDto.Latitude,
			Longitude = weatherDetailsDto.Longitude,
			TemperatureC = weatherDetailsDto.TemperatureC,
			FeelsLikeTemperatureC = weatherDetailsDto.FeelsLikeTemperatureC,
			MinTemperatureC = weatherDetailsDto.MinTemperatureC,
			MaxTemperatureC = weatherDetailsDto.MaxTemperatureC,
			PressureHpa = weatherDetailsDto.PressureHpa,
			SeaLevelPressureHpa = weatherDetailsDto.SeaLevelPressureHpa,
			GroundLevelPressureHpa = weatherDetailsDto.GroundLevelPressureHpa,
			Humidity = weatherDetailsDto.Humidity,
			Visibility = weatherDetailsDto.Visibility,
			WindSpeed = weatherDetailsDto.WindSpeed,
			WindDegrees = weatherDetailsDto.WindDegrees,
			WindGust = weatherDetailsDto.WindGust,
			Cloudiness = weatherDetailsDto.Cloudiness,
			RainLast1hMm = weatherDetailsDto.RainLast1hMm,
			SnowLast1hMm = weatherDetailsDto.SnowLast1hMm,
			SunriseTime = weatherDetailsDto.SunriseTime,
			SunsetTime = weatherDetailsDto.SunsetTime,
			MeasuredTime = weatherDetailsDto.MeasuredTime
		};
		return document;
	}

	public static WeatherDetailsDto ToDto(this WeatherDetails weatherDetails) {
		var dto = new WeatherDetailsDto {
			LocationName = weatherDetails.LocationName,
			Latitude = weatherDetails.Latitude,
			Longitude = weatherDetails.Longitude,
			TemperatureC = weatherDetails.TemperatureC,
			FeelsLikeTemperatureC = weatherDetails.FeelsLikeTemperatureC,
			MinTemperatureC = weatherDetails.MinTemperatureC,
			MaxTemperatureC = weatherDetails.MaxTemperatureC,
			PressureHpa = weatherDetails.PressureHpa,
			SeaLevelPressureHpa = weatherDetails.SeaLevelPressureHpa,
			GroundLevelPressureHpa = weatherDetails.GroundLevelPressureHpa,
			Humidity = weatherDetails.Humidity,
			Visibility = weatherDetails.Visibility,
			WindSpeed = weatherDetails.WindSpeed,
			WindDegrees = weatherDetails.WindDegrees,
			WindGust = weatherDetails.WindGust,
			Cloudiness = weatherDetails.Cloudiness,
			RainLast1hMm = weatherDetails.RainLast1hMm,
			SnowLast1hMm = weatherDetails.SnowLast1hMm,
			SunriseTime = weatherDetails.SunriseTime,
			SunsetTime = weatherDetails.SunsetTime,
			MeasuredTime = weatherDetails.MeasuredTime
		};

		return dto;
	}
}