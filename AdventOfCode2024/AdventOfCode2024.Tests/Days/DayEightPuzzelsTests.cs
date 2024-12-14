using AdventOfCode2024.Days.Day8;
using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;

namespace AdventOfCode2024.Tests.Days;

public class DayEightPuzzelsTests
{
	[Fact]
	public void PartOne()
	{
		var rawGrid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToTestInput(8));
		var antennaGrid = new Grid<char>(rawGrid);


		var answerOne = DayEightPuzzels.PartOne(antennaGrid);
		Assert.Equal(14, answerOne);
	}

	[Fact]
	public void PartTwo()
	{
		var rawGrid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToTestInput(8));
		var antennaGrid = new Grid<char>(rawGrid);


		var answerTwo = DayEightPuzzels.PartTwo(antennaGrid);
		Assert.Equal(34, answerTwo);
	}
}
