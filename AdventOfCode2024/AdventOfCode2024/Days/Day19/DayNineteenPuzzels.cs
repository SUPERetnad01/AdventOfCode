using AdventOfCode2024.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Days.Day19;

public class DayNineteenPuzzles
{
	public void HandlePuzzles()
	{
		var inputString = File.ReadAllText(ReadInputFile.GetPathToInput(19));
		var regexResult = Regex.Split(inputString, "\r\n\r\n");

		var availableTowels = regexResult.First().Split(',').Select(_ => _.Trim());
		var allPatterns = regexResult[1].Split("\r\n").Select(_ => _.Trim());

		var answerPartOne = new DayNineteenPuzzles().PartOne(availableTowels.ToList(), allPatterns.ToList());
		Console.WriteLine($"Day 19 Part one: {answerPartOne}");

		var answerPartTwo = new DayNineteenPuzzles().PartTwo(availableTowels.ToList(), allPatterns.ToList());
		Console.WriteLine($"Day 19 Part two: {answerPartTwo}");
	}

	public int PartOne(List<string> availableTowels, List<string> allPatterns)
	{
		var validPatterns = 0;
		foreach (var pattern in allPatterns)
		{
			var validPattern = CanMakePattern(availableTowels, pattern);
			if (validPattern)
			{
				validPatterns++;
			}
		}


		return validPatterns;
	}

	public int PartTwo(List<string> availableTowels, List<string> allPatterns)
	{
		var amountOfPatterns = 0;
		foreach (var pattern in allPatterns)
		{
			amountOfPatterns += AllPossiblePatterns(availableTowels, pattern);
		}


		return amountOfPatterns;
	}

	private Dictionary<string, bool> Memoization { get; set; } = [];
	public bool CanMakePattern(List<string> availableTowels, string pattern)
	{

		if (pattern == string.Empty)
		{
			return true;
		}

		var matchingTowels = availableTowels.Where(pattern.StartsWith);

		if (Memoization.TryGetValue(pattern, out var isValid))
		{
			return isValid;
		}


		foreach (var towel in matchingTowels)
		{
			var stripedTowel = pattern.Substring(towel.Length);
			var canMakePattern = CanMakePattern(availableTowels, stripedTowel);
			if (canMakePattern)
			{

				Memoization.TryAdd(stripedTowel, true);
				return true;
			}

		}
		Memoization.Add(pattern, false);
		return false;
	}

	private Dictionary<string, int> MemoizationTwo { get; set; } = [];

	public int AllPossiblePatterns(List<string> availableTowels, string pattern)
	{

		if (pattern == string.Empty)
		{
			return 1;
		}

		var matchingTowels = availableTowels.Where(pattern.StartsWith);

		if (MemoizationTwo.TryGetValue(pattern, out var value) && value < 0)
		{
			return value;
		}

		var amountOfPatterns = 0;

		foreach (var towel in matchingTowels)
		{
			var stripedTowel = pattern.Substring(towel.Length);
			amountOfPatterns += AllPossiblePatterns(availableTowels, stripedTowel);

			if (amountOfPatterns >= 1)
			{
				MemoizationTwo.TryAdd(stripedTowel, amountOfPatterns);
			}

		}
		if (amountOfPatterns == 0)
			MemoizationTwo.TryAdd(pattern, 0);

		return amountOfPatterns;
	}
}
