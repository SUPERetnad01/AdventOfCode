using System.Diagnostics;

namespace AdventOfCode2024.Days.Day11;

public class DayElevenPuzzles
{
	public void HandlePuzzles()
	{
		var input = new List<long>() { 554735, 45401, 8434, 0, 188, 7487525, 77, 7 };
		var stopwatch = new Stopwatch();

		stopwatch.Start();
		var result = PartOne(input, 25);
		stopwatch.Stop();
		//Console.WriteLine($"Day 11 part one: {result} time ms: {stopwatch.ElapsedMilliseconds}");

		stopwatch.Start();
		var partTwo = PartOne(input, 75);
		stopwatch.Stop();
		Console.WriteLine($"Day 11 part two: {partTwo} time ms: {stopwatch.ElapsedMilliseconds}");
	}

	public static int PartOne(List<long> input, int amountOfTimesToBlink)
	{
		var memoizedDict = new Dictionary<long, List<long>>();
		var resultList = input;

		for (int blinkCount = 0; blinkCount < amountOfTimesToBlink; blinkCount++)
		{
			var newList = new List<long>();
			for (var i = 0; i < resultList.Count; i++)
			{
				var stone = resultList[i];

				if (memoizedDict.TryGetValue(stone, out var val))
				{
					foreach (var item in val)
					{
						newList.Add(item);
					}
					continue;
				}

				if (stone == 0)
				{
					newList.Add(1);
					continue;
				}

				var amountOfDigits = Math.Floor(Math.Log10(stone) + 1);
				if (amountOfDigits % 2 == 0)
				{
					var splitThingy = Math.Pow(10, amountOfDigits / 2);

					var stone1 = (long)(stone / splitThingy);
					var stone2 = (long)(stone % splitThingy);

					newList.Add(stone1);
					newList.Add(stone2);
					memoizedDict.Add(stone, [stone1, stone2]);
					continue;
				}

				var multipliedStone = stone * 2024;
				newList.Add(multipliedStone);
				memoizedDict.Add(stone, [multipliedStone]);
			}

			resultList = newList;
		}



		return resultList.Count;
	}

	public long PartTwo(List<long> pebbles, long timesToBlink)
	{
		long result = 0;
		foreach (var pebble in pebbles)
		{
			result += PebbleFight(pebble, timesToBlink);
		}

		return result;
	}

	private Dictionary<(long blinkCount, long pebble), long> Memoization { get; set; } = [];


	public long PebbleFight(long pebble, long blinkCount)
	{
		if (blinkCount == 0)
		{
			return 1;
		}

		if (Memoization.TryGetValue((blinkCount, pebble), out var val))
		{
			return val;
		}


		if (pebble == 0)
		{
			var result = PebbleFight(pebble, blinkCount - 1);
			Memoization.Add((blinkCount, pebble), result);
			return result;
		}

		var amountOfDigits = Math.Floor(Math.Log10(pebble) + 1);
		if (amountOfDigits % 2 == 0)
		{
			var splitThingy = Math.Pow(10, amountOfDigits / 2);

			var stone1 = (long)(pebble / splitThingy);
			var stone2 = (long)(pebble % splitThingy);

			var result1 = PebbleFight(stone1, blinkCount - 1);
			var result2 = PebbleFight(stone2, blinkCount - 1);

			var splitStoneResult = result1 + result2;
			Memoization.Add((blinkCount, pebble), splitStoneResult);
			return splitStoneResult;
		}


		var resultMultiply = PebbleFight(pebble * 2024, blinkCount - 1);
		Memoization.Add((blinkCount, pebble), resultMultiply);
		return resultMultiply;
	}


}