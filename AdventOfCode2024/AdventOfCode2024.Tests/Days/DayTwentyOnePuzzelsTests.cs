using AdventOfCode2024.Days.Day21;

namespace AdventOfCode2024.Tests.Days;

public class DayTwentyOnePuzzelsTests
{
	[Fact]
	public void PartOne()
	{
		var input = new List<string>()
		{
			"029A",
			"980A",
			"179A",
			"456A",
			"379A",
		};

		var partOne = new DayTwentyOnePuzzels().PartOne(input);
		Assert.Equal(126384, partOne);
	}


	[Fact]
	public void PartTwo()
	{
		var input = new List<string>()
		{
			"029A",
			"980A",
			"179A",
			"456A",
			"379A",
		};

		var partOne = new DayTwentyOnePuzzels().PartTwo(input);
		Assert.Equal(154115708116294, partOne);
	}
}
