using AdventOfCode2024.Days.Day7;

namespace AdventOfCode2024.Tests.Days;

public class DaySevenPuzzelsTests
{
	[Fact]
	public void PartOne()
	{
		var testResult = new List<(long, List<long>)>() {
			{ (190, new List<long>() { 10, 19 }) },
			{(3267, new List<long>() { 81,40,27})},
			{ (83,new List<long>() { 17,5})},

			{ (156,new List<long>() { 15,6})},
			{ (7290,new List<long>() { 6,8,6,15}) },
			{ (161011,new List<long>() { 16,10,13})},

			{ (192,new List<long>() { 17,8,14})},
			{ (21037,new List<long>() { 9,7,18,13})},
			{ (292,new List<long>() { 11,6,16,20})},
		};

		var anwserPartOne = DaySevenPuzzels.PartOne(testResult);

		Assert.Equal(3749, anwserPartOne);
	}

	[Fact]
	public void PartTwo()
	{
		var testResult = new List<(long, List<long>)>() {

			{ (111, new List<long>() { 11,1,2 }) },
			{ (190, new List<long>() { 10, 19}) },
			{(3267, new List<long>() { 81,40,27})},
			{ (83,new List<long>() { 17,5})},

			{ (156,new List<long>() { 15,6})},
			{ (7290,new List<long>() { 6,8,6,15}) },
			{ (161011,new List<long>() { 16,10,13})},

			{ (192,new List<long>() { 17,8,14})},
			{ (21037,new List<long>() { 9,7,18,13})},
			{ (292,new List<long>() { 11,6,16,20})},
		};

		var anwserPartOne = DaySevenPuzzels.PartTwo(testResult);

		Assert.Equal(11387, anwserPartOne);
	}

}
