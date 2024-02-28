using WebApi.Models.Base.Transport;
using WebApi.Models.Locations.Transport;
using WebApi.Models.Weather.Transport;

namespace WebApi.Integrations.OpenWeatherMap.Interfaces;

public interface IExternalWeatherService {
	Task<BaseResult<WeatherDetailsDto>> GetCurrentWeatherDetails(LocationDto locationDto);
	Task<BaseResult<WeatherDetailsDto>> GetCurrentWeatherDetails(double latitude, double longitude);
}