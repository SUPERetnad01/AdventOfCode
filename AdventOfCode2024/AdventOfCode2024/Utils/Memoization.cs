using System.Collections.Concurrent;

namespace AdventOfCode2024.Utils;

public static class Memoization
{
	public static Func<T, TResult> Memoize<T, TResult>(this Func<T, TResult> f)
	{
		var cache = new ConcurrentDictionary<T, TResult>();
		return a => cache.GetOrAdd(a, f);
	}
}
