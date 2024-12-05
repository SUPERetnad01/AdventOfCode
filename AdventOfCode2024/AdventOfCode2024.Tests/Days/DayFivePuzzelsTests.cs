using AdventOfCode2024.Days.Day5;

namespace AdventOfCode2024.Tests.Days;

public class DayFivePuzzelsTests
{
	[Fact]
	public void PartOne()
	{
		var orderingRulesInput = @"
			47|53
			97|13
			97|61
			97|47
			75|29
			61|13
			75|53
			29|13
			97|29
			53|29
			61|53
			97|53
			61|29
			47|13
			75|47
			97|75
			47|61
			75|61
			47|29
			75|13
			53|13";

		var orderingRules = orderingRulesInput
			.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
			.Select(_ =>
			{
				var xandY = _.Split("|");

				return new PageOrderingRule()
				{
					RuleX = int.Parse(xandY[0]),
					RuleY = int.Parse(xandY[1]),
				};

			}).ToList();


		var manualOrder = new List<List<int>> {
			new() {75,47,61,53,29},
			new() {97,61,53,29,13},
			new() {75,29,13 },
			new() {75,97,47,61,53},
			new() {61,13,29},
			new() {97,13,75,29,47}
		};

		var answerPartOne = DayFivePuzzels.PartOne(orderingRules, manualOrder);

		Assert.Equal(143, answerPartOne);




	}
	[Fact]
	public void PartTwo()
	{
		var orderingRulesInput = @"
			47|53
			97|13
			97|61
			97|47
			75|29
			61|13
			75|53
			29|13
			97|29
			53|29
			61|53
			97|53
			61|29
			47|13
			75|47
			97|75
			47|61
			75|61
			47|29
			75|13
			53|13";

		var orderingRules = orderingRulesInput
			.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
			.Select(_ =>
			{
				var xandY = _.Split("|");

				return new PageOrderingRule()
				{
					RuleX = int.Parse(xandY[0]),
					RuleY = int.Parse(xandY[1]),
				};

			}).ToList();


		var manualOrder = new List<List<int>> {
			new() {75,47,61,53,29},
			new() {97,61,53,29,13},
			new() {75,29,13 },
			new() {75,97,47,61,53},
			new() {61,13,29},
			new() {97,13,75,29,47}
		};

		var answerTwo = DayFivePuzzels.PartTwo(orderingRules, manualOrder);

		Assert.Equal(123, answerTwo);
	}
}