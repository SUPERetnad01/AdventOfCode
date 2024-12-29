using AdventOfCode2024.Days.Day17;
using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Tests.Days;

public class DaySeventeenPuzzlesTests
{
	[Fact]
	public void PartOne()
	{
		var input = File.ReadAllText(ReadInputFile.GetPathToTestInput(17));

		var awnwer1 = new DaySeventeenPuzzles().PartOne(input);

		Assert.Equal("4,6,3,5,6,3,5,2,1,0", awnwer1);
	}
}
