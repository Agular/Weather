namespace WebApi.Framework; 

public static class ListExtensions {
	public static bool IsEmpty<T>(this IList<T>? list) {
		return list == null || list.Count == 0;
	}
}