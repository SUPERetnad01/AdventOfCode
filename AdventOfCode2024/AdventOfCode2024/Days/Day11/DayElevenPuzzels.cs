using System.Diagnostics;

namespace AdventOfCode2024.Days.Day11;

public static class DayElevenPuzzels
{
	public static void HanldePuzzle()
	{
		var input = new List<int>() { 554735, 45401, 8434, 0, 188, 7487525, 77, 7 };
		var stopwatch = new Stopwatch();

		stopwatch.Start();
		var result = PartOne(input, 25);
		stopwatch.Stop();
		Console.WriteLine($"Day 11 part one: {result} time ms: {stopwatch.ElapsedMilliseconds}");

		stopwatch.Start();
		var partTwo = PartOne(input, 75);
		stopwatch.Stop();
		Console.WriteLine($"Day 11 part one: {partTwo} time ms: {stopwatch.ElapsedMilliseconds}");
	}	

	public static int PartOne(List<int> input,int amountOfTimesToBlink)
	{
		var momoized = new Dictionary<int,List<int>>();
		var resultList = input;

        for (int _ = 0; _ < amountOfTimesToBlink; _++)
        {
			var newList = new List<int>();
            for (var i = 0; i < resultList.Count; i++)
            {
                var stone = resultList[i];

				if(momoized.TryGetValue(stone,out var val))
				{
					foreach(var item in val)
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
					var splitThingy = Math.Pow(10,amountOfDigits / 2);

					var stone1 = (int)(stone / splitThingy);
					var stone2 = (int)(stone % splitThingy);
					
					newList.Add(stone1);
					newList.Add(stone2);
					momoized.Add(stone, [stone1, stone2]);
					continue;
				}

				var multpiedstone = stone * 2024;
				newList.Add(multpiedstone);
				momoized.Add(stone, [multpiedstone]);
			}

			resultList = newList;
		}

        

		return resultList.Count;
	}

	public static List<int> RecursivePebbleFight(List<int> pebbles, int depth)
	{ 

	}

}
