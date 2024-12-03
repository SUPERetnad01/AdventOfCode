using AdventOfCode2024.Days.Day2;
using AdventOfCode2024.Days.Day3;

namespace AdventOfCode2024.Tests.Days;

public class DayThreePuzzelsTests
{

	[Fact]
	public void PartOne()
	{
		var corruptedInstruction = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
		var awnser = DayThreePuzzels.PartOne(corruptedInstruction);

		Assert.Equal(161, awnser);
	}

	[Fact]
	public void PartTwo()
	{
		var corruptedInstruction = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
		var awnser = DayThreePuzzels.PartTwo(corruptedInstruction);

		Assert.Equal(48, awnser);
	}


}
