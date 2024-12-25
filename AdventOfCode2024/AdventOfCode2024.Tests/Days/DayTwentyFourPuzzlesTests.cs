using AdventOfCode2024.Days.Day24;
using AdventOfCode2024.Utils;
using System.Security.Cryptography;

namespace AdventOfCode2024.Tests.Days;

public class DayTwentyFourPuzzlesTests
{
	[Fact]
	public void PartOneTest() 
	{
		var input = File.ReadAllText(ReadInputFile.GetPathToTestInput(24));

		var result = new DayTwentyFourPuzzles().PartOne(input);
		Assert.Equal(2024, result);

	}

	[Fact]
	public void PartTwoTest()
	{
		var rootPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
		var pathToQuestion = Path.Combine(rootPath, "TestInput", $"input24pt2.txt");

		var input = File.ReadAllText(pathToQuestion);
		var result = new DayTwentyFourPuzzles().PartTwo(input);
		Assert.Equal(2024, result);

	}
}
