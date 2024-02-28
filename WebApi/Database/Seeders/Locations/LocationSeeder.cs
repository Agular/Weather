using System.Text.Json;
using MongoDB.Driver;
using WebApi.Database.Repositories;
using WebApi.Database.Seeders.Base;
using WebApi.Framework;
using WebApi.Models.Locations.Entities;
using WebApi.Models.Locations.Extensions;

namespace WebApi.Database.Seeders.Locations;

public class LocationSeeder : DataSeeder {
	public LocationSeeder(ICommonRepository repository) : base(repository) {
	}

	public override async Task Seed() {
		var filter = Builders<Location>.Filter.Empty;
		var existingLocation = await Repository.Get(filter);
		if (existingLocation != null) {
			return;
		}

		try {
			var path = Path.Combine(Directory.GetCurrentDirectory(), "city.list.json");
			var jsonText = await File.ReadAllTextAsync(path);

			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			var jsonLocations = JsonSerializer.Deserialize<List<JsonLocation>>(jsonText, options);
			if (jsonLocations.IsEmpty()) {
				return;
			}

			var dbLocations = jsonLocations.Select(r => r.ToLocation()).ToList();
			await Repository.AddMany(dbLocations);
		} catch (Exception ex) {
			return;
		}
	}
}