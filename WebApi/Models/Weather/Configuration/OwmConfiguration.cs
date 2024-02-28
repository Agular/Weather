namespace WebApi.Models.Weather.Configuration; 

public class OwmConfiguration {
	public const string SectionKey = "OpenWeatherMap";
	public string ApiKey { get; set; }
	public string Endpoint { get; set; }
}