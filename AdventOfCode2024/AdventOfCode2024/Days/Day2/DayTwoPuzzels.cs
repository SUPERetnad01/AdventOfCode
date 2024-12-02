using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Days.Day2;

public static class DayTwoPuzzels
{
	public static void HandeQuestions() 
	{
		var grid = ReadInputFile.GetGrid(ReadInputFile.GetPathToInput(2));
		var partOne = PartOne(grid);
		Console.WriteLine($"Day2 PartOne: {partOne}");

		var partTwo = PartTwo(grid);
		Console.WriteLine($"Day2 PartTwo: {partTwo}");
	}

	public static int PartOne(List<List<int>> reports)
	{
		var notSafeReports = 0;
		foreach (var report in reports)
		{

			var previousItem = 0;
			var isIncreasing = report[0] - report[1] < 0;
			foreach(var item in report)
			{
				if (previousItem == 0) {
					previousItem = item;
					continue;
				}

				var diffrence = item - previousItem;

				previousItem = item;

				if((isIncreasing && diffrence < 0) || (!isIncreasing && diffrence > 0)) {
					notSafeReports++;
					break;
				}
				

				if (Math.Abs(diffrence) > 3 || diffrence == 0) 
				{
					notSafeReports++;
					break;
				}

			}
		}

		var totalSafeReports =  reports.Count - notSafeReports;
		return totalSafeReports;
	}

	public static int PartTwo(List<List<int>> reports)
	{
		var notSafeReports = 0;
		foreach (var report in reports)
		{

			var previousItem = 0;
			var amountOfErrors = 0;
			var isIncreasing = report[0] - report[1] < 0;
			foreach (var item in report)
			{
				if (previousItem == 0)
				{
					previousItem = item;
					continue;
				}

				var diffrence = item - previousItem;

			

				if ((isIncreasing && diffrence < 0) || (!isIncreasing && diffrence > 0))
				{
					if (amountOfErrors > 0) {
						notSafeReports++;
						break;
					}
					amountOfErrors++;
					continue;
			
				}


				if (Math.Abs(diffrence) > 3 || diffrence == 0)
				{
					if (amountOfErrors > 0)
					{
						notSafeReports++;
						break;
					}
					amountOfErrors++;
					continue;
				}

				previousItem = item;
			}
		}

		var totalSafeReports = reports.Count - notSafeReports;
		return totalSafeReports;
	}
}
