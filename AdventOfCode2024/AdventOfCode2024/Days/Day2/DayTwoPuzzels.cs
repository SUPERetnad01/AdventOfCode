using AdventOfCode2024.Utils;
using System.Diagnostics;

namespace AdventOfCode2024.Days.Day2;

public static class DayTwoPuzzels
{
	public static void HandleQuestions() 
	{
		var grid = ReadInputFile.GetGrid(ReadInputFile.GetPathToInput(2));

		var stopwatch = new Stopwatch();
		stopwatch.Start();
		var partOne = PartOne(grid);
		stopwatch.Stop();
		Console.WriteLine($"Day 2 part one: {partOne}, {stopwatch.ElapsedMilliseconds} ms");
		
		stopwatch.Restart();
		var partTwo = PartTwo(grid);
		stopwatch.Stop();

		Console.WriteLine($"Day 2 part two: {partTwo}, {stopwatch.ElapsedMilliseconds} ms");
	}

	public static int PartOne(List<List<int>> reports)
	{
		var amountOfIValidReports = reports.Where(ValidReport).Count();
		return amountOfIValidReports;
	}

	private static bool ValidReport(List<int> report) 
	{
		var isAscending = report[0] - report[1] < 0;
		var isDecending = report[0] - report[1] > 0;

		var previousItem = report.First();

		foreach (var item in report.Skip(1))
		{
			
			var diffrence = item - previousItem;
			previousItem = item;
			// if order is not correct
			if ((diffrence < 0 && isAscending) || (diffrence > 0 && isDecending))
			{
				return false;
			}

			// if the diffrence is to large
			if (Math.Abs(diffrence) > 3 || diffrence == 0)
			{
				return false;
			}
		}

		return true;
	}


	private static bool ValidReportPartTwo(List<int> report)
	{
		var originalSign = Math.Sign(report[0] - report[1]);

		var amountOfErrorsInReport = 0;
		var previousItem = report.First();

		foreach (var item in report.Skip(1))
		{
			var diffrence = item - previousItem;

			var newSign = Math.Sign(previousItem - item);

			// if order is not correct
			if (newSign != originalSign)
			{
				if (amountOfErrorsInReport == 0) {
					amountOfErrorsInReport++;
					continue;
				}

				return false;
			}

			// if the diffrence is to large
			if (Math.Abs(diffrence) > 3 || diffrence == 0)
			{
				if(amountOfErrorsInReport == 0) {
					amountOfErrorsInReport++;
					continue;
				}

				return false;
			}

			previousItem = item;
		}

		return true;
	}


	public static int PartTwo(List<List<int>> reports)
	{
		var amountOfIValidReports = reports.Where(_ => !ValidReportPartTwo(_));
		var reversedInvalidReports = amountOfIValidReports.Select(_ => {
			_.Reverse();
			return _;
		});

		var finalAmountOfFailedReports = reversedInvalidReports.Where(_ => !ValidReportPartTwo(_)).Count();

		var totalwithOutReverse = reports.Count() - finalAmountOfFailedReports;

		return totalwithOutReverse;	
	}
}
