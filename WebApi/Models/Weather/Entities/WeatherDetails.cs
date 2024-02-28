using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using WebApi.Models.Base.Attributes;
using WebApi.Models.Base.Constants;
using WebApi.Models.Base.Entities;

namespace WebApi.Models.Weather.Entities;

[BsonCollection(DocumentCollections.WeatherDetails)]
public class WeatherDetails : Document {
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

	[BsonRepresentation(BsonType.String)]
	public DateTimeOffset SunriseTime { get; set; }

	[BsonRepresentation(BsonType.String)]
	public DateTimeOffset SunsetTime { get; set; }

	[BsonRepresentation(BsonType.String)]
	public DateTimeOffset MeasuredTime { get; set; }
}