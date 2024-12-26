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

		Console.WriteLine($"Day 22 part one: {partOne} time ms: {stopwatch.ElapsedMilliseconds}");

		stopwatch.Restart();
		var partTwo = PartTwo(input, 2000);
		stopwatch.Stop();

		Console.WriteLine($"Day 22 part two: {partTwo} time ms: {stopwatch.ElapsedMilliseconds}");
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

			//// remove first because there is no price change
			allDiffrences.RemoveAt(0);

			brokers.Add(allDiffrences);
			
		}

		var containsSequence = new List<IEnumerable<long>>();

		var diffrentBuyingOptions = new List<List<BananaGroup>>();

		var splitUpBrokers =  new List<List<List<long>>>();

		//var maxBannans = MaxBannas2(brokers);
		var maxBannans = MaxBannas3(brokers);
		var x = 0;
		//var maxBannanas = MaxBannas(brokers);

		return maxBannans;
	}

	public int MaxBannas3(List<List<(int price, int diffrence)>> brokers)
	{
		var buyersLedgers = new Dictionary<int, Dictionary<(int, int, int, int), int>>();

		var brokerId = 0;


		foreach (var broker in brokers)
		{
			buyersLedgers.Add(brokerId, []);

			for (var i = 0; i < broker.Count; i++)
			{
				if (i + 4 > broker.Count)
				{
					break;
				}

				var window = broker.GetRange(i, 4);
				var windowDiffrences = window.Select(_ => _.diffrence).ToArray();
				var key = (windowDiffrences[0], windowDiffrences[1], windowDiffrences[2], windowDiffrences[3]);
				var dict = buyersLedgers[brokerId];
				dict.TryAdd(key, window.Last().price);
			}
			brokerId++;
		}

		var buyersLedgerss = buyersLedgers
			.SelectMany(_ => _.Value)
			.GroupBy(_ => _.Key)
			.Select(
				_ => new {
					_.Key,
					Value = _.Sum(s => s.Value)
				}
			).MaxBy(_ => _.Value);

		return buyersLedgerss.Value;
	}

	public int MaxBannas2(List<List<(int price, int diffrence)>> brokers)
	{
		var buyersLedgers = new Dictionary<int, Dictionary<(int, int, int, int), int>>();

		var brokerId = 0;


		foreach (var broker in brokers) 
		{
			buyersLedgers.Add(brokerId, []);

			for (var i = 0; i < broker.Count; i++)
			{
				if (i + 4 > broker.Count)
				{
					break;
				}

				var window = broker.GetRange(i, 4);
				var windowDiffrences = window.Select(_ => _.diffrence).ToArray();
				var key = (windowDiffrences[0], windowDiffrences[1], windowDiffrences[2], windowDiffrences[3]);
				var dict = buyersLedgers[brokerId];
				dict.TryAdd(key, window.Last().price);
			}
			brokerId++;
		}


		var MaxBannanas = 0;

		var checkedKeys = new HashSet<(long, long, long, long)>();


		foreach (var buyer in buyersLedgers)
		{
	
			var values = buyer.Value.Keys;
			

			foreach(var sequenceToCheck in values) {
				var totalBannas = buyer.Value[sequenceToCheck];

				if (checkedKeys.Contains(sequenceToCheck))
				{
					continue;
				}

				checkedKeys.Add(sequenceToCheck);

				foreach (var otherbuyer in buyersLedgers)
				{

					if (buyer.Key == otherbuyer.Key)
					{
						continue;
					}

					if (otherbuyer.Value.TryGetValue(sequenceToCheck, out var val))
					{
						totalBannas += val;
					}
				}
				MaxBannanas = Math.Max(MaxBannanas, totalBannas);
			}	
		}



		return MaxBannanas;
	}


	public long MaxBannas(List<List<(long price, long diffrence)>> brokers)
	{
		var maxBannans = 0L;
		var checkedWindows = new HashSet<IEnumerable<long>>();

		var brokerCount = 0;

		foreach (var broker in brokers)
		{
			Console.WriteLine($"BrokerCounter: {brokerCount}");
			brokerCount++;

			for (var i = 0; i < broker.Count; i++)
			{
				var possibleMaxBannans = 0L;
				if (i + 4 > broker.Count)
				{
					break;
				}
				var window = broker.GetRange(i,4);
				var windowDiffrences = window.Select(_ => _.diffrence);

				
				if(checkedWindows.FirstOrDefault(_ => _.SequenceEqual(windowDiffrences)) != null)
				{
					continue;
				}

				checkedWindows.Add(windowDiffrences);


				possibleMaxBannans += window.Last().price;


				foreach (var otherBroker in brokers)
				{
					if (otherBroker.SequenceEqual(broker))
					{
						continue;
					}

					for (var j = 0; j < otherBroker.Count; j++)
					{
						if (j + 4 > broker.Count)
						{
							break;
						}
						var otherWindow = otherBroker.GetRange(j, 4);
						var otherwindowDiffrences = otherWindow.Select(_ => _.diffrence);

						if (otherwindowDiffrences.SequenceEqual(windowDiffrences))
						{
							possibleMaxBannans += otherWindow.Last().price;
							break;
						}

					}
					maxBannans = Math.Max(maxBannans, possibleMaxBannans);
				}
			}
		}
		return maxBannans;
	}


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
