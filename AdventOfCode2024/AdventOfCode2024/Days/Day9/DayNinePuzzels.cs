using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Days.Day9;

public static class DayNinePuzzels
{
	public static void HandlePuzzels()
	{
		var input = File.ReadAllText(ReadInputFile.GetPathToInput(9));

		//var resultPartOne = PartOne(input);
		var resultPartTwo = PartTwo(input);

		//Console.WriteLine($"partOne: {resultPartOne}");
		Console.WriteLine($"partOne: {resultPartTwo}");
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

		for (int i = 0; i < operatingSystem.Count; i++)
		{
			var lastIndex = operatingSystem.Count - 1 - i;
			var lastChunk = operatingSystem[lastIndex];

			var currentFile = lastChunk.file;
			var currentFileId = lastChunk.fileId;

			if (currentFile.Contains(".") || passedFileIds.Contains(currentFileId))
			{
				continue;
			}

			var firstFreeSpace = operatingSystem.FirstOrDefault(_ => _.file.Contains(".") && _.file.Count >= currentFile.Count);

			if (firstFreeSpace.file == null || firstFreeSpace.fileId == null)
			{
				continue;
			}

			var resultingFreeSpace = firstFreeSpace.file.GetRange(0, firstFreeSpace.file.Count - currentFile.Count);


		}

		var x = 0;


		return 1;
	}
}

