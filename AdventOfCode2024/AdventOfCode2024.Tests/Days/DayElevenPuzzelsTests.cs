using AdventOfCode2024.Days.Day11;

namespace AdventOfCode2024.Tests.Days;

public class DayElevenPuzzelsTests
{

	[Fact]
	public void PartOne() 
	{
		var newlist = new List<int>() { 125, 17 };
		var partOne = DayElevenPuzzels.PartOne(newlist,25);

		Assert.Equal(55312,partOne);
	}
}
