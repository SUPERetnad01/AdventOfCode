using AdventOfCode2024.Days.Day19;
using AdventOfCode2024.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Tests.Days;

public class DayNineteenPuzzlesTests
{

	[Fact]
	public void PartOne()
	{
		var inputString = File.ReadAllText(ReadInputFile.GetPathToTestInput(19));
		var regexResult = Regex.Split(inputString, "\r\n\r\n");

		var availableTowels = regexResult.First().Split(',').Select(_ => _.Trim());
		var allPatterns = regexResult[1].Split("\r\n").Select(_ => _.Trim());

		var awnserPartOne = new DayNineteenPuzzles().PartOne(availableTowels.ToList(), allPatterns.ToList());

		Assert.Equal(6, awnserPartOne);
	}

	[Fact]
	public void PartTwo()
	{
		var inputString = File.ReadAllText(ReadInputFile.GetPathToTestInput(19));
		var regexResult = Regex.Split(inputString, "\r\n\r\n");

		var availableTowels = regexResult.First().Split(',').Select(_ => _.Trim());
		var allPatterns = regexResult[1].Split("\r\n").Select(_ => _.Trim());

		var answerPartTwo = new DayNineteenPuzzles().PartTwo(availableTowels.ToList(), allPatterns.ToList());

		Assert.Equal(16, answerPartTwo);

	}
}
