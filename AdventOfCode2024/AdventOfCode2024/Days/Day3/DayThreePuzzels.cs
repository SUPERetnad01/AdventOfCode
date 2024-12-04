using AdventOfCode2024.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days.Day3;

public static class DayThreePuzzels
{
	public static void HandleQuestions()
	{

		var path = ReadInputFile.GetPathToInput(3);
		var input = File.ReadAllText(path);

		var partOne = PartOne(input);
		Console.WriteLine($"Day 3 PartOne: {partOne}");

		var partTwo = PartTwo(input);
		Console.WriteLine($"Day 3 PartTwo: {partTwo}");
	}

	public static int PartOne(string corruptedInstruction)
	{
		string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
		var matches = Regex.Matches(corruptedInstruction, pattern).ToList();

		var total = matches
			.Select(_ => int.Parse(_.Groups[1].Value) * int.Parse(_.Groups[2].Value))
			.Sum();

		return total;
	}

	public static int PartTwo(string corruptedInstruction)
	{
		var splitondo = corruptedInstruction
			.Split("do()")
			.Select(_ => _.Split("don't()").First())
			.Select(PartOne).Sum();

		return splitondo;

	}
}
