using AdventOfCode2024.Days.Day2;

namespace AdventOfCode2024.Tests.Days;

public class DayTwoPuzzelsTests
{
	[Fact]
	public void PartOne()
	{
		var reports = new List<List<int>>()
		{
			new() { 7,6,4,2,1 },
			new() { 1,2,7,8,9 },
			new() { 9,7,6,2,1 },
			new() { 1,3,2,4,5 },
			new() { 8,6,4,4,1 },
			new() { 1,3,6,7,9 },
			
		};

		var awnser = DayTwoPuzzels.PartOne(reports);


		Assert.Equal(2, awnser);
	}

	[Fact]
	public void PartTwo()
	{
		var reports = new List<List<int>>()
		{
			new() { 7,6,4,2,1 },
			new() { 1,2,7,8,9 },
			new() { 9,7,6,2,1 },
			new() { 1,3,2,4,5 },
			new() { 8,6,4,4,1 },
			new() { 1,3,6,7,9 },

		};

		var awnser = DayTwoPuzzels.PartTwo(reports);


		Assert.Equal(4, awnser);
	}
}
