using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Days.Day9;

public static class DayNinePuzzels
{
	public static void HandlePuzzels() {
		var input = File.ReadAllText(ReadInputFile.GetPathToInput(9));

		//var resultPartOne = PartOne(input);
		var resultPartTwo = PartTwo(input);

		//Console.WriteLine($"partOne: {resultPartOne}");
		Console.WriteLine($"partOne: {resultPartTwo}");
	}

	public static long PartOne(string input) {

		var resultList = new List<string>();
		var currentId = 0;
		var numbersList = input.Select(_ => int.Parse(_.ToString())).ToList();

        for (int numberindex = 0; numberindex < numbersList.Count(); numberindex++)
        {
			var numberToCheck = numbersList[numberindex];

			if(numberindex % 2 != 0)
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

		var resultList = new List<List<string>>();
		var currentId = 0;
		var numbersList = input.Select(_ => int.Parse(_.ToString())).ToList();

		for (int numberindex = 0; numberindex < numbersList.Count(); numberindex++)
		{
			var numberToCheck = numbersList[numberindex];

			if (numberindex % 2 != 0)
			{
				var dotIndex = new List<string>(); 
				for (var _ = 0; _ < numberToCheck; _++)
				{
					dotIndex.Add(".");
				}

				resultList.Add(dotIndex);


				continue;
			}

			var tempList = new List<string>();
			for (var _ = 0; _ < numberToCheck; _++)
			{
				tempList.Add(currentId.ToString());
			}

			resultList.Add(tempList);

			currentId++;
		}

		List<List<string>> x = [];

		for (int i = 0; i < resultList.Count; i++)
		{
			var elementIndex = resultList.Count - 1 - i;
			var lastElement = resultList[elementIndex];

			if (lastElement.Contains(".") || x.Contains(lastElement))
			{
				continue;
			}

			var emptySpace = resultList.FirstOrDefault(_ => _.Contains(".") && _.Count >= lastElement.Count);
			var indexOfEmptySpace = resultList.IndexOf(emptySpace);

			if (emptySpace == null || elementIndex < indexOfEmptySpace)
			{
				continue;
			}

			if (lastElement.Count <= emptySpace.Count && !lastElement.Contains("."))
			{
				var newStringToInsert = lastElement;
				var savedCount = resultList.Count;

				resultList.RemoveAt(savedCount - 1 - i);
				resultList.Insert(savedCount - 1 - i, Enumerable.Repeat(".", lastElement.Count).ToList());

				resultList.RemoveAt(indexOfEmptySpace);
				resultList.Insert(indexOfEmptySpace, newStringToInsert);

				if (emptySpace.Count - lastElement.Count > 0)
				{
					var newFreeSpace = emptySpace
						.Where(_ => _ == ".")
						.ToList();

					resultList
						.Insert(indexOfEmptySpace + 1, newFreeSpace);
				}
			}
		}

		var newResult = new List<string>();
		for (int i = 0; i < resultList.Count; i++)
		{
			foreach (var character in resultList[i])
			{
				newResult.Add(character.ToString());
			}
		}

		long result = 0;

		for (int i = 0; i < newResult.Count; i++)
		{
			if (!newResult[i].Contains("."))
			{
				result += long.Parse(newResult[i]) * i;
			}

		}


		return result;
	}

	static List<string> MergeDots(List<string> input)
	{
		List<string> merged = new List<string>();
		string dotGroup = null;

		foreach (var item in input)
		{
			if (item.Trim('.').Length == 0) // Check if the string contains only dots
			{
				dotGroup = dotGroup == null ? item : dotGroup + item;
			}
			else
			{
				if (dotGroup != null)
				{
					merged.Add(dotGroup);
					dotGroup = null;
				}
				merged.Add(item);
			}
		}

		// Add remaining dots if the list ends with dots
		if (dotGroup != null)
		{
			merged.Add(dotGroup);
		}

		return merged;
	}
}

