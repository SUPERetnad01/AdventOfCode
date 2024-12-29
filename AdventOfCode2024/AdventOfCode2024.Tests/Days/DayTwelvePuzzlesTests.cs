using AdventOfCode2024.Days.Day12;
using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;

namespace AdventOfCode2024.Tests.Days;

public class DayTwelvePuzzlesTests
{

	[Fact]
	public void PartOneTest()
	{
		var rawGrid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToTestInput(12));

		var grid = new Grid<char>(rawGrid);

		var result1 = new DayTwelvePuzzles()
			.PartOne(grid);

		Assert.Equal(1930, result1);
	}

	[Fact]
	public void PartTwoTest()
	{
		var rawGrid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToTestInput(12));

		var grid = new Grid<char>(rawGrid);

		var result2 = new DayTwelvePuzzles()
			.PartTwo(grid);

		Assert.Equal(1206, result2);
	}
}
