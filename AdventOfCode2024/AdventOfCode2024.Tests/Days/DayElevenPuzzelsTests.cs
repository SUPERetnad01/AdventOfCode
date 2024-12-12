using AdventOfCode2024.Days.Day11;

namespace AdventOfCode2024.Tests.Days;

public class DayElevenPuzzelsTests
{

	[Fact]
	public void PartOne()
	{
		var newlist = new List<long>() { 125, 17 };
		var partOne = new DayElevenPuzzles().PartTwo(newlist, 25);

		Assert.Equal(55312, partOne);
	}

	[Fact]
	public void PartOneTest2()
	{
		var newlist = new List<long>() { 125, 17 };
		var partOne = new DayElevenPuzzles().PartTwo(newlist, 6);

		Assert.Equal(22, partOne);
	}

	[Fact]
	public void AnswerPartOne()
	{
		var newlist = new List<long>() { 554735, 45401, 8434, 0, 188, 7487525, 77, 7 };
		var partOne = new DayElevenPuzzles().PartTwo(newlist, 25);

		Assert.Equal(209412, partOne);
	}
}
