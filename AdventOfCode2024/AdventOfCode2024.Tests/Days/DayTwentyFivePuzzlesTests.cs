using AdventOfCode2024.Days.Day25;
using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Tests.Days;

public class DayTwentyFivePuzzlesTests
{
	[Fact]
	public void PartOne()
	{
		var input = File.ReadAllText(ReadInputFile.GetPathToTestInput(25));
		var awnser = new DayTwentyFivePuzzles().PartOne(input);
	}
}
