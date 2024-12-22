using AdventOfCode2024.Days.Day22;
using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Tests.Days;

public class DayTwentyTwoPuzzelsTests
{
	[Fact]
	public void PartOneTests()
	{
		var input = File.ReadAllLines(ReadInputFile.GetPathToTestInput(22))
			.Select(_ => int.Parse(_.ToString()));

		var partOne = new DayTwentyTwoPuzzels().PartOne(input.ToList(),2000);
		Assert.Equal(37327623, partOne);
	}

	[Fact]
	public void PartTwoTests()
	{
		var input = File.ReadAllLines(ReadInputFile.GetPathToTestInput(22))
			.Select(_ => int.Parse(_.ToString())).ToList();

		var partOne = new DayTwentyTwoPuzzels().PartTwo(input, 2000);
		Assert.Equal(23, partOne);
	}
}
