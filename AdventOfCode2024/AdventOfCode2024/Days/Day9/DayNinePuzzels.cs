using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Days.Day9;

public static class DayNinePuzzels
{
	public static void HandlePuzzels() {
		var input = File.ReadAllText(ReadInputFile.GetPathToInput(9));
		var resultPartOne = PartOne(input);
	}

	public static int PartOne(string input) {

		var resultString = "";
		var currentId = 0;
		var numbersList = input.Select(_ => int.Parse(_.ToString())).ToList();

        for (int i = 0; i < numbersList.Count(); i++)
        {
			var numberToCheck = numbersList[i];

			if(i % 2 != 0)
			{
				var freeSpace = String.Concat(Enumerable.Repeat('.', numberToCheck));
				resultString += freeSpace;
				continue;
			}

			var x = String.Concat(Enumerable.Repeat(currentId.ToString(), numberToCheck));
			resultString += x;

			currentId++;
        }


		// whle no . are pressent move the end of the list to the nearest dot;

        return 0;
	}
}
