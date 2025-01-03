﻿using AdventOfCode2024.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

		var stopwatch = new Stopwatch();
		stopwatch.Start();
		var partOne = PartOne(input);
		stopwatch.Stop();
		Console.WriteLine($"Day 3 part one: {partOne}, {stopwatch.ElapsedMilliseconds} ms");

		stopwatch.Restart();
		var partTwo = PartTwo(input);
		stopwatch.Stop();
		Console.WriteLine($"Day 3 part two: {partTwo}, {stopwatch.ElapsedMilliseconds} ms");

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
