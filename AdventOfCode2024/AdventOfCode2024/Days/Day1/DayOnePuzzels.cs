using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Days.Day1;

public static class DayOnePuzzels
{
	public static void HandleQuestions()
	{
		var inputFilePath = ReadInputFile.GetPathToInput(1);

		var input = File.ReadAllLines(inputFilePath);

		var listOne = input.Select(_ => int.Parse(_.Split("   ")[0])).ToList();
		var listTwo = input.Select(_ => int.Parse(_.Split("   ")[1])).ToList();

		var partOne = PartOne(listOne, listTwo);
		Console.WriteLine($"Day1 PartOne: {partOne}");

		var partTwo = PartTwo(listOne, listTwo);
		Console.WriteLine($"Day1 PartTwo: {partTwo}");
	}

	public static int PartOne(List<int> listOne, List<int> listTwo)
	{
		var result = listOne.OrderBy(x => x)
			.Zip(listTwo.OrderBy(x => x))
			.Sum(_ => Math.Abs(_.First - _.Second));

		return result;
	}

	public static int PartTwo(List<int> listOne, List<int> listTwo)
	{
		var groupedListTwo = listTwo.GroupBy(_ => _);

		var result = listOne
			.GroupBy(x => x)
			.Where(gr => groupedListTwo.FirstOrDefault(_ => _.Key == gr.Key) != null)
			.Sum(_ =>
			{
				var amountOfTimesInListTwo = groupedListTwo.FirstOrDefault(gr => _.Key == gr.Key);
				return _.Key * amountOfTimesInListTwo.Count() * _.Count();
			});

		return result;
	}
}
