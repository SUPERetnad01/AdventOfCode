using AdventOfCode2024.Utils.Grid;
using AdventOfCode2024.Utils;
using System.Security.Cryptography;

namespace AdventOfCode2024.Days.Day5;

public class DayFivePuzzels
{
	public static void HandleQuestions()
	{
		var rootPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
		var pathToOrderingRules = Path.Combine(rootPath, "Days", $"Day5", "inputRules.txt");
		var pathToManuals = Path.Combine(rootPath, "Days", $"Day5", "inputManuals.txt");

		var orderingRules = File.ReadAllLines(pathToOrderingRules)
			.Select(_ => {
				var xandY = _.Split("|");

				return new PageOrderingRule()
				{
					RuleX = int.Parse(xandY[0]),
					RuleY = int.Parse(xandY[1]),
				};

			}).ToList();

		var manuals = File.ReadAllLines(pathToManuals)
			.Select(_ => _.Split(",").Select(_ => int.Parse(_)).ToList()
			).ToList();

		var awnserOne = PartOne(orderingRules,manuals);
		Console.WriteLine($"Day 5 part one: {awnserOne}");

		//var awnserTwo = PartTwo();
		//Console.WriteLine($"Day 4 part two: {awnserTwo}");
	}

	public static int PartOne(List<PageOrderingRule> orderingRules, List<List<int>> manuals)
	{
		var middleNumbers = 0;
		foreach(var manual in manuals)
		{
			var isInValidmanual = orderingRules
				.Where(_ => manual.Contains(_.RuleX) && manual.Contains(_.RuleY))
				.Select(_ => new PageOrderingRule()
				{
					RuleX = _.RuleX,
					RuleY = _.RuleY,
					XIndex = manual.IndexOf(_.RuleX),
					YIndex = manual.IndexOf(_.RuleY)
				})
				.Select(_ => _.IsValidRule())
				.Contains(false);

			if (isInValidmanual)
			{
				continue;
			}

			middleNumbers  += manual[manual.Count / 2];

		}

		return middleNumbers;
	}

	public static int PartTwo()
	{
		return 1;
	}
}

public class PageOrderingRule { 
	public int RuleX { get; set; }
	public int RuleY { get; set; }

	public int XIndex { get; set; }
	public int YIndex { get; set; }

	public bool IsValidRule()
	{
		return XIndex < YIndex;
	}

}
