namespace WebApi.Framework;

public static class StringExtensions {
	public static bool IsNullOrEmpty(this string s) {
		return string.IsNullOrEmpty(s);
	}

	public static List<string> SplitConfig(this string str) {
		return str.IsNullOrEmpty()
			? new List<string>()
			: str.Split(';', ',').ToList();
	}
}