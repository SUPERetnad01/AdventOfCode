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
		var isAscending = report[0] - report[1] < 0;
		var isDecending = report[0] - report[1] > 0;

		var amountOfErrorsInReport = 0;
		var previousItem = report.First();

		foreach (var item in report.Skip(1))
		{
			var diffrence = item - previousItem;
	

			// if order is not correct
			if ((diffrence < 0 && isAscending) || (diffrence > 0 && isDecending))
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
