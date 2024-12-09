using AdventOfCode2024.Days.Day9;

namespace AdventOfCode2024.Tests.Days;

public class DayNinePuzzelsTests
{
	[Fact]
	public void PartOne() 
	{
		var input = "2333133121414131402";
		var awnserPartOne = DayNinePuzzels.PartOne(input);

		Assert.Equal(1928, awnserPartOne);
	}

	[Fact]
	public void PartTwo()
	{
		var input = "2333133121414131402";
		var awnserPartTwo = DayNinePuzzels.PartTwo(input);

		Assert.Equal(2858, awnserPartTwo);
	}
}
