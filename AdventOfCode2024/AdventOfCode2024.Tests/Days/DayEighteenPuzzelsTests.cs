using AdventOfCode2024.Days.Day18;
using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;

namespace AdventOfCode2024.Tests.Days;

public class DayEighteenPuzzelsTests
{
	[Fact]
	public void PartOne()
	{
		var coordinates = File.ReadLines(ReadInputFile.GetPathToTestInput(18))
			.Select(_ =>
			{
				var splitcord = _.Split(",");
				return new Coordinate() { X = int.Parse(splitcord[0]), Y = int.Parse(splitcord[1]) };
			});

		var partOne = new DayEighteenPuzzles().PartOne(coordinates.ToList(), 6, 12);

		Assert.Equal(22, partOne);
	}

	[Fact]
	public void PartTwo()
	{
		var coordinates = File.ReadLines(ReadInputFile.GetPathToTestInput(18))
			.Select(_ =>
			{
				var splitcord = _.Split(",");
				return new Coordinate() { X = int.Parse(splitcord[0]), Y = int.Parse(splitcord[1]) };
			});

		var partTwo = new DayEighteenPuzzles().PartTwo(coordinates.ToList(), 6);

		Assert.Equal(new Coordinate() { X = 6, Y = 1 }, partTwo);
	}
}
