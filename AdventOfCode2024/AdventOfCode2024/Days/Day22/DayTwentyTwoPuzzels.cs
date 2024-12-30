using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace AdventOfCode2024.Days.Day22;

public class DayTwentyTwoPuzzels
{
	public void HandlePuzzles()
	{
		var input = File.ReadAllLines(ReadInputFile.GetPathToInput(22))
			.Select(_ => int.Parse(_.ToString()))
			.ToList();

		var stopwatch = new Stopwatch();
		stopwatch.Start();
		var partOne = PartOne(input,2000);
		stopwatch.Stop();

		Console.WriteLine($"Day 22 part one: {partOne}, {stopwatch.ElapsedMilliseconds} ms");

		stopwatch.Restart();
		var partTwo = PartTwo(input, 2000);
		stopwatch.Stop();

		Console.WriteLine($"Day 22 part two: {partTwo}, {stopwatch.ElapsedMilliseconds} ms");
	}

	public long PartOne(List<int> initialsSecrets, int secretCount)
	{

		var total = 0L;
		foreach(var initialSecret in initialsSecrets)
		{
			var callculatedSecret = CalculateSecret(initialSecret, secretCount);
			total += callculatedSecret;
		}

		return total;
	}

	public record BananaGroup(
		IEnumerable<long> sequence,
		long bannaValue
	);

	public int PartTwo(List<int> initialsSecrets, int secretCount)
	{
		var brokers = new List<List<(int price,int diffrence)>>();

		foreach (var initialSecret in initialsSecrets)
		{
			var callculatedSecret = CalculateBannaChanges(initialSecret, secretCount);
			
			callculatedSecret.Insert(0, initialSecret);
			
			var getLastNums = callculatedSecret.Select(_ => _ % 10).ToList();

			var allDiffrences = new List<(int, int)>();

			for (var latnum = 0; latnum < getLastNums.Count(); latnum++)
			{
				if (latnum == 0)
				{
					allDiffrences.Add((initialSecret % 10, 0));
					continue;
				}
				var current = getLastNums[latnum];
				var previous = getLastNums[latnum - 1];
				allDiffrences.Add(((int)current, (int)(current - previous)));

			}

			allDiffrences.RemoveAt(0);

			brokers.Add(allDiffrences);
			
		}

		var maxBannans = MaxBannas4(brokers);

		return maxBannans;
	}

	public int MaxBannas4(List<List<(int price, int diffrence)>> brokers)
	{
		var buyersLedgers = new Dictionary<(int, int, int, int), int>();

		var maxBannanas = 0;

		foreach (var broker in brokers)
		{
			var seenSequence = new HashSet<(int, int, int, int)>();
			for (var i = 0; i < broker.Count; i++)
			{
				if (i + 4 > broker.Count)
				{
					break;
				}

				var window = broker.GetRange(i, 4);
				var windowDiffrences = window.Select(_ => _.diffrence).ToArray();
				var key = (windowDiffrences[0], windowDiffrences[1], windowDiffrences[2], windowDiffrences[3]);
				var price = window.Last().price;

				if (seenSequence.Contains(key))
				{
					continue;
				}

				seenSequence.Add(key);

				if (!buyersLedgers.TryAdd(key, price))
				{
					var newPrice = buyersLedgers[key] + price;
					buyersLedgers[key] = newPrice ;
					maxBannanas = Math.Max(maxBannanas, newPrice);
				}
			}
		}

		return maxBannanas;
	}

	public List<long> CalculateBannaChanges(long currentSecret, long secretCount)
	{
		if (secretCount == 0)
		{
			return [currentSecret];
		}

		var firsStep = FirstStep(currentSecret);

		var secondStep = SecondStep(firsStep);

		var thridStep = ThirdStep(secondStep);

		var total = CalculateBannaChanges(thridStep, secretCount - 1);
		total.Insert(0, thridStep);
		return total;
	}

	public long CalculateSecret(long currentSecret, long secretCount)
	{
		if(secretCount == 0)
		{
			return currentSecret;
		}

		var firsStep = FirstStep(currentSecret);

		var secondStep = SecondStep(firsStep);

		var thridStep = ThirdStep(secondStep);

		var total = CalculateSecret(thridStep, secretCount - 1);

		return total;
	}

	private long FirstStep(long secret)
	{
		var multiply = secret * 64;
		var resultWhenMixed = Mix(secret, multiply);
		var resultWhenPruned = Prune(resultWhenMixed);
		return resultWhenPruned;
	}

	private long SecondStep(long secretNumber)
	{
		var multiply = (int)Math.Floor((decimal)(secretNumber / 32));
		var resultWhenMixed = Mix(secretNumber, multiply);
		var pruned = Prune(resultWhenMixed);
		return pruned;
	}

	private long ThirdStep(long secretNumber)
	{
		var multiply = secretNumber * 2048;
		var mixed = Mix(secretNumber, multiply);
		var pruned = Prune(mixed);

		return pruned;
	}

	private long Mix(long currentSecret, long newSecret)
	{
		return currentSecret ^ newSecret;
	}

	private long Prune(long currentSecret)
	{
		return currentSecret % 16777216;
	}
}
class EnumerableComparer<T> : IEqualityComparer<IEnumerable<T>>
{
	public bool Equals(IEnumerable<T>? x, IEnumerable<T>? y)
	{
		if (x == null && y == null) return true;
		if (x == null || y == null) return false;
		return x.SequenceEqual(y);
	}

	public int GetHashCode(IEnumerable<T> obj)
	{
		if (obj == null) throw new ArgumentNullException(nameof(obj));

		// Combine hash codes of elements
		unchecked
		{
			return obj.Aggregate(17, (hash, item) => hash * 23 + item?.GetHashCode() ?? 0);
		}
	}
}
