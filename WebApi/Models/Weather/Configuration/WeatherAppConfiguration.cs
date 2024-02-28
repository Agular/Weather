namespace WebApi.Models.Weather.Configuration; 

public class WeatherAppConfiguration {
	public const string SectionKey = "Weather";
	public string DefaultLocations { get; set; }
}