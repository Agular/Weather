using MongoDB.Driver;
using WebApi.Database.Repositories;
using WebApi.Models.Locations.Entities;
using WebApi.Models.Locations.Extensions;
using WebApi.Models.Locations.Transport;
using WebApi.Services.Base;

namespace WebApi.Services.Locations;

public interface ILocationService {
	Task<List<LocationDto>> GetLocations();
	Task<LocationDto?> GetLocationByName(string name);
	Task<List<LocationDto>> GetLocationsByNames(List<string> names);
}

public class LocationService : BaseService<LocationService>, ILocationService {
	public LocationService(ICommonRepository commonRepository, ILogger<LocationService> logger) : base(commonRepository, logger) {
	}

	public async Task<List<LocationDto>> GetLocations() {
		var filter = Builders<Location>.Filter.Empty;
		var findOptions = new FindOptions<Location> {
			Sort = Builders<Location>.Sort.Ascending(r => r.Name)
		};

		var dbLocations = await CommonRepository.GetMany(filter, findOptions);
		var dtos = dbLocations.Select(r => r.ToDto()).ToList();
		return dtos;
	}

	public async Task<LocationDto?> GetLocationByName(string name) {
		var filter = Builders<Location>.Filter.Eq(r=> r.Name, name);
		var dbLocation = await CommonRepository.Get(filter);
		var location = dbLocation?.ToDto();
		return location;
	}

	public async Task<List<LocationDto>> GetLocationsByNames(List<string> names) {
		var filter = Builders<Location>.Filter.In(r => r.Name, names);
		var dbLocations = await CommonRepository.GetMany(filter);
		var locations = dbLocations.Select(r => r.ToDto()).ToList();
		return locations;
	}
}