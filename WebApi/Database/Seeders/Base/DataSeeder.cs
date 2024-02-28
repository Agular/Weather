using WebApi.Database.Repositories;

namespace WebApi.Database.Seeders.Base;

public interface IDataSeeder {
	Task Seed();
}

public abstract class DataSeeder : IDataSeeder {
	protected readonly ICommonRepository Repository;

	protected DataSeeder(ICommonRepository repository) {
		Repository = repository;
	}

	public abstract Task Seed();
}