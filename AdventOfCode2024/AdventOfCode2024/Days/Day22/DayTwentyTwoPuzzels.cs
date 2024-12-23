﻿using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;
using System.Diagnostics;
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

		Console.WriteLine($"Day 22 part one: {partOne} time ms: {stopwatch.ElapsedMilliseconds}");

		stopwatch.Restart();
		var partTwo = PartTwo(input, 2000);
		stopwatch.Stop();

		Console.WriteLine($"Day 22 part one: {partTwo} time ms: {stopwatch.ElapsedMilliseconds}");
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

	public long PartTwo(List<int> initialsSecrets, int secretCount)
	{
		var brokers = new List<List<(long,long)>>();

		var r = new List<long>();

		foreach (var initialSecret in initialsSecrets)
		{
			var callculatedSecret = CalculateBannaChanges(initialSecret, secretCount);
			var last = callculatedSecret.Last();
			callculatedSecret.Insert(0, initialSecret);
			var getLastNums = callculatedSecret.Select(_ => _ % 10).ToList();

			var allDiffrences = new List<(long, long)>();

			for (var latnum = 0; latnum < getLastNums.Count(); latnum++)
			{
				if (latnum == 0)
				{
					allDiffrences.Add((initialSecret % 10, 0));
					continue;
				}
				var current = getLastNums[latnum];
				var previous = getLastNums[latnum - 1];
				allDiffrences.Add((current, (current - previous)));

			}
			brokers.Add(allDiffrences);
		}


		var containsSequence = new List<IEnumerable<long>>();

		var diffrentBuyingOptions = new List<List<BananaGroup>>();

		var splitUpBrokers =  new List<List<List<long>>>();
		var brokerCounter = 0;
		foreach (var broker in brokers)
		{
			brokerCounter++;	
			var brokerlist = new List<BananaGroup>();
			containsSequence = [];
			for (var i = 4; i < broker.Count; i++)
			{
				var window = 4;
				var brokerSequesnce = broker
					.GetRange(i - window, window);
				var diffrences = brokerSequesnce.Select(_ => _.Item2);
				var value = brokerSequesnce.Last().Item1;

				if (containsSequence.FirstOrDefault(_ => _.SequenceEqual(diffrences)) != null) 
				{
					continue;
				}
				containsSequence.Add(diffrences.ToList());
				var llfsl = new BananaGroup(diffrences, value);
				brokerlist.Add(llfsl);

			}
			diffrentBuyingOptions.Add(brokerlist);
		}

		var maxAmountOfbannas = diffrentBuyingOptions
			.Select(_ => _)
			.SelectMany(_ => _)
			.GroupBy(
				s => s.sequence,
				new EnumerableComparer<long>()
				).Select(_ =>
			{
				var kk = _.Sum(_ => _.bannaValue);
				return new { totalBannas = kk, sequence = _.Key };
			})
			.MaxBy(_ => _.totalBannas);


		return maxAmountOfbannas.totalBannas;
	}

	// Custom EqualityComparer for IEnumerable<T>

	public static long GetNumberAfterSequence(List<long> sequence, List<(long, long)> largerList)
	{
		if (sequence == null || largerList == null || sequence.Count == 0 || largerList.Count == 0)
			throw new ArgumentException("Lists cannot be null or empty");

		var allPossible = new List<long>();

		// Iterate through the larger list to find the sequence
		for (int i = 0; i <= largerList.Count - sequence.Count; i++)
		{
			// Check if the current subsequence matches
			if (largerList.Skip(i).Take(sequence.Count).Select(_ => _.Item2).SequenceEqual(sequence))
			{
				// Check if there's an element after the subsequence
				int nextIndex = i + sequence.Count - 1;
				if (nextIndex < largerList.Count)
				{
					allPossible.Add(largerList[nextIndex].Item1);

					//return largerList[nextIndex].Item1; // Return the number after the subsequence
				}
			}
		}

		if(allPossible.Count == 0)
		{
			return 0;
		}

		return allPossible.Max();
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
