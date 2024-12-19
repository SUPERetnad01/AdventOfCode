using AdventOfCode2024.Utils.Grid;
using AdventOfCode2024.Utils;
using AdventOfCode2024.Days.Day16;

namespace AdventOfCode2024.Tests.Days;

public class DaySixteenPuzzelsTests
{
	[Fact]
	public void PartOne()
	{
		var rawGrid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToTestInput(16));
		var grid = new Grid<char>(rawGrid);

		var partOne = new  DaySixteenPuzzels().PartOne(grid);

		Assert.Equal(7036, partOne);
	}

	[Fact]
	public void PartTwo()
	{
		var rawGrid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToTestInput(16));
		var grid = new Grid<char>(rawGrid);

		var partTwo = new DaySixteenPuzzels().PartTwo(grid);

		Assert.Equal(64, partTwo);
	}
}
