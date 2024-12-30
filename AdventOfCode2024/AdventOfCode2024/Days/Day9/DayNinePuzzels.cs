using AdventOfCode2024.Utils;
using System.Diagnostics;

namespace AdventOfCode2024.Days.Day9;

public static class DayNinePuzzels
{
	public static void HandlePuzzels()
	{
		var input = File.ReadAllText(ReadInputFile.GetPathToInput(9));

		var stopwatch = new Stopwatch();
		stopwatch.Start();
		var resultPartOne = PartOne(input);
		Console.WriteLine($"Day 9 part one: {resultPartOne}, {stopwatch.ElapsedMilliseconds} ms");
		stopwatch.Stop();

		stopwatch.Restart();
		var resultPartTwo = PartTwo(input);
		stopwatch.Stop();

		Console.WriteLine($"Day 9 part two: {resultPartTwo}, {stopwatch.ElapsedMilliseconds} ms");
	}

	public static long PartOne(string input)
	{

		var resultList = new List<string>();
		var currentId = 0;
		var numbersList = input.Select(_ => int.Parse(_.ToString())).ToList();

		for (int numberIndex = 0; numberIndex < numbersList.Count(); numberIndex++)
		{
			var numberToCheck = numbersList[numberIndex];

			if (numberIndex % 2 != 0)
			{
				for (var _ = 0; _ < numberToCheck; _++)
				{
					resultList.Add(".");
				}

				continue;
			}


			for (var _ = 0; _ < numberToCheck; _++)
			{
				resultList.Add(currentId.ToString());
			}

			currentId++;
		}

		while (resultList.Contains("."))
		{
			resultList[resultList.IndexOf(".")] = resultList.Last();
			resultList.RemoveAt(resultList.Count - 1);
		}

		long result = 0;

		for (int i = 0; i < resultList.Count; i++)
		{
			result += long.Parse(resultList[i]) * i;
		}

		return result;
	}

	public static long PartTwo(string input)
	{
		var operatingSystem = new List<(int fileId, List<string> file)>();
		var currentId = 0;
		var numbersList = input.Select(_ => int.Parse(_.ToString())).ToList();

		// create the input
		for (int numberIndex = 0; numberIndex < numbersList.Count(); numberIndex++)
		{
			var numberToCheck = numbersList[numberIndex];

			if (numberIndex % 2 != 0)
			{
				if (numberToCheck == 0)
				{
					continue;
				}

				var freeSpace = Enumerable.Repeat(".", numberToCheck).ToList();
				operatingSystem.Add((numberIndex, freeSpace));

				continue;
			}


			var file = Enumerable.Repeat($"{currentId}", numberToCheck).ToList();
			operatingSystem.Add((numberIndex, file));

			currentId++;
		}


		var passedFileIds = new List<int>();

		// format the disk
		for (int i = 0; i < operatingSystem.Count; i++)
		{
			var lastIndex = operatingSystem.Count - 1 - i;
			var lastChunk = operatingSystem[lastIndex];

			var currentFile = lastChunk.file;
			var currentFileId = lastChunk.fileId;

			if (currentFileId == 0 || currentFile.Count == 0)
			{
				break;
			}

			if (currentFile.Contains(".") || passedFileIds.Contains(currentFileId))
			{
				continue;
			}

			var firstFreeSpace = operatingSystem.FirstOrDefault(_ => _.file.Contains(".") && _.file.Count >= currentFile.Count);

			if (firstFreeSpace.file == null || firstFreeSpace.fileId == null)
			{
				continue;
			}

			var firstFreeSpaceIndex = operatingSystem.IndexOf(firstFreeSpace);

            if (firstFreeSpaceIndex > lastIndex)
            {
				continue;
            }

            var resultingFreeSpace = firstFreeSpace.file.GetRange(0, firstFreeSpace.file.Count - currentFile.Count);

			var nextSpace = operatingSystem[firstFreeSpaceIndex + 1];

			if (nextSpace.file.Contains("."))
			{

				nextSpace.file.Concat(resultingFreeSpace);

			}
			else if (resultingFreeSpace.Count != 0)
			{
				operatingSystem.RemoveAt(firstFreeSpaceIndex);
				operatingSystem.Insert(firstFreeSpaceIndex, (-1, resultingFreeSpace));
				operatingSystem.Insert(firstFreeSpaceIndex, lastChunk);
			}
			else if (resultingFreeSpace.Count == 0)
			{
				operatingSystem.RemoveAt(firstFreeSpaceIndex);
				operatingSystem.Insert(firstFreeSpaceIndex, lastChunk);
			}

			// use old index because index is changing
			var oldLastIndex = operatingSystem.Count - 1 - i;

			// replace old place with dots 
			operatingSystem.RemoveAt(oldLastIndex);
			operatingSystem.Insert(oldLastIndex, (-1, Enumerable.Repeat(".", currentFile.Count).ToList()));
		}

		var resultOperatingSystem = operatingSystem
			.SelectMany(_ => _.file)
			.Select(_ =>
			{
				if (_ == ".") {
					return 0;
				}
				return int.Parse(_);
			}).ToList();

		long total = 0;

        for (int i = 0; i < resultOperatingSystem.Count(); i++)
        {
			total += resultOperatingSystem[i] * i;
        }


        return total;
	}
}

