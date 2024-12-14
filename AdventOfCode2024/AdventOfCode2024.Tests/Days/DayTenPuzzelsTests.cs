using AdventOfCode2024.Days.Day10;
using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;

namespace AdventOfCode2024.Tests.Days;

public class DayTenPuzzelsTests
{

	[Fact]
	public void PartOne() 
	{
		var rawGrid = ReadInputFile.GetPathToTestInput(10);
		var x = ReadInputFile.GetGridWithOutSplit(rawGrid);
		var grid = new Grid<int>(x);

		var partOne = new DayTenPuzzels().PartOne(grid);
		Assert.Equal(36, partOne);
	
	}

	[Fact]
	public void PartTwo()
	{
		var rawGrid = ReadInputFile.GetGridWithOutSplit(ReadInputFile.GetPathToTestInput(10));
		var grid = new Grid<int>(rawGrid);

		var partTwo = new DayTenPuzzels().PartTwo(grid);
		Assert.Equal(81, partTwo);

	}
}
