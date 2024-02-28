using Refit;

namespace WebApi.Integrations.OpenWeatherMap.Models;

public class OwmWeatherDataRequest
{
    [AliasAs("lat")]
    public double Latitude { get; set; }

    [AliasAs("lon")]
    public double Longitude { get; set; }

    [AliasAs("units")]
    public string Units { get; set; }
}