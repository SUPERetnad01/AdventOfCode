
namespace AdventOfCode2024.Days.Day5;

public class DayFivePuzzels
{
	public static void HandleQuestions()
	{
		var rootPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
		var pathToOrderingRules = Path.Combine(rootPath, "Days", $"Day5", "inputRules.txt");
		var pathToManuals = Path.Combine(rootPath, "Days", $"Day5", "inputManuals.txt");

		var orderingRules = File.ReadAllLines(pathToOrderingRules)
			.Select(_ =>
			{
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

		var awnserOne = PartOne(orderingRules, manuals);
		Console.WriteLine($"Day 5 part one: {awnserOne}");

		var awnserTwo = PartTwo(orderingRules, manuals);
		Console.WriteLine($"Day 5 part two: {awnserTwo}");
	}

	public static int PartOne(List<PageOrderingRule> orderingRules, List<List<int>> manuals)
	{
		var middleNumbers = 0;
		foreach (var manual in manuals)
		{
			var (isValidManual, _) = IsValidManual(manual, orderingRules);

			if (!isValidManual)
			{
				continue;
			}

			middleNumbers += manual[manual.Count / 2];

		}

		return middleNumbers;
	}

	public static (bool, List<PageOrderingRule>) IsValidManual(List<int> manual, List<PageOrderingRule> orderingRules)
	{
		var setupOrderingRules = orderingRules
				.Where(_ => manual.Contains(_.RuleX) && manual.Contains(_.RuleY))
				.Select(_ => new PageOrderingRule()
				{
					RuleX = _.RuleX,
					RuleY = _.RuleY,
					XIndex = manual.IndexOf(_.RuleX),
					YIndex = manual.IndexOf(_.RuleY)
				}).ToList();

		var isInvalidManual = setupOrderingRules
			.Select(_ => _.IsValidRule())
			.Contains(false);

		return (!isInvalidManual, setupOrderingRules);
	}


	public static int PartTwo(List<PageOrderingRule> orderingRules, List<List<int>> manuals)
	{
		var incorrectManuals = manuals
			.Select(_ => (_, IsValidManual(_, orderingRules)))
			.Where(_ => _.Item2.Item1 == false)
			.Select(_ => (_.Item1, _.Item2.Item2));

		var result = incorrectManuals
		   .Select(_ => SolveManual(_.Item1, _.Item2).Item1)
		   .Select(_ => _[_.Count / 2])
		   .Sum();

		return result;
	}

	private static (List<int>, List<PageOrderingRule>) SolveManual(List<int> manual, List<PageOrderingRule> pageOrderingRules)
	{
		var faultyRule = pageOrderingRules.Where(_ => !_.IsValidRule()).First();

		if (faultyRule == null)
		{
			return (manual, pageOrderingRules);
		}
		manual[faultyRule.XIndex] = faultyRule.RuleY;
		manual[faultyRule.YIndex] = faultyRule.RuleX;


		var valid = IsValidManual(manual, pageOrderingRules);
		if (!valid.Item1)
		{
			return SolveManual(manual, valid.Item2);
		}

		return (manual, pageOrderingRules);
	}
}

public class PageOrderingRule
{
	public int RuleX { get; set; }
	public int RuleY { get; set; }

	public int XIndex { get; set; }
	public int YIndex { get; set; }

	public bool IsValidRule()
	{
		return XIndex < YIndex;
	}

}
