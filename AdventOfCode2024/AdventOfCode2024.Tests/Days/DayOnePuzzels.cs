namespace AdventOfCode2024.Tests.Days;
public class DayOnePuzzels
{
	[Fact]
	public void PartOne()
	{
		var listOne = new List<int>() { 3, 4, 2, 1, 3, 3 };
		var listTwo = new List<int>() { 4, 3, 5, 3, 9, 3 };

		var question1 = AdventOfCode2024.Days.Day1.DayOnePuzzels.PartOne(listOne, listTwo);

		Assert.Equal(11, question1);
	}

	[Fact]
	public void PartTwo()
	{
		var listOne = new List<int>() { 3, 4, 2, 1, 3, 3 };
		var listTwo = new List<int>() { 4, 3, 5, 3, 9, 3 };

		var question1 = AdventOfCode2024.Days.Day1.DayOnePuzzels.PartTwo(listOne, listTwo);

		Assert.Equal(31, question1);
	}
}
