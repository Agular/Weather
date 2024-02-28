using Refit;
using WebApi.Integrations.Base.Interfaces;

namespace WebApi.Integrations.Base.Services;

public class BaseIntegrationService<T> : IIntegrationService {
	public ILogger<T> Logger { get; set; }

	public BaseIntegrationService(ILogger<T> logger) {
		Logger = logger;
	}

	public async Task LogException(ApiException apiException) {
		var errors = await apiException.GetContentAsAsync<Dictionary<string, string>>() ?? new Dictionary<string, string>();
		var errorMessage = string.Join("; ", errors.Values);
		Logger.LogError(apiException, "{IntegrationService}: Method call failed, errorMessage={ErrorMessage}", nameof(T), errorMessage);
	}
}