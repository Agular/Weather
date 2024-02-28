using WebApi.Database.Repositories;

namespace WebApi.Services.Base;

public interface IService {
}

public class BaseService<T> : IService {
	public ICommonRepository CommonRepository { get; set; }
	public ILogger<T> Logger { get; set; }

	public BaseService(ICommonRepository commonRepository, ILogger<T> logger) {
		CommonRepository = commonRepository;
		Logger = logger;
	}
}