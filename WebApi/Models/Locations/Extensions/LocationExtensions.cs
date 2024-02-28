using WebApi.Models.Locations.Entities;
using WebApi.Models.Locations.Transport;

namespace WebApi.Models.Locations.Extensions;

public static class LocationExtensions {
	public static LocationDto ToDto(this Location location) {
		var dto = new LocationDto {
			Name = location.Name,
			State = location.State,
			Country = location.Country,
			Latitude = location.Latitude,
			Longitude = location.Longitude
		};

		return dto;
	}

	public static Location ToLocation(this JsonLocation jsonLocation) {
		var location = new Location {
			Name = jsonLocation.Name,
			State = jsonLocation.State,
			Country = jsonLocation.Country,
			Latitude = jsonLocation.Coord.Lat,
			Longitude = jsonLocation.Coord.Lon
		};

		return location;
	}
}