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
		var attenaGrid = new Grid<char>(rawGrid);


		var awnserOne = DayEightPuzzels.PartOne(attenaGrid);
		Assert.Equal(14, awnserOne);
	}
}
