using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Base;
using WebApi.Models.Weather.Transport;
using WebApi.Services.Weather;

namespace WebApi.Controllers.Weather;

[Route("/api/weather")]
public class WeatherController : BaseController {
	private readonly IWeatherService _weatherService;

	public WeatherController(IWeatherService weatherService) {
		_weatherService = weatherService;
	}

	[HttpGet("location/{locationName}/current")]
	[ProducesResponseType(typeof(WeatherDetailsDto), (int)HttpStatusCode.OK)]
	[ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
	public async Task<IActionResult> GetLocationCurrentWeatherDetails(string locationName) {
		var weatherDetails = await _weatherService.GetLocationCurrentWeatherDetails(locationName);
		if (weatherDetails == null) {
			return NotFound();
		}

		return Ok(weatherDetails);
	}
}