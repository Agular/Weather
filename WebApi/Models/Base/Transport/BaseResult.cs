using WebApi.Models.Base.Interfaces;

namespace WebApi.Models.Base.Transport;

public class BaseResult : ITransportObject {
	public string? Error { get; set; }
	public bool IsValid { get; set; } = true;
	public static BaseResult Faulty(string error) => new() { IsValid = false, Error = error };
}

public class BaseResult<T> : BaseResult {
	public BaseResult(T result) {
		Result = result;
	}

	public BaseResult() {
	}

	public new static BaseResult<T> Faulty(string error) => new() { IsValid = false, Error = error };

	public BaseResult<T> WithResult(BaseResult result) {
		IsValid = result.IsValid;
		Error ??= result.Error;
		return this;
	}

	public T GetResult() {
		if (Result == null) {
			throw new InvalidOperationException("Result was accessed when it actually null, please always validate using IsValid");
		}

		return Result;
	}

	public T? Result { get; set; }
}