using AdventOfCode2024.Days.Day15;
using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Tests.Days;

public class DayFifteenPuzzlesTests
{
	[Fact]
	public void PartOne()
	{
		var input = File.ReadAllText(ReadInputFile.GetPathToTestInput(15));
		var result = new DayFiftheenPuzzels().PartOne(input);
	
		Assert.Equal(10092, result);
	}

	[Fact]
	public void PartTwo()
	{
		var input = File.ReadAllText(ReadInputFile.GetPathToTestInput(15));
		var result = new DayFiftheenPuzzels().PartTwo(input);

		Assert.Equal(9021, result);
	}
}
