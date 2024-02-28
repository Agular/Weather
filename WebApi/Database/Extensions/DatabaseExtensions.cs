using WebApi.Database.Repositories;
using WebApi.Database.Seeders.Base;

namespace WebApi.Database.Extensions;

public static class DatabaseExtensions {
	public static async Task SeedDatabase(this WebApplication app) {
		using var scope = app.Services.CreateScope();
		var repository = scope.ServiceProvider.GetRequiredService<ICommonRepository>();
		var seederTypes = typeof(DataSeeder).Assembly.DefinedTypes.Select(t => t.AsType())
			.Where(x => typeof(IDataSeeder).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract);

		foreach (var seederType in seederTypes) {
			var seeder = (DataSeeder)Activator.CreateInstance(seederType, repository);
			await seeder.Seed();
		}
	}
}