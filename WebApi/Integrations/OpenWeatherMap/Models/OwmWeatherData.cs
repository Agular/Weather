using System.Text.Json.Serialization;

namespace WebApi.Integrations.OpenWeatherMap.Models;

public class OwmWeatherData {
	[JsonPropertyName("id")]
	public int CityId { get; set; }

	[JsonPropertyName("name")]
	public string CityName { get; set; }

	[JsonPropertyName("coord")]
	public OwmWeatherDataCoordinates Coordinates { get; set; }

	public OwmWeatherMainData Main { get; set; }
	public int Visibility { get; set; }
	public OwmWeatherWindData? Wind { get; set; }
	public OwmWeatherCloudData? Clouds { get; set; }
	public OwmWeatherRainData? Rain { get; set; }
	public OwmWeatherSnowData? Snow { get; set; }

	[JsonPropertyName("sys")]
	public OwmWeatherSystemData System { get; set; }

	[JsonPropertyName("dt")]
	public long Timestamp { get; set; }

	[JsonPropertyName("timezone")]
	public int TimeZoneSeconds { get; set; }
}

public class OwmWeatherDataCoordinates {
	public double Lon { get; set; }
	public double Lat { get; set; }
}

public class OwmWeatherMainData {
	[JsonPropertyName("temp")]
	public double Temperature { get; set; }

	[JsonPropertyName("feels_like")]
	public double FeelsLikeTemperature { get; set; }

	[JsonPropertyName("temp_min")]
	public double MinTemperature { get; set; }

	[JsonPropertyName("temp_max")]
	public double MaxTemperature { get; set; }

	[JsonPropertyName("pressure")]
	public int PressureHpa { get; set; }

	[JsonPropertyName("sea_level")]
	public int? SeaLevelPressureHpa { get; set; }

	[JsonPropertyName("grnd_level")]
	public int? GroundLevelPressureHpa { get; set; }

	public double Humidity { get; set; }
}

public class OwmWeatherWindData {
	public double Speed { get; set; }

	[JsonPropertyName("deg")]
	public int Degrees { get; set; }

	public double? Gust { get; set; }
}

public class OwmWeatherCloudData {
	[JsonPropertyName("all")]
	public int CloudinessPercentage { get; set; }
}

public class OwmWeatherRainData {
	[JsonPropertyName("1h")]
	public double Last1hMm { get; set; }
}

public class OwmWeatherSnowData {
	[JsonPropertyName("1h")]
	public double Last1hMm { get; set; }
}

public class OwmWeatherSystemData {
	[JsonPropertyName("sunrise")]
	public long SunriseTime { get; set; }

	[JsonPropertyName("sunset")]
	public long SunsetTime { get; set; }
}