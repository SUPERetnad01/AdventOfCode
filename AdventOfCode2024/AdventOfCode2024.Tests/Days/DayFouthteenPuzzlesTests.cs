using AdventOfCode2024.Days.Day14;
using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Tests.Days;

public class DayFouthteenPuzzlesTests
{
	[Fact]
	public void PartOne()
	{
		var testinput = File.ReadAllLines(ReadInputFile.GetPathToTestInput(14)).ToList();
		var result = new DayFourthteenPuzzles().PartOne(testinput,7,11);

		Assert.Equal(12,result);
	}
}
