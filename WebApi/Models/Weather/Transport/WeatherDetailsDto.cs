using WebApi.Models.Base.Interfaces;

namespace WebApi.Models.Weather.Transport;

public class WeatherDetailsDto : ITransportObject {
	public string Id { get; set; }
	public string LocationName { get; set; }
	public double Latitude { get; set; }
	public double Longitude { get; set; }
	public double TemperatureC { get; set; }
	public double FeelsLikeTemperatureC { get; set; }
	public double MinTemperatureC { get; set; }
	public double MaxTemperatureC { get; set; }
	public double PressureHpa { get; set; }
	public double? SeaLevelPressureHpa { get; set; }
	public double? GroundLevelPressureHpa { get; set; }
	public double Humidity { get; set; }
	public double Visibility { get; set; }
	public double? WindSpeed { get; set; }
	public double? WindDegrees { get; set; }
	public double? WindGust { get; set; }
	public double? Cloudiness { get; set; }
	public double? RainLast1hMm { get; set; }
	public double? SnowLast1hMm { get; set; }
	public DateTimeOffset SunriseTime { get; set; }
	public DateTimeOffset SunsetTime { get; set; }
	public DateTimeOffset MeasuredTime { get; set; }
}