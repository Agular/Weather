using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Controllers.Base;
using WebApi.Models.Locations.Transport;
using WebApi.Services.Locations;

namespace WebApi.Controllers.Locations;

[Route("/api/locations")]
public class LocationController : BaseController {
	private readonly ILocationService _locationService;

	public LocationController(ILocationService locationService) {
		_locationService = locationService;
	}

	[HttpGet]
	[ProducesResponseType(typeof(List<LocationDto>), (int)HttpStatusCode.OK)]
	public async Task<IActionResult> GetLocations() {
		var locations = await _locationService.GetLocations();
		return Ok(locations);
	}
}