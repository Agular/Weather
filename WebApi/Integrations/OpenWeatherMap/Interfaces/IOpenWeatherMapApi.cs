using Refit;
using WebApi.Integrations.OpenWeatherMap.Models;

namespace WebApi.Integrations.OpenWeatherMap.Interfaces;

public interface IOpenWeatherMapApi {
	[Get("/data/2.5/weather")]
	Task<OwmWeatherData> GetCurrentWeatherData(OwmWeatherDataRequest request);
}